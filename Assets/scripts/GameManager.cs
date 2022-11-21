using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using handNameSpace;
using cardNameSpace;
using RP;
using factoryNameSpace;
using SceneManagerNS;
using FactoryDrawing;
using CardDrawing;

namespace GMNameSpace {

    public class PlayCardEvent : UnityEvent<int> {}

    public class GameManager : MonoBehaviour {
        
        static float xScale = (float)1920 / (float)Screen.width;
        static float yScale = (float)1080 / (float)Screen.height;
        public static Vector2 screenScale = new Vector2(xScale,yScale);
        
        public static PlayCardEvent tryToPlayCard;
        public static System.Random random = new System.Random();

        private Deck factoryDeck;
        private Deck dealsDeck;
        private Deck factoryDiscard;
        private Deck dealsDiscard;
        private Hand hand;
        private ResourcePanel resources;

        public GameObject factoryDeckGO;
        public GameObject dealsDeckGO;
        public GameObject factoryDiscardGO;
        public GameObject dealsDiscardGO;
        public GameObject handGO;
        public GameObject BoardGO;
        public GameObject ResourcesGO;

        private int turn = 0;
        private int year = 1;
    

        public int factoryDraw;
        public int dealsDraw;
        public int balance = 5;
        public int baseFunding = 3;
        private int funding;
        public int pollution = 0;
        public int backing;
        public int maxPollution;
        public int power;
        public int powerRequirement;

        public static int backingTop = 85;
        public static int backingBottom = 30;
        
        public int winCounter;
        // Turns without having created polution till the player wins.
        public int winCon;
        private int pollutionStartTurn;
        private bool powerReqMet;


        public GameObject FactoryPrefab;

        void Start() {
            factoryDeck = factoryDeckGO.GetComponent<Deck>();
            dealsDeck = dealsDeckGO.GetComponent<Deck>();
            factoryDiscard = factoryDiscardGO.GetComponent<Deck>();
            dealsDiscard = dealsDiscardGO.GetComponent<Deck>();
            hand = handGO.GetComponent<Hand>();
            resources = ResourcesGO.GetComponent<ResourcePanel>();

            tryToPlayCard = new PlayCardEvent();
            tryToPlayCard.AddListener(PlayCard);
            
            funding = baseFunding;
            resources.update_text(balance, funding, pollution, maxPollution, turn, year, powerRequirement, power, backing);

            factoryDeck.shuffle();
            dealsDeck.shuffle();
            DrawHand();
        }

        void Update() {
            
            if (backing > 100) {
                backing = 100;
            }
            if (pollution < 0) {
                pollution = 0;
            }
            if (backing <= 0 || pollution >= maxPollution) {
                SceneManagement.ChangeScene("LoserScene");
            }
            if (backing >= 90) {
                funding = 2*baseFunding;
            } else if (backing <= 30) {
                funding = (int)(0.5f*(float)baseFunding);
            } else {
                funding = baseFunding;
            }
            resources.update_text(balance, funding, pollution, maxPollution, turn, year, powerRequirement, power, backing);
        }

        public void NextTurn() {
            filterToDiscard(hand.DiscardHand());
            if (power < powerRequirement) {
                backing -= 15;
                powerReqMet = false;
            }
            if (power >= powerRequirement) {
                backing += 5;
                powerReqMet = true;
            }
            balance += funding;
            power = 0;

            foreach (Transform factoryTransform in BoardGO.transform) {
                factoryTransform.gameObject.GetComponent<DisplayFactory>().factory.Upkeep();
            }

            if (powerReqMet && pollutionStartTurn >= pollution) {
                winCounter += 1;
                if (winCounter >= winCon) {
                    print("Du har vundet spillet");
                }
            } else {
                winCounter = 0;
            }

            if (turn == 3) {
                year += 1;
                ShuffleDiscardIntoDeck(CardType.FactoryType);
                ShuffleDiscardIntoDeck(CardType.DealType);
                powerRequirement += 3;
            }

            turn = (turn+1)%4;
            DrawHand();
            pollutionStartTurn = pollution;
        }
        
        public void Draw(CardType deckToDrawFrom, int drawAmount){
            for (int i = 0; i < drawAmount; i++) {
                switch(deckToDrawFrom){
                    case CardType.FactoryType:
                        if (factoryDeck.size() == 0) {
                            if (factoryDiscard.size() == 0) {
                                Debug.Log("There are no more factory cards to draw");
                                return;
                            }
                            ShuffleDiscardIntoDeck(CardType.FactoryType);
                        }
                        hand.AddCard(factoryDeck.drawCard());
                        break;
                    case CardType.DealType:
                        if (dealsDeck.size() == 0) {
                            if (dealsDiscard.size() == 0) {
                                Debug.Log("There are no more deal cards to draw");
                                return;
                            }
                            ShuffleDiscardIntoDeck(CardType.DealType);
                        }
                        hand.AddCard(dealsDeck.drawCard());
                        break;
                }
            }
            hand.CreateHand();
        }

        public void DrawHand(){
            Draw(CardType.FactoryType, factoryDraw);
            Draw(CardType.DealType, dealsDraw);
            hand.CreateHand();
        }
        
        public void ShuffleDiscardIntoDeck(CardType discardPile){
            if (discardPile == CardType.DealType) {
                dealsDeck.add(dealsDiscard);
                dealsDiscard.clear();
                dealsDeck.shuffle();
            } else {
                factoryDeck.add(factoryDiscard);
                factoryDiscard.clear();
                factoryDeck.shuffle();
            }

        }

        public void filterToDiscard(List<Card> discard) {
            foreach (Card card in discard) {
                filterToDiscard(card);
            }
        }

        public void filterToDiscard(Card card) {
            if (card.type == CardType.FactoryType) {
                factoryDiscard.add(card);
            } else {
                dealsDiscard.add(card);
            }
        }

        public void PlayCard(int cardIndex) {
            if (TryToPay(hand.hand[cardIndex].cardCost)) {
                Card discard = hand.Discard(cardIndex);
                PlayEffects(discard.cardCost);
                PlayEffects(discard.cardEffect);
                filterToDiscard(discard);
            }
        }

        public bool TryToPay(List<Effect> resources){
            foreach (Effect resource in resources) {
                if (!TryToPay(resource)) {
                    return false;
                }
            }
            return true;
        }

        public bool TryToPay(Effect resource) {
            switch (resource.effectType){
                case EffectType.Money:
                    return (balance + resource.amount >= 0);
                case EffectType.Funding:
                    return (baseFunding + resource.amount > 0);
                case EffectType.Backing:
                    return (backing + resource.amount > 0);
                case EffectType.Power:
                    return (power + resource.amount >= 0);
                case EffectType.Pollution:
                    return (resource.amount + pollution < maxPollution);
                case EffectType.DiscardRandom:
                    return (hand.hand.Count > 0);
                case EffectType.DiscardHand: 
                    return true;
            }
            return false;
        }

        public void PlayEffects(List<Effect> effects) {
            foreach (Effect effect in effects) {
                PlayEffect(effect);
            }
        }
        
        public void PlayEffect(Effect effect) {
            switch (effect.effectType){
                case EffectType.Money:
                    balance += effect.amount;
                    break;
                case EffectType.Funding:
                    baseFunding += effect.amount;
                    break;
                case EffectType.Backing:
                    backing += effect.amount;
                    break;
                case EffectType.Power:
                    power += effect.amount;
                    break;
                case EffectType.Pollution:
                    pollution += effect.amount;
                    break;
                case EffectType.Draw:
                    if (effect.name == "Factory") {
                        Draw(CardType.FactoryType, effect.amount);
                    } else {
                        Draw(CardType.DealType, effect.amount);
                    }
                    break;
                case EffectType.DrawRandom:
                    for (int i = 0; i < effect.amount; i++ ) {
                        if (random.Next(1,2) == 1) {
                            Draw(CardType.FactoryType, 1);
                        } else {
                            Draw(CardType.DealType, 1);
                        }
                    }
                    break;
                case EffectType.DrawHand:
                    DrawHand();
                    break;
                case EffectType.DiscardHand:
                    hand.DiscardHand();
                    break;
                case EffectType.DiscardRandom:
                    for (int i = 0; i < effect.amount; i++ ) {
                        if (i > hand.hand.Count) { 
                            break;
                        }
                        filterToDiscard(
                            hand.Discard(
                                hand.handDisplay[random.Next(hand.hand.Count)].GetComponent<DisplayCard>().cardID
                            )
                        );
                        hand.CreateHand();
                    }
                    break;
                case EffectType.CreateFactory:
                    CreateFactory(effect.name);
                    break;
            }
        }

        public void CreateFactory(string factoryName){
            GameObject factoryGO = null;
            factoryGO = Instantiate(FactoryPrefab);
            Factory factory = Instantiate(Resources.Load("Factories/" + factoryName) as Factory);
            factory.SetGameManager(this);
            factoryGO.GetComponent<DisplayFactory>().SetDisplay(factory);
            factoryGO.transform.position = Input.mousePosition*screenScale;
            factoryGO.transform.SetParent(BoardGO.transform);
        }
    }
    
    public enum EffectType {
        Money,
        Funding,
        Pollution,
        Backing,
        Power,
        Draw,
        DrawHand,
        DrawRandom,
        CreateFactory,
        DiscardHand,
        DiscardRandom,
    }
    
    [Serializable]
    public class Effect {
        public EffectType effectType;
        public int amount;
        public string name;
    }
}
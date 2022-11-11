using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using handNameSpace;
using cardNameSpace;
using RP;
using factoryNameSpace;

namespace GMNameSpace {

    public class PlayCardEvent : UnityEvent<int> {}

    public class GameManager : MonoBehaviour {
        
        private Deck factoryDeck;
        private Deck dealsDeck;
        private Deck factoryDiscard;
        private Deck dealsDiscard;
        private Hand hand;
        private ResourcePanel resources;

        public GameObject GOfactoryDeck;
        public GameObject GOdealsDeck;
        public GameObject GOfactoryDiscard;
        public GameObject GOdealsDiscard;
        public GameObject GOhand;
        public GameObject GOBoard;
        public GameObject GOResources;

        private int turn = 0;
        private int year = 1;
        public int factoryDraw;
        public int dealsDraw;

        public int balance = 5;
        public int baseFunding = 3;
        private int funding;
        public int pollution = 0;
        public int maxPollution;
        public int backing;
        public int power;
        public int powerRequirement;

        public GameObject FactoryPrefab;

        public static PlayCardEvent tryToPlayCard;

        void Start() {
            factoryDeck = GOfactoryDeck.GetComponent<Deck>();
            dealsDeck = GOdealsDeck.GetComponent<Deck>();
            factoryDiscard = GOfactoryDiscard.GetComponent<Deck>();
            dealsDiscard = GOdealsDiscard.GetComponent<Deck>();
            hand = GOhand.GetComponent<Hand>();
            resources = GOResources.GetComponent<ResourcePanel>();

            tryToPlayCard = new PlayCardEvent();
            tryToPlayCard.AddListener(PlayCard);
            
            funding = baseFunding;
            resources.update_text(balance, funding, pollution, maxPollution, turn, year, powerRequirement, power, backing);

            for (int t = 0; t < 16; t++ ) {
                factoryDeck.add(
                    new Card(
                        "Factory 1",
                        "Lav en fabrik",
                        "Lav en fabrik",
                        new List<Tuple<Effect, int>>{Tuple.Create(Effect.Money, -1)},
                        new List<Tuple<Effect, int>>{Tuple.Create(Effect.CreateFactory, 1)},
                        Resources.Load<Sprite>("sprites/factory"),
                        CardType.FactoryType
                    )
                );
            }
            for (int t = 0; t < 16; t++ ) {
                dealsDeck.add(
                    new Card(
                        "Deal 1",
                        "lav en deal",
                        "lav en deal",
                        new List<Tuple<Effect, int>>{Tuple.Create(Effect.Power, -2)},
                        new List<Tuple<Effect, int>>{Tuple.Create(Effect.Money, 4)},
                        Resources.Load<Sprite>("sprites/factory"),
                        CardType.DealType
                    )   
                );
            }
            factoryDeck.shuffle();
            dealsDeck.shuffle();
            DrawHand();
        }

        void Update() {
            
            if (backing > 100) {
                backing = 100;
            }
            if (backing <= 0 || pollution >= maxPollution) {
                print("Du er bad, du tabte spillet");
            }
            resources.update_text(balance, funding, pollution, maxPollution, turn, year, powerRequirement, power, backing);
        }

        public void NextTurn() {
            filterToDiscard(hand.DiscardHand());

            if (turn == 3) {
                year += 1;
                ShuffleDiscardIntoDeck(CardType.FactoryType);
                ShuffleDiscardIntoDeck(CardType.DealType);
                powerRequirement += 3;
            }

            if (power < powerRequirement) {
                backing -= 20;
            }
            if (power >= powerRequirement) {
                backing += 5;
            }
            if (backing >= 90) {
                funding = 2*baseFunding;
            }
            if (backing <= 30) {
                funding = (int)(0.5f*(float)baseFunding);
            }
            
            
            turn = (turn+1)%4;
            balance += funding;
            power = 0;

            foreach (Transform factoryTransform in GOBoard.transform) {
                factoryTransform.gameObject.GetComponent<Factory>().Upkeep();
            }
            DrawHand();
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
                        Card drawnFactory = factoryDeck.drawCard();
                        hand.AddCard(drawnFactory);
                        break;
                    case CardType.DealType:
                        if (dealsDeck.size() == 0) {
                            if (dealsDiscard.size() == 0) {
                                Debug.Log("There are no more deal cards to draw");
                                return;
                            }
                            ShuffleDiscardIntoDeck(CardType.DealType);
                        }
                        Card drawnDeal = dealsDeck.drawCard();
                        hand.AddCard(drawnDeal);
                        break;
                }
            }
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

        public bool TryToPay(List<Tuple<Effect, int>> resources){
            foreach (Tuple<Effect, int> resource in resources) {
                if (!TryToPay(resource)) {
                    return false;
                }
            }
            return true;
        }

        public bool TryToPay(Tuple<Effect, int> resource) {
            switch (resource.Item1){
                case Effect.Money:
                    return (balance + resource.Item2 >= 0);
                case Effect.Funding:
                    return (baseFunding + resource.Item2 > 0);
                case Effect.Backing:
                    return (backing + resource.Item2 > 0);
                case Effect.Power:
                    return (power + resource.Item2 >= 0);
                case Effect.Pollution:
                    return (resource.Item2 + pollution < maxPollution);
            }
            return false;
        }

        public void PlayEffects(List<Tuple<Effect, int>> effects) {
            foreach (Tuple<Effect, int> effect in effects) {
                PlayEffect(effect);
            }
        }
        
        public void PlayEffect(Tuple<Effect, int> effect) {
            switch (effect.Item1){
                case Effect.Money:
                    balance += effect.Item2;
                    break;
                case Effect.Funding:
                    baseFunding += effect.Item2;
                    break;
                case Effect.Backing:
                    backing += effect.Item2;
                    break;
                case Effect.Power:
                    power += effect.Item2;
                    break;
                case Effect.Pollution:
                    pollution += effect.Item2;
                    break;
                case Effect.DrawDeal:
                    Draw(CardType.DealType, effect.Item2);
                    break;
                case Effect.DrawFactory:
                    Draw(CardType.DealType, effect.Item2);
                    break;
                case Effect.CreateFactory:
                    CreateFactory(effect.Item2);
                    break;
            }
        }

        public void CreateFactory(int factoryID){
            GameObject factory = null;
            // Factory factory
            factory = Instantiate(FactoryPrefab);
            factory.GetComponent<Factory>().Init("Coal Power Plant", Resources.Load<Sprite>("sprites/factory"), null, null, 
                Tuple.Create(Effect.Money, -2), new List<Tuple<Effect, int>>{Tuple.Create(Effect.Power, 4), Tuple.Create(Effect.Pollution, 4)},
                new List<Tuple<Effect, int>>{Tuple.Create(Effect.Pollution, 1)});

            factory.transform.position = Input.mousePosition;
            factory.transform.SetParent(GOBoard.transform);
        }
    }
    
    public enum Effect {
        Money,
        Funding,
        Pollution,
        Backing,
        Power,
        DrawFactory,
        DrawDeal,
        CreateFactory,
        DiscardHand,
    }

}
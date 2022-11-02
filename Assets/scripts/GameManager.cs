using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using handNameSpace;
using cardNameSpace;
using RP;

namespace GMNameSpace {

    public class CardPlayedEvent : UnityEvent<int> {}

    public class GameManager : MonoBehaviour {
        
        private Deck factoryDeck;
        private Deck dealsDeck;
        private Deck factoryDiscard;
        private Deck dealsDiscard;
        private Hand hand;
        private Board board;
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
        public int funding = 3;
        public int pollution = 0;
        public int maxPollution;

        public static CardPlayedEvent cardplayed;

        void Start() {
            factoryDeck = GOfactoryDeck.GetComponent<Deck>();
            dealsDeck = GOdealsDeck.GetComponent<Deck>();
            factoryDiscard = GOfactoryDiscard.GetComponent<Deck>();
            dealsDiscard = GOdealsDiscard.GetComponent<Deck>();
            hand = GOhand.GetComponent<Hand>();
            // board = GOBoard.GetComponent<Board>();
            board = null;
            resources = GOResources.GetComponent<ResourcePanel>();

            cardplayed = new CardPlayedEvent();
            cardplayed.AddListener(playCard);

            resources.update_text(balance, funding, pollution, maxPollution, turn, year);

            for (int t = 0; t < 16; t++ ) {
                factoryDeck.add(
                    new Card(
                        "Factory 1",
                        "Lav en fabrik",
                        "Lav en fabrik",
                        1,
                        Resources.Load<Sprite>("sprites/factory"),
                        CardType.FactoryType,
                        CostType.Money
                    )
                );
            }
            
            for (int t = 0; t < 16; t++ ) {
                dealsDeck.add(
                        new Card(
                        "Deal 1",
                        "lav en deal",
                        "lav en deal",
                        0,
                        Resources.Load<Sprite>("sprites/factory"),
                        CardType.DealType,
                        CostType.Pollution
                    )
                );
            }
            factoryDeck.shuffle();
            dealsDeck.shuffle();
            drawHand();
        }

        void Update() {
            resources.update_text(balance, funding, pollution, maxPollution, turn, year);
        }

        public void nextTurn() {
            filterToDiscard(hand.DiscardHand());

            if (turn == 3) {
                year += 1;
                dealsDeck.add(dealsDiscard);
                dealsDiscard.clear();
                dealsDeck.shuffle();
                factoryDeck.add(factoryDiscard);
                factoryDiscard.clear();
                factoryDeck.shuffle();
            }

            turn = (turn+1)%4;
            balance += funding;

            drawHand();
        }


        public void playCard(int cardIndex) {
            hand.Discard(cardIndex);
        }

        public void drawHand(){
            for (int t = 0; t < factoryDraw; t++ ) {
                Card drawncard = factoryDeck.drawCard();
                hand.AddCard(drawncard);
            }
            for (int t = 0; t < dealsDraw; t++ ) {
                Card drawncard = dealsDeck.drawCard();
                hand.AddCard(drawncard);
            }
            hand.createHand(GOhand);
        }

        public void filterToDiscard(List<Card> discard) {
            foreach (Card card in discard) {
                if (card.type == CardType.FactoryType) {
                    factoryDiscard.add(card);
                } else {
                    dealsDiscard.add(card);
                }
            }
        }
    }

}
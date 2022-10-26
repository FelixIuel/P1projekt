using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using handNameSpace;
using cardNameSpace;

namespace GMNameSpace {

    public class GameManager : MonoBehaviour {
            
        private Deck factoryDeck;
        private Deck dealsDeck;
        private Deck factoryDiscard;
        private Deck dealsDiscard;
        private Hand hand;

        public GameObject GOfactoryDeck;
        public GameObject GOdealsDeck;
        public GameObject GOfactoryDiscard;
        public GameObject GOdealsDiscard;
        public GameObject GOhand;

        // Start is called before the first frame update
        void Start() {
            factoryDeck = GOfactoryDeck.GetComponent<Deck>();
            dealsDeck = GOdealsDeck.GetComponent<Deck>();
            factoryDiscard = GOfactoryDiscard.GetComponent<Deck>();
            dealsDiscard = GOdealsDiscard.GetComponent<Deck>();
            hand = GOhand.GetComponent<Hand>();

            for (int t = 0; t < 4; t++ ) {
                factoryDeck.add(
                        new Card(
                        "Factory 1",
                        "Lav en fabrik",
                        "Lav en fabrik",
                        1,
                        Resources.Load<Sprite>("sprites/factory"),
                        CardType.DealType
                    )
                );
            }
            for (int t = 0; t < 4; t++ ) {
                factoryDeck.add(
                        new Card(
                        "Deal 1",
                        "lav en deal",
                        "lav en deal",
                        1,
                        Resources.Load<Sprite>("sprites/factory"),
                        CardType.DealType
                    )
                );
            }
            factoryDeck.shuffle();
            for (int t = 0; t < 4; t++ ) {
                Card drawncard = factoryDeck.drawCard();
                hand.AddCard(drawncard);
            }
            hand.createHand(GOhand);
        }

        // Update is called once per frame
        void Update() {
            
        }
    }

}
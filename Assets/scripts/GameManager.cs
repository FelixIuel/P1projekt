using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using handNameSpace;
using cardNameSpace;

namespace GMNameSpace {

    public class GameManager : MonoBehaviour {
            
        public Deck factoryDeck;
        public Deck dealsDeck;
        public Deck factoryDiscard;
        public Deck dealsDiscard;
        public Hand hand;

        // Start is called before the first frame update
        void Start() {
            // Card cardtoadd = ;
            for (int t = 0; t < 16; t++ ) {
                factoryDeck.add(
                        new Card(
                        "factory1",
                        "lav en fabrik",
                        "lav en fabrik",
                        1,
                        Resources.Load<Sprite>("sprites/factory"),
                        Resources.Load<Sprite>("sprites/Factory_card_1")
                    )
                );
            }
            Card drawncard = factoryDeck.drawCard();
            hand.AddCard(drawncard);
            hand.createHand();
        }

        // Update is called once per frame
        void Update() {
            
        }
    }

}
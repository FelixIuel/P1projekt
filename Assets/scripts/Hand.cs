using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;

namespace handNameSpace{
    public class Hand : MonoBehaviour {

        private List<Card> hand;
        private List<GameObject> handDisplay;
        private int maxHandSize;
        public GameObject factoryPrefab;
        public GameObject dealPrefab;
        public GameObject projectPrefab;
        // Update is called once per frame

        public Hand(){
            hand = new List<Card>();
            handDisplay = new List<GameObject>();
            maxHandSize = 7;
        }

        void Update() {
            
        }

        public List<Card> DiscardHand() {
            List<Card> temp = new List<Card>(hand);
            hand.Clear();
            handDisplay.Clear();
            return temp;
        }

        // Need to add so it removes the DisplayCard from HandDisplay As well
        public Card Discard(int cardIndex){
            Card cardToReturn = hand[cardIndex];
            hand.RemoveAt(cardIndex);
            return cardToReturn;
        }
        
        public void DestroyObject(int ID){
            Destroy(handDisplay[ID]);
            hand.RemoveAt(ID);
        }
        
        public void AddCard(Card card) {
            hand.Add(card);
        }

        public void createHand(GameObject parent) {
            print(hand.Count);
            for (int t = 0; t < hand.Count; t++ ) {
                GameObject objectToAdd = null;
                switch(hand[t].type){
                    case CardType.FactoryType:
                        objectToAdd = Instantiate(factoryPrefab);
                        break;
                    case CardType.DealType:
                        objectToAdd = Instantiate(dealPrefab);
                        break;
                    case CardType.ProjectType:
                        objectToAdd = Instantiate(projectPrefab);
                        break;
                }
                hand[t].SetCardInfo(t, objectToAdd);
                objectToAdd.transform.SetParent(parent.transform);
                
                // float x_pos = Mathf.Lerp(-835f,835f, t/hand.Count);
                float x_pos = -835f+(835*2/maxHandSize)*t+960;
                objectToAdd.transform.position = new Vector3(x_pos, -373+540.0f,0.0f);
                handDisplay.Add(objectToAdd);
            }
        }

        // public void onNewTurn(){
        // }

    }
}
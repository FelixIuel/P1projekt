using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;
using CardDrawing;

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
            foreach (GameObject card in handDisplay) {
                Destroy(card);
            }
            handDisplay.Clear();

            return temp;
        }

        public Card Discard(int cardID){
            int cardIndex = -1;
            for (int i = 0; i < handDisplay.Count; i++ ) {
                if (handDisplay[i].GetComponent<DisplayCard>().cardID == cardID) {
                    cardIndex = i;
                    break;
                }
            }
            if (cardIndex == -1) {
                return null;
            }

            Card cardToReturn = hand[cardIndex];
            hand.RemoveAt(cardIndex);
            DestroyObject(handDisplay[cardIndex]);
            handDisplay.RemoveAt(cardIndex);

            return cardToReturn;
        }
        
        private void DestroyObject(int cardIndex){
            Destroy(handDisplay[cardIndex]);
            hand.RemoveAt(cardIndex);
        }
        
        public void AddCard(Card card) {
            hand.Add(card);
        }

        public void createHand(GameObject parent) {
            for (int i = 0; i < hand.Count; i++ ) {
                GameObject objectToAdd = null;
                switch(hand[i].type){
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
                hand[i].SetCardInfo(i, objectToAdd);
                objectToAdd.transform.SetParent(parent.transform);
                
                // float x_pos = Mathf.Lerp(-835f,835f, t/hand.Count);
                float x_pos = -835f+(835*2/maxHandSize)*i+960;
                objectToAdd.transform.position = new Vector3(x_pos, -373+540.0f,0.0f);
                handDisplay.Add(objectToAdd);
            }
        }

        // public void onNewTurn(){
        // }

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;
using CardDrawing;

namespace handNameSpace{
    public class Hand : MonoBehaviour {

        public List<Card> hand;
        public List<GameObject> handDisplay;
        public GameObject factoryPrefab;
        public GameObject dealPrefab;
        public GameObject projectPrefab;
        
        public Hand(){
            hand = new List<Card>();
            handDisplay = new List<GameObject>();
        }

        public List<Card> DiscardHand() {
            List<Card> temp = new List<Card>(hand);
            ClearHand();
            hand.Clear();
            CreateHand();
            return temp;
        }

        public void ClearHand(){
            foreach (GameObject card in handDisplay) {
                Destroy(card);
            }
            handDisplay.Clear();
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
            // ClearHand();
            CreateHand();
            return cardToReturn;
        }
        
        private void DestroyObject(int cardIndex){
            Destroy(handDisplay[cardIndex]);
            handDisplay.RemoveAt(cardIndex);
            hand.RemoveAt(cardIndex);
        }
        
        public void AddCard(Card card) {
            hand.Add(card);
        }

        public void CreateHand() {
            ClearHand();
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
                objectToAdd.transform.SetParent(this.transform);
                handDisplay.Add(objectToAdd);
            }
        }
    }
}
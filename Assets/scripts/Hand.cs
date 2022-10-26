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
            maxHandSize = 8;
        }

        void Update() {
            
        }

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
            Debug.Log(hand.Count);
        }

        public void createHand(GameObject parent) {
            for (int t = 0; t < hand.Count; t++ ) {
                GameObject objectToAdd = null;
                switch(hand[t].type){
                    case CardType.FactoryType:
                        objectToAdd = hand[t].AddGameObject(t, factoryPrefab);
                        break;
                    case CardType.DealType:
                        objectToAdd = hand[t].AddGameObject(t, dealPrefab);
                        break;
                    case CardType.ProjectType:
                        objectToAdd = hand[t].AddGameObject(t, projectPrefab);
                        break;
                }
                
                objectToAdd.transform.SetParent(parent.transform);
                
                float x_pos = Mathf.Lerp(-835f,835f, t/10);
                Debug.Log(x_pos);
                objectToAdd.transform.position = new Vector3(x_pos, -373.0f,0.0f);
                handDisplay.Add(objectToAdd);
                Debug.Log(handDisplay.Count);
            }
        }

        // public void onNewTurn(){
        // }

    }
}
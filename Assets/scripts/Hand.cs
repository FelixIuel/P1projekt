using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;

namespace handNameSpace{
    public class Hand : MonoBehaviour {

        private List<Card> hand;
        private List<GameObject> handDisplay;
        public GameObject CardPrefab;
        // Update is called once per frame

        public void Start(){
            hand = new List<Card>();
            // handAsGameObjects = new List<GameObject>();
        }

        void Update() {
            
        }

        public Card Discard(int cardIndex){
            Card cardToReturn = hand[cardIndex];
            hand.RemoveAt(cardIndex);
            return cardToReturn;
        }

        // public GameObject CardsToGameObject() {
            
        // }
        
        public void DestroyObject(int ID){
            Destroy(handDisplay[ID]);
            hand.RemoveAt(ID);
        }
        
        public void AddCard(Card card) {
            hand.Add(card);
        }


        public void createHand() {
            for (int t = 0; t < hand.Count; t++ ) {
                handDisplay.Add((hand[t]).AddGameObject(t, CardPrefab));
            }
        }
        // public void createHand() {
        //     for (int t = 0; t < hand.Count; t++ ) {
        //         (hand[t]).AddGameObject(t, CardPrefab);
        //     }
        // }

        // public void onNewTurn(){
        // }

    }
}
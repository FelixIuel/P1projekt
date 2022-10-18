using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;

namespace handNameSpace{
    public class Hand : MonoBehaviour {

        private List<Card> hand;
        private List<GameObject> handAsGameObjects;

        // Update is called once per frame
        void Update() {

        }

        public Card Discard(int cardIndex){
            Card cardToReturn = hand[cardIndex];
            hand.RemoveAt(cardIndex);
            return cardToReturn;
        }

        // public GameObject CardToGameObject() {

        // }

        public void AddCard(Card card) {
            hand.Add(card);
        }

        public void createHand() {
            
        }

        // public void onNewTurn(){
        // }

    }
}
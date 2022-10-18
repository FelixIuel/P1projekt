using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour {

    private List<Card> hand;
    private List<GameObject> handAsGameObjects;

    // Update is called once per frame
    void Update() {

    }

    public card Discard(int cardIndex){
        Card cardToReturn = deck[cardIndex];
        deck.RemoveAt(cardIndex);
        return cardToReturn;
    }

    public GameObject CardToGameObject() {

    }

    public void AddCard(Card card) {
        hand.Add(card);
    }

    public createHand(){
        
    }

    // public void onNewTurn(){
    // }

}

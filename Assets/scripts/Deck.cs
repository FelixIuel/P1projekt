using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;

public class Deck : MonoBehaviour {
    
    private List<Card> deck;

    public Deck() {
        deck = new List<Card>();
    }

    public void shuffle() {
        //Vi bruger en knud shuffle https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        for (int i = 0; i < deck.Count; i++ ) {
            Card tmp = deck[i];
            int r = Random.Range(i, deck.Count);
            deck[i] = deck[r];
            deck[r] = tmp;
        }
    }
    
    public void clear() {
        deck.Clear();
    }
    public int size(){
        return deck.Count;
    }

    public void add(Deck deckToAdd) {
        deck.AddRange(deckToAdd.deck);
    }

    public void add(List<Card> deckToAdd) {
        deck.AddRange(deckToAdd);
    }
    
    public void add(Card cardToAdd) {
        deck.Add(cardToAdd);
    }
    
    public Card drawCard(){
        Card cardToReturn = null;
        // new Card();
        cardToReturn = deck[deck.Count-1];
        deck.RemoveAt(deck.Count-1);
        return cardToReturn;
    }

    public Card Discard(int cardIndex){
        Card cardToReturn = deck[cardIndex];
        deck.RemoveAt(cardIndex);
        return cardToReturn;
    }
}

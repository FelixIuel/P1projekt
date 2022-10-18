using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cardNameSpace;

public class Deck : MonoBehaviour {
    
    private List<Card> deck;

    public void shuffle(){
        //Vi bruger sådan en fræk knud shuffle https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle 
        for (int t = 0; t < deck.Count; t++ )
        {
            Card tmp = deck[t];
            int r = Random.Range(t, deck.Count);
            deck[t] = deck[r];
            deck[r] = tmp;
        }
    }
    
    public void add(List<Card> deckToAdd) {
        deck.AddRange(deckToAdd);
    }
    
    public void add(Card cardToAdd) {
        deck.Add(cardToAdd);
    }
    
    public Card drawCard(){
        Card cardToReturn = deck[deck.Count-1];
        deck.RemoveAt(deck.Count-1);
        return cardToReturn;
    }
    
}

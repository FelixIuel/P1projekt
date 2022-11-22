using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDrawing;
using GMNameSpace;

namespace cardNameSpace {
    public enum CardType {
        FactoryType,
        DealType,
        ProjectType
    }

    [CreateAssetMenu(fileName = "Cards/new Card", menuName = "Card")]
    [System.Serializable]
    public class Card : ScriptableObject {
        public string cardName;
        public string flavorText;
        private string effectText;
        [SerializeField]
        public List<Effect> cardCost;
        [SerializeField]
        public List<Effect> effects;
        public Sprite cardArt;
        public CardType type;

        public Card(string _cardName, string _flavorText, string _effectText, List<Effect> _cardCost, List<Effect> _cardEffect, Sprite _cardArt, CardType _type) {
            cardName = _cardName;
            flavorText = _flavorText;
            cardCost = _cardCost;
            effectText = _effectText;
            cardArt = _cardArt;
            type = _type;
            effects =_cardEffect;
        }
        
        public Card() {}
    }
}
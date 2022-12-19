using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CardDisplay;
using GMNameSpace;

namespace cardNameSpace {
    public enum CardType {
        FactoryType,
        DealType,
    }

    [CreateAssetMenu(fileName = "Cards/new Card", menuName = "Card/Default")]
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
        public Effect useCost;
        [SerializeField]
        public List<Effect> useOutput;
        [SerializeField]
        public List<Effect> upkeepOutput;
        public GameManager gM = null;

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

        void update() {
        }
    }
}
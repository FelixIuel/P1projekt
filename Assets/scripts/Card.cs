using System;
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

    public class Card {
        private string cardName;
        private string flavorText;
        private string effectText;
        public List<Tuple<Effect,int>> cardCost;
        public List<Tuple<Effect,int>> cardEffect;
        private Sprite cardArt;
        private Sprite backgroundArt;
        public CardType type;

        public Card(string _cardName, string _flavorText, string _effectText, List<Tuple<Effect,int>> _cardCost, List<Tuple<Effect,int>> _cardEffect, Sprite _cardArt, CardType _type) {
            cardName = _cardName;
            flavorText = _flavorText;
            cardCost = _cardCost;
            effectText = _effectText;
            cardArt = _cardArt;
            type = _type;
            cardEffect =_cardEffect;

            switch(_type){
                case CardType.FactoryType:
                    backgroundArt = Resources.Load<Sprite>("Factory_card_1");
                    break;
                case CardType.DealType:
                    backgroundArt = Resources.Load<Sprite>("Deal_card_1");
                    break;
                case CardType.ProjectType:
                    backgroundArt = Resources.Load<Sprite>("Deal_card_1");
                    break;
            }
        }
        
        public Card() {}
        
        public void SetCardInfo(int cardID, GameObject CardObject) {
            // int cost = 0;
            // foreach ((Effect resource, int _cost) in cardCost) {
            //     if (resource == Effect.Money) {
            //         cost = _cost;
            //     }
            // }
            CardObject.GetComponent<DisplayCard>().SetCard(
                cardID,
                cardName,
                flavorText,
                cardEffect,
                cardCost,
                cardArt,
                backgroundArt,
                type
            );
        }

    }
}
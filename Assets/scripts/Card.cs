using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using CardDrawing;

namespace cardNameSpace {

    public enum CardType {
        FactoryType,
        DealType,
        ProjectType
    }

    public enum CostType {
        Money,
        Pollution,
        Funding,
    }

    public class Card {
        private string cardName;
        private string flavorText;
        private string effectText;
        private int cardCost;
        private Sprite cardArt;
        private Sprite backgroundArt;
        public CardType type;
        public CostType costType;

        public Card(string _cardName, string _flavorText, string _effectText, int _cardCost, Sprite _cardArt, CardType _type, CostType _costType) {
            cardName = _cardName;
            flavorText = _flavorText;
            cardCost = _cardCost;
            effectText = _effectText;
            cardArt = _cardArt;
            type = _type;
            costType = _costType;

            switch(_type){
                case CardType.FactoryType:
                    backgroundArt = Resources.Load<Sprite>("sprites/Factory_card_1");
                    break;
                case CardType.DealType:
                    backgroundArt = Resources.Load<Sprite>("sprites/Deal_card_1");
                    break;
                case CardType.ProjectType:
                    backgroundArt = Resources.Load<Sprite>("sprites/Deal_card_1");
                    break;
            }
        }
        
        public Card() {}
        
        public void SetCardInfo(int cardID, GameObject CardObject) {
            CardObject.GetComponent<DisplayCard>().SetCard(
                cardID,
                cardName,
                flavorText,
                effectText,
                cardCost,
                cardArt,
                backgroundArt,
                type
            );
        }

    }
}
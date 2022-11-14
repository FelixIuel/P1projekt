using UnityEngine;
using UnityEngine.UI;
using cardNameSpace;
using TMPro;
using GMNameSpace;
using System;

namespace CardDrawing {
    public class DisplayCard : MonoBehaviour {
        
        public TextMeshProUGUI cardName;
        public TextMeshProUGUI flavorText;
        public TextMeshProUGUI effectText;
        public TextMeshProUGUI cardCost;
        public Image cardArt;
        public Image backgroundArt;
        public int cardID;
        public bool add = false;

        public void SetCard(int _cardID, string _cardName, string _flavorText, string _effectText, int _cardCost, Sprite _cardArt, Sprite _backgroundArt, CardType _type) {
            cardID = _cardID;
            cardName.text = " " + _cardName;
            flavorText.text = " " + _flavorText;
            if (_type == CardType.FactoryType) {
                cardCost.text = " " + -1*_cardCost;
            }
            
            effectText.text = " " +_effectText;
            cardArt.sprite = _cardArt;
            backgroundArt.sprite = _backgroundArt;
            

        }

        public string AddText(String CurrentText, Effect effect, int amount) {
            string returnText = CurrentText;
            switch (effect) {
                    case Effect.Money:
                        returnText = Add(returnText);
                        returnText += amount + " <sprite name="+"Money_symbol"+">";
                        break;
                    case Effect.Funding:
                        returnText = Add(returnText);
                        returnText += amount + " funding.";
                        break;
                    case Effect.Backing:
                        returnText = Add(returnText);
                        returnText += amount + " backing.";
                        break;
                    case Effect.Power:
                        returnText = Add(returnText);
                        returnText += amount + " <sprite name="+"Energy_symbol"+">";
                        break;
                    case Effect.Pollution:
                        returnText = Add(returnText);
                        returnText += amount + " <sprite name="+"Pollution_symbol"+">";
                        break;
                    case Effect.DrawDeal:
                        returnText = "Draw" + amount + "deal cards. " + returnText;
                        break;
                    case Effect.DrawFactory:
                        returnText = "Draw" + amount + "deal cards. " + returnText;
                        break;
                    case Effect.CreateFactory:
                        // no upkeep/factories creates factories
                        break;
                }
                return returnText;
            }

        public string Add(string text){
            if (!add) {
                text += " add ";
                add = true;
                return text;
            }
            return text;
        }
    }
}
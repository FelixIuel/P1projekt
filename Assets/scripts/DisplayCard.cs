using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
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

        public void SetCard(int _cardID, string _cardName, string _flavorText, List<Tuple<Effect,int>> _effects, 
            List<Tuple<Effect,int>> _cardCost, Sprite _cardArt, Sprite _backgroundArt, CardType _type) {
            cardID = _cardID;
            cardArt.sprite = _cardArt;
            backgroundArt.sprite = _backgroundArt;

            cardName.text = " " + _cardName;
            flavorText.text = " " + _flavorText;
            if (_type == CardType.FactoryType) {
                int cost = 0;
                foreach ((Effect resource, int _cost) in _cardCost) {
                if (resource == Effect.Money) {
                    cost = _cost;
                    cardCost.text = " " + -1*_cost;
                }
            }
            }
            
            
            effectText.text = " ";
            
            add = false;
            foreach(Tuple<Effect,int> effect in _effects) {
                // effectText.text += ", ";
                effectText.text = AddText(effectText.text, effect.Item1, effect.Item2);
            }
            
            // effectText.text = "<sprite name="+"Use_symbol"+">" + useText.text;

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
                        if (amount >= 0) {
                            returnText += amount + " <sprite name="+"Backing_symbol_happy"+">";
                        } else {
                            returnText += amount + " <sprite name="+"Backing_symbol_mad"+">";
                        }
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
                        returnText += "Create a" + "Indsæt navn";
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
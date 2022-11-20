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

        public void SetCard(int _cardID, string _cardName, string _flavorText, List<Effect> _effects, 
            List<Effect> _cardCost, Sprite _cardArt, CardType _type) {
            cardID = _cardID;
            cardArt.sprite = _cardArt;
            switch(_type){
                case CardType.FactoryType:
                    backgroundArt.sprite = Resources.Load<Sprite>("Factory_card_1");
                    break;
                case CardType.DealType:
                    backgroundArt.sprite = Resources.Load<Sprite>("Deal_card_1");
                    break;
                case CardType.ProjectType:
                    backgroundArt.sprite = Resources.Load<Sprite>("Deal_card_1");
                    break;
            }
            
            cardName.text = " " + _cardName;
            flavorText.text = " " + _flavorText;
            if (_type == CardType.FactoryType) {
                int cost = 0;
                foreach (Effect effect in _cardCost) {
                    if (effect.effectType == EffectType.Money) {
                        cost = effect.amount;
                        cardCost.text = " " + -1*effect.amount;
                    }
                }
            }
            
            effectText.text = " ";
            add = false;
            foreach(Effect effect in _effects) {
                effectText.text = AddText(effectText.text, effect);
            } 
        }

        public string AddText(String CurrentText, Effect effect) {
            string returnText = CurrentText;
            switch (effect.effectType) {
                    case EffectType.Money:
                        returnText = Add(returnText);
                        returnText += effect.amount + " <sprite name=Money_symbol>";
                        break;
                    case EffectType.Funding:
                        returnText = Add(returnText);
                        returnText += effect.amount + " funding.";
                        break;
                    case EffectType.Backing:
                        returnText = Add(returnText);
                        if (effect.amount >= 0) {
                            returnText += effect.amount + " <sprite name=Backing_symbol_happy>";
                        } else {
                            returnText += effect.amount + " <sprite name=Backing_symbol_mad>";
                        }
                        break;
                    case EffectType.Power:
                        returnText = Add(returnText);
                        returnText += effect.amount + " <sprite name=Energy_symbol>";
                        break;
                    case EffectType.Pollution:
                        returnText = Add(returnText);
                        returnText += effect.amount + " <sprite name=Pollution_symbol>";
                        break;
                    case EffectType.Draw:
                        returnText = "Draw " + effect.amount + " " + effect.name + " card(s). " + returnText;
                        break;
                    case EffectType.CreateFactory:
                        returnText += "Create a " + effect.name;
                        // no upkeep/factories creates factories
                        break;
                    case EffectType.DrawRandom:
                        returnText = "Draw " + effect.amount + " card(s)" + " from random deck(s). " + returnText;
                        break;
                    case EffectType.DrawHand:
                        returnText = "Draw a full hand. " + returnText;
                        break;
                    case EffectType.DiscardHand:
                        returnText = "Discard your hand. " + returnText;
                        break;
                    case EffectType.DiscardRandom:
                        returnText = "Discard " + effect.amount + " card(s) at random. " + returnText;
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
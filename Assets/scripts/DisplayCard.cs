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
        private Card card;
        

        public void SetCard(int _cardID, Card _card) {
            cardID = _cardID;
            card = _card;
            SetCardDisplay();
        }

        public Card GetCard(){
            return card;
        }

        public void SetCardDisplay() {
            cardArt.sprite = card.cardArt;
            switch(card.type){
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
            
            cardName.text = " " + card.cardName;
            flavorText.text = " " + card.flavorText;
            if (card.type == CardType.FactoryType) {
                int cost = 0;
                foreach (Effect effect in card.cardCost) {
                    if (effect.effectType == EffectType.Money) {
                        cost = effect.amount;
                        cardCost.text = " " + -1*effect.amount;
                    }
                }
            }
            
            effectText.text = " ";
            add = true;
            if (card.type == CardType.DealType) {
                foreach (Effect effect in card.cardCost) {
                    if (effect.effectType == EffectType.Money || effect.effectType == EffectType.Power) {
                        effectText.text += "Pay ";
                    }
                    effectText.text = AddText(effectText.text, effect);
                }
            }

            add = false;
            foreach(Effect effect in card.effects) {
                effectText.text = AddText(effectText.text, effect);
            } 
        }

        public string AddText(String CurrentText, Effect effect) {
            string returnText = CurrentText;
            switch (effect.effectType) {
                case EffectType.Money:
                    returnText = Add(returnText);
                    returnText += effect.amount + " <sprite name=Money_symbol> ";
                    break;
                case EffectType.Funding:
                    returnText = Add(returnText);
                    returnText += effect.amount + " funding. ";
                    break;
                case EffectType.Backing:
                    returnText = Add(returnText);
                    if (effect.amount >= 0) {
                        returnText += effect.amount + " <sprite name=Backing_symbol_happy> ";
                    } else {
                        returnText += effect.amount + " <sprite name=Backing_symbol_mad> ";
                    }
                    break;
                case EffectType.Power:
                    returnText = Add(returnText);
                    returnText += effect.amount + " <sprite name=Energy_symbol> ";
                    break;
                case EffectType.Pollution:
                    returnText = Add(returnText);
                    returnText += effect.amount + " <sprite name=Pollution_symbol> ";
                    break;
                case EffectType.Draw:
                    if (effect.amount <= 1) {
                        returnText += "Draw " + effect.amount + " " + effect.name + " card. ";                            
                    } else {
                        returnText += "Draw " + effect.amount + " " + effect.name + " cards. ";
                    }
                    break;
                case EffectType.CreateFactory:
                    returnText += "Create a " + effect.name + ". ";
                    break;
                case EffectType.DrawRandom:
                    if (effect.amount <= 1) {
                        returnText += "Draw " + effect.amount + " card" + " from a random deck. ";                            
                    } else {
                        returnText += "Draw " + effect.amount + " cards" + " from random decks. ";
                    }
                    break;
                case EffectType.DrawHand:
                    returnText += "Draw a full hand. ";
                    break;
                case EffectType.DiscardHand:
                    returnText = "Discard your hand. " + returnText;
                    break;
                case EffectType.DiscardRandom:
                    if (effect.amount <= 1) {
                        returnText = "Discard " + effect.amount + " card at random. " + returnText;                            
                    } else {
                        returnText = "Discard " + effect.amount + " cards at random. " + returnText;
                    }
                    break;
                case EffectType.AddCard:
                    if (effect.amount <= 1) {
                        returnText += "Add " + effect.amount + " " + effect.name + " card to your deck. ";
                    } else {
                        returnText += "Add " + effect.amount + " " + effect.name + " cards to your deck. ";
                    }
                    break;
            }
            return returnText;
        }

        public string Add(string text){
            if (!add) {
                text += " Add ";
                add = true;
                return text;
            }
            return text;
        }
    }
}
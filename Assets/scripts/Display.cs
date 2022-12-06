using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using cardNameSpace;
using TMPro;
using GMNameSpace;
using System;

namespace DisplayNS {
    public class Display : MonoBehaviour {

        public TextMeshProUGUI cardName;
        public Image backgroundArt;
        public Image cardArt;
        public GameObject CanPlay;
        // we need a better name for this one
        //checks if we should add the text "add" (for adding resources like money) since we only want it to be there once.
        public bool add = false;


        // Update is called once per frame
        void Update(){}
        
        public virtual void SetDisplay() {}

        public virtual string AddText(String CurrentText, Effect effect) {
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
                case EffectType.Exhaust:
                    returnText += "Exhaust this card. ";
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
                case EffectType.ExhaustRandom:
                    if (effect.amount <= 1) {
                        returnText += "Exhaust " + effect.amount + " card from your hand. ";
                    } else {
                        returnText += "Exhaust " + effect.amount + " cards from you hand. ";
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

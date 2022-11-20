using System;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using GMNameSpace;
using TMPro;
using UnityEngine.EventSystems;

namespace FactoryDrawing {
    public class DisplayFactory : MonoBehaviour, IPointerClickHandler {
        
        public TextMeshProUGUI factoryName;
        public Image factoryArt;
        public TextMeshProUGUI useText;
        public TextMeshProUGUI upkeepText;
        public Factory factory;
        // we need a better name for this one
        //checks if we should add the text "add" (for adding resources like money) since we only want it to be there once.
        private bool add = false;

        public void SetDisplay(Factory _factory) {
            factory = _factory;
            SetDisplay();
        }

        public void SetDisplay() {
            factoryName.text = "" + factory.factoryName;
            factoryArt.sprite = factory.factoryArt;
            upkeepText.text = "";
            useText.text = "";
            if (factory.useCost.effectType == EffectType.Money || factory.useCost.effectType == EffectType.Power) {
                useText.text = "Pay ";
            }
            add = true;
            useText.text = AddText(useText.text, factory.useCost);
            add = false; //spaghetti kode find en bedre løsning.

            
            if (factory.upkeepOutput.Count != 0) {
                foreach(Effect effect in factory.upkeepOutput) {
                    upkeepText.text += ", ";
                    upkeepText.text = AddText(upkeepText.text, effect);
                }
                upkeepText.text += ".";
                upkeepText.text = "on" + "<sprite name="+"Upkeep_symbol"+">" + upkeepText.text;
            }
            
            add = false;
            
            foreach(Effect effect in factory.useOutput) {
                useText.text += ", ";
                useText.text = AddText(useText.text, effect);
            }
            
            useText.text = "<sprite name="+"Use_symbol"+">" + useText.text;
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

        public void Update() {
            this.GetComponent<Animator>().SetBool("Used", factory.Used);
        }

        public void OnPointerClick(PointerEventData pointerEventData){
            if (pointerEventData.button == PointerEventData.InputButton.Right && !factory.Used && factory.UseEffect()) {
                factory.Used = true;
            }
        }
    }
}   
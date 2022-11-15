using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using GMNameSpace;
using TMPro;

namespace FactoryDrawing {
    public class DisplayFactory : MonoBehaviour {
        
        public TextMeshProUGUI factoryName;
        public Image factoryArt;
        public TextMeshProUGUI useText;
        public TextMeshProUGUI upkeepText;
        // we need a better name for this one
        //checks if we should add the text "add" (for adding resources like money) since we only want it to be there once.
        private bool add = false;

        public void SetDisplay(string _factoryName, Sprite _factoryArt, Tuple<Effect,int> _useCost, List<Tuple<Effect,int>> _useOutput, List<Tuple<Effect,int>> _upkeepOutput) {
            factoryName.text = "" + _factoryName;
            factoryArt.sprite = _factoryArt;
            upkeepText.text = "";
            useText.text = "Pay ";
            add = true;
            useText.text = AddText(useText.text, _useCost.Item1, _useCost.Item2);
            add = false; //spaghetti kode find en bedre løsning.
            if (_upkeepOutput.Count != 0) {
                foreach(Tuple<Effect,int> effect in _upkeepOutput) {
                    upkeepText.text += ", ";
                    upkeepText.text = AddText(upkeepText.text, effect.Item1, effect.Item2);
                }
                upkeepText.text += ".";
                upkeepText.text = "on" + "<sprite name="+"Upkeep_symbol"+">" + upkeepText.text;
            }
            add = false;
            foreach(Tuple<Effect,int> effect in _useOutput) {
                useText.text += ", ";
                useText.text = AddText(useText.text, effect.Item1, effect.Item2);
            }
            
            useText.text = "<sprite name="+"Use_symbol"+">" + useText.text;
        }

        public string AddText(String CurrentText, Effect effect, int amount) {
            string returnText = CurrentText;
            switch (effect){
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
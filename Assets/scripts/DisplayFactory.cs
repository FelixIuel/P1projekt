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
        public void SetDisplay(string _factoryName, Sprite _factoryArt, Tuple<Effect,int> _useCost, List<Tuple<Effect,int>> useOutput, List<Tuple<Effect,int>> _upkeepOutput) {
            factoryName.text = " " + _factoryName;
            factoryArt.sprite = _factoryArt;
            useText.text = " ";
            upkeepText.text = "<sprite name="+"Energy_symbol"+">";
        }



    } 
}   
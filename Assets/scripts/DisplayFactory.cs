using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using cardNameSpace;
using TMPro;

namespace FactoryDrawing {
    public class DisplayFactory : MonoBehaviour {
        
        public TextMeshProUGUI factoryName;
        public Image factoryArt;
        
        public void SetCard(string _factoryName, Sprite _factoryArt) {
            factoryName.text = " " + _factoryName;
            factoryArt.sprite = _factoryArt;
        }
    }
}
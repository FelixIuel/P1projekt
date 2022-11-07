using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using cardNameSpace;
using TMPro;

namespace FactoryDrawing {
    public class DisplayFactory : MonoBehaviour {
        
        public TextMeshProUGUI cardName;
        public Image cardArt;
        public Image backgroundArt;
        public int cardID;
        
        public void SetCard(int _cardID, string _cardName, Sprite _cardArt, Sprite _backgroundArt) {
            cardID = _cardID;
            cardName.text = " " + _cardName;
            cardArt.sprite = _cardArt;
            backgroundArt.sprite = _backgroundArt;
        }
    }
}
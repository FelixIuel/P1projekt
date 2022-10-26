using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using cardNameSpace;
using TMPro;

namespace CardDrawing {
    public class DisplayCard : MonoBehaviour {
        
        public TextMeshProUGUI cardName;
        public TextMeshProUGUI flavorText;
        public TextMeshProUGUI effectText;
        public TextMeshProUGUI cardCost;
        public Image cardArt;
        public Image backgroundArt;
        public int cardID;
        
        public void SetCard(int _cardID, string _cardName, string _flavorText, string _effectText, int _cardCost, Sprite _cardArt, Sprite _backgroundArt) {
            cardID = _cardID;
            cardName.text = " " + _cardName;
            flavorText.text = " " + _flavorText;
            cardCost.text = " " + _cardCost;
            effectText.text = " " +_effectText;
            cardArt.sprite = _cardArt;
            backgroundArt.sprite = _backgroundArt;
        }
    }
}
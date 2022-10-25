using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using CardDrawing;

namespace cardNameSpace {

    public enum CardType {
        FactoryTypel,
        DealType,
        ProjectType
    }
    public class Card : MonoBehaviour {
        
        private string cardName;
        private string flavorText;
        private string effectText;
        private int cardCost;
        private Sprite cardArt;
        private Sprite backgroundArt;
        private CardType type;
        // public GameObject asGameObject;

        public Card(string _cardName, string _flavorText, string _effectText, int _cardCost, Sprite _cardArt, Sprite _backgroundArt) {
            cardName = _cardName;
            flavorText = _flavorText;
            cardCost = _cardCost;
            cardArt = _cardArt;
            effectText = _effectText;
            backgroundArt = _backgroundArt;
        }

        // Update is called once per frame
        void Update() {
            
        }
        
        public GameObject AddGameObject(int cardID, GameObject Template) {
            GameObject objectToAdd = Instantiate(Template);
                (objectToAdd.GetComponent<DisplayCard>()).SetCard(
                    cardID,
                    cardName,
                    flavorText,
                    effectText,
                    cardCost,
                    cardArt,
                    backgroundArt
                );
            return objectToAdd;
            // asGameObject = objectToAdd;
        }

        // public void DestroyObject(){
        //     Destroy(asGameObject);
        //     asGameObject = null;
        // }

        public void CardEffect() {
            
        } 
    }



    // public class FactoryCard : Card {
    //     public Card(string _cardName, string _flavorText, string _effectText, int _cardCost, Image _cardArt, Image _backgroundArt, CardType _type) {
    //         cardName = _cardName;
    //         flavorText = _flavorText;
    //         cardCost = _cardCost;
    //         cardArt = _cardArt;
    //         effectText = _effectText;
    //         backgroundArt = _backgroundArt;
    //         type = _type;
    //     }
    //     public override Factory CardEffect();
    // }

}
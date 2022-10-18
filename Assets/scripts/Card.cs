using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace cardNameSpace {
    public abstract class Card : MonoBehaviour {
        
        public string cardName;
        public string flavourText;
        public string effectText;
        public int cardCost;
        public Image cardArt;
        public GameObject AsGameObject;

        // Start is called before the first frame update
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {
            
        }
        
        public void AddGameObject() {
            GameObject objectToAdd = new GameObject();
            objectToAdd.name = cardName;
            // objectToAdd.AddComponent
            
            AsGameObject = objectToAdd;
        }


        public abstract void CardEffect();   
    }

}
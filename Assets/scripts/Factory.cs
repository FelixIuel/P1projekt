using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace factoryNameSpace {
    public class Factory : MonoBehaviour {
        
        private string factoryName;
        private string flavorText;
        private string effectText;
        private Image art;
        private Image backgroundArt;
        public GameObject asGameObject;

        public Factory(string _factoryName, string _flavorText, string _effectText, int _cardCost, Image _factoryArt, Image _backgroundArt) {
            factoryName = _factoryName;
            flavorText = _flavorText;
            art = _factoryArt;
            effectText = _effectText;
            backgroundArt = _backgroundArt;
            GameObject objectToAdd = new GameObject();
            objectToAdd.name = factoryName;

            asGameObject = objectToAdd;
        }
        
        // Start is called before the first frame update
        void Start() {
            
        }

        // Update is called once per frame
        void Update() {
            
        }


        public void ActivateEffect() {
            
        }
    }

}
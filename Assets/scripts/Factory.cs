using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace factoryNameSpace {
    public class Factory : MonoBehaviour {
        
        private string factoryName;
        private string effectText;
        private Sprite cardArt;
        private Sprite backgroundArt;
        // private Image art;
        // private Image backgroundArt;

        public Factory(string _factoryName, Sprite _factoryArt, Sprite _backgroundArt) {
            factoryName = _factoryName;
            cardArt = _factoryArt;
            backgroundArt = _backgroundArt;
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
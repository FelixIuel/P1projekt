using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GMNameSpace;
using FactoryDrawing;
using UnityEngine.EventSystems;

namespace factoryNameSpace {
    public class Factory : MonoBehaviour, IPointerClickHandler {
        
        private string factoryName;
        private Sprite factoryArt;
        private Sprite useSprite;
        private Sprite upkeepSprite;
        private (Resource, int) useCost;
        private List<(Resource,int)> useOutput;
        private List<(Resource, int)> upkeepOutput;

        private bool Used;
        
        public Factory(string _factoryName, Sprite _factoryArt, Sprite _useSprite, Sprite _upkeepSprite, 
            (Resource, int) _useCost, List<(Resource,int)> _useOutput, List<(Resource, int)> _upkeepOutput) {
            
            factoryName = _factoryName;
            factoryArt = _factoryArt;
            useSprite = _useSprite;
            upkeepSprite = _upkeepSprite;
            useCost = _useCost;
            useOutput = _useOutput;
            upkeepOutput = _upkeepOutput;
        }
        
        // Start is called before the first frame update
        void Start() {
            
        }
        
        public void OnPointerClick(PointerEventData pointerEventData){
            if (pointerEventData.button == PointerEventData.InputButton.Right) {
                Used = !Used;
                this.GetComponent<Animator>().SetBool("Used", Used);
            }
        }

        public void Upkeep() {
            Used = false;
        }

        public void UseEffect() {
            
        }
        public void SetCardInfo() {
            this.GetComponent<DisplayFactory>().SetCard(
                factoryName,
                factoryArt
            );
        }
    }

}
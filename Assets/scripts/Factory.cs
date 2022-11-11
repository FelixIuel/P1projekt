using System;
using System.Collections.Generic;
using UnityEngine;
using GMNameSpace;
using FactoryDrawing;
using UnityEngine.EventSystems;

namespace factoryNameSpace {
    public class Factory : MonoBehaviour, IPointerClickHandler {
        private string factoryName;
        private Sprite factoryArt;
        private Sprite useSprite;
        private Sprite upkeepSprite;
        private Tuple<Effect,int> useCost;
        private List<Tuple<Effect,int>> useOutput;
        private List<Tuple<Effect,int>> upkeepOutput;
        private GameManager gM;
        private bool Used;
        
        public void Init(string _factoryName, Sprite _factoryArt, Sprite _useSprite, Sprite _upkeepSprite, 
            Tuple<Effect,int> _useCost, List<Tuple<Effect,int>> _useOutput, List<Tuple<Effect,int>> _upkeepOutput) {
            
            factoryName = _factoryName;
            factoryArt = _factoryArt;
            useSprite = _useSprite;
            upkeepSprite = _upkeepSprite;
            useCost = _useCost;
            useOutput = _useOutput;
            upkeepOutput = _upkeepOutput;

            Used = false;
            gM = GameObject.Find("GameHandler").GetComponent<GameManager>();

        }

        void Update() {
            this.GetComponent<Animator>().SetBool("Used", Used);
        }

        // Start is called before the first frame update
        void Start() {
            Used = false;
            gM = GameObject.Find("GameHandler").GetComponent<GameManager>();
        }
        
        public void OnPointerClick(PointerEventData pointerEventData){
            if (pointerEventData.button == PointerEventData.InputButton.Right && !Used && UseEffect()) {
                Used = true;
            }
        }

        public void Upkeep() {
            Used = false;
            if (upkeepOutput.Count != 0){
                gM.PlayEffects(upkeepOutput);
            }
        }

        public bool UseEffect() {
            if(gM.TryToPay(useCost)){
                gM.PlayEffect(useCost);
                gM.PlayEffects(useOutput);
                return true;
            }
            return false;
        }
        public void SetCardInfo() {
            this.GetComponent<DisplayFactory>().SetCard(
                factoryName,
                factoryArt
            );
        }
    }

}
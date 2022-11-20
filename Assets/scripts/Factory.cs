using System;
using System.Collections.Generic;
using UnityEngine;
using GMNameSpace;

namespace factoryNameSpace {

    [CreateAssetMenu(fileName = "Factories/New Factory", menuName = "Factory")]
    [Serializable]
    public class Factory : ScriptableObject{
        public string factoryName;
        public Sprite factoryArt;
        public Effect useCost;
        [SerializeField]
        public List<Effect> useOutput;
        [SerializeField]
        public List<Effect> upkeepOutput;
        private GameManager gM;
        public bool Used = false;
        
        public void SetGameManager(GameManager _gM) {
            gM = _gM;
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
    }

}
using System;
using System.Collections.Generic;
using UnityEngine;
using GMNameSpace;
using cardNameSpace;

namespace factoryNameSpace {

    // [CreateAssetMenu(fileName = "Factories/New Factory", menuName = "Factory")]
    // [Serializable]
    public class Factory {
        public string factoryName;
        public Sprite factoryArt;
        public Effect useCost;
        [SerializeField]
        public List<Effect> useOutput;
        [SerializeField]
        public List<Effect> upkeepOutput;
        public GameManager gM;
        public bool Used = false;
        public Card baseCard;
        

        public Factory(Card _baseCard, string _name, GameManager _gM){
            factoryName = _name;
            factoryArt = _baseCard.cardArt;
            useCost = _baseCard.useCost;
            useOutput = _baseCard.useOutput;
            upkeepOutput = _baseCard.upkeepOutput;
            baseCard = _baseCard;
            gM = _gM;
        }

        public void SetGameManager(GameManager _gM) {
            gM = _gM;
        }

        public void Upkeep() {
            Used = false;
            if (upkeepOutput.Count != 0){
                gM.PlayEffects(upkeepOutput, null);
            }
        }

        public bool UseEffect() {
            if(gM.TryToPay(useCost)){
                gM.PlayEffect(useCost, null);
                gM.PlayEffects(useOutput, null);
                return true;
            }
            return false;
        }
    }

}
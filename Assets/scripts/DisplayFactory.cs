using System;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using GMNameSpace;
using TMPro;
using UnityEngine.EventSystems;

namespace FactoryDisplay {
    public class DisplayFactory : DisplayNS.Display, IPointerClickHandler {
        
        public TextMeshProUGUI factoryName;
        public Image factoryArt;
        public TextMeshProUGUI useText;
        public TextMeshProUGUI upkeepText;
        public Factory factory;

        public void SetFactory(Factory _factory) {
            factory = _factory;
            this.GetComponent<HoverManager>().SetCard(factory.baseCard);
            SetDisplay();
        }

        public override void SetDisplay() {
            factoryName.text = "" + factory.factoryName;
            factoryArt.sprite = factory.factoryArt;
            upkeepText.text = "";
            useText.text = "";
            if (factory.useCost.effectType == EffectType.Money || factory.useCost.effectType == EffectType.Power) {
                useText.text = "Pay ";
            }
            add = true;
            useText.text = AddText(useText.text, factory.useCost);
            add = false;
            if (factory.upkeepOutput.Count != 0) {
                foreach(Effect effect in factory.upkeepOutput) {
                    upkeepText.text = AddText(upkeepText.text, effect);
                }
                upkeepText.text += ".";
                upkeepText.text = "On " + "<sprite name="+"Upkeep_symbol"+"> : " + upkeepText.text;
            }
             
            add = false;
            
            foreach(Effect effect in factory.useOutput) {
                useText.text = AddText(useText.text, effect);
            }
            if (factory.useOutput.Count > 0) {
                useText.text = "On " + "<sprite name="+"Use_symbol"+"> : " + useText.text;
            }
        }

        public void Update() {
            if (factory != null){
                this.GetComponent<Animator>().SetBool("Used", factory.Used);
                CanPlay.SetActive(factory.gM.TryToPay(factory.useCost) && !factory.Used);
            }
        }

        public void OnPointerClick(PointerEventData pointerEventData){
            if (pointerEventData.button == PointerEventData.InputButton.Right && !factory.Used && factory.UseEffect()) {
                factory.Used = true;
            }
        }

        public void OnBecameInvisible() {
            this.gameObject.GetComponent<Drag>().CancelDrag();
        }
    }
}   
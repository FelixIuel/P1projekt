using UnityEngine;
using cardNameSpace;
using TMPro;
using GMNameSpace;

namespace CardDisplay {
    public class DisplayCard : DisplayNS.Display {

        public TextMeshProUGUI flavorText;
        public TextMeshProUGUI effectText;
        public TextMeshProUGUI cardCost;
        private Card card;
        public bool toggleCanPlay = true;
        public GameManager gM;
        
        public void SetCard(Card _card) {
            card = _card;
            card.gM = GameManager.Instance;
            SetDisplay();
        }
        
        public void ToggleCanPlay(bool toggle){
            toggleCanPlay = toggle;
        }
        public Card GetCard(){
            return card;
        }

        public override void SetDisplay() {
            cardArt.sprite = card.cardArt;
            switch(card.type){
                case CardType.FactoryType:
                    backgroundArt.sprite = Resources.Load<Sprite>("Factory_card_1");
                    break;
                case CardType.DealType:
                    backgroundArt.sprite = Resources.Load<Sprite>("Deal_card_1");
                    break;
            }
            
            cardName.text = " " + card.cardName;
            flavorText.text = " " + card.flavorText;
            
            if (card.type == CardType.DealType || card.type == CardType.FactoryType && card.effects.Count > 1){
                effectText.text = "On Play: ";
            } else {
                effectText.text = "";
            }

            add = true;
            if (card.type == CardType.DealType) {
                foreach (Effect effect in card.cardCost) {
                    if (effect.effectType == EffectType.Money || effect.effectType == EffectType.Power) {
                        effectText.text += "Pay ";
                    }
                    effectText.text = AddText(effectText.text, effect);
                }
                effectText.text += "<br>";
            }

            add = false;
            if (card.type == CardType.FactoryType) {
                int cost = 0;
                foreach (Effect effect in card.cardCost) {
                    if (effect.effectType == EffectType.Money) {
                        cost = effect.amount;
                        cardCost.text = " " + -1*effect.amount;
                    }
                }
            }

            foreach(Effect effect in card.effects) {
                effectText.text = AddText(effectText.text, effect);
            }

            // for adding FactoryEffect Text to cards
            if (card.type == CardType.FactoryType) {
                string upkeepText = "";
                string useText = "";
                if (card.useCost.effectType == EffectType.Money || card.useCost.effectType == EffectType.Power) {
                    useText = "Pay ";
                }
                if (card.useOutput.Count > 0) {
                    add = true;
                    useText = AddText(useText, card.useCost);
                }
                add = false;
                if (card.upkeepOutput.Count != 0) {
                    foreach(Effect effect in card.upkeepOutput) {
                        upkeepText = AddText(upkeepText, effect);
                    }
                    upkeepText += ".";
                    upkeepText = "On " + "<sprite name="+"Upkeep_symbol"+"> : " + upkeepText;
                }
                add = false;
                foreach(Effect effect in card.useOutput) {
                    useText = AddText(useText, effect);
                }
                if (card.useOutput.Count > 0) {
                    useText =  "On " + "<sprite name="+"Use_symbol"+"> : " + useText;
                }
                effectText.text += "<br>" + useText + "<br>" + upkeepText;
            }
        }

        public void Update() {
            if (card != null) {
                CanPlay.SetActive(card.gM.TryToPay(card.cardCost) && toggleCanPlay);
            }
        }
    }
}
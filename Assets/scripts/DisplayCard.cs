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
        
        public void SetCard(Card _card) {
            card = _card;
            SetDisplay();
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
                case CardType.ProjectType:
                    backgroundArt.sprite = Resources.Load<Sprite>("Deal_card_1");
                    break;
            }
            
            cardName.text = " " + card.cardName;
            flavorText.text = " " + card.flavorText;
            if (card.type == CardType.FactoryType) {
                int cost = 0;
                foreach (Effect effect in card.cardCost) {
                    if (effect.effectType == EffectType.Money) {
                        cost = effect.amount;
                        cardCost.text = " " + -1*effect.amount;
                    }
                }
            }
            
            effectText.text = " ";
            add = true;
            if (card.type == CardType.DealType) {
                foreach (Effect effect in card.cardCost) {
                    if (effect.effectType == EffectType.Money || effect.effectType == EffectType.Power) {
                        effectText.text += "Pay ";
                    }
                    effectText.text = AddText(effectText.text, effect);
                }
            }

            add = false;
            foreach(Effect effect in card.effects) {
                effectText.text = AddText(effectText.text, effect);
            } 
        }
    }
}
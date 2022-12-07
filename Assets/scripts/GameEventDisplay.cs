using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameEventNS;
using cardNameSpace;
using CardDisplay;
public class GameEventDisplay : DisplayNS.Display {
      public GameEvent gameEvent;
        public TextMeshProUGUI eventText;
        public List<TextMeshProUGUI> optionText;
        public GameObject optionContainer;
        public TextMeshProUGUI outcomeText;

        public void SetEvent(GameEvent _event){
            gameEvent = _event;
            SetChoiceDisplay();
        }
        
        public void SetChoiceDisplay() {
            cardName.text = gameEvent.eventName;
            cardArt.sprite = gameEvent.eventImage;
            eventText.text = gameEvent.eventText;
            optionText = new List<TextMeshProUGUI>(optionContainer.GetComponentsInChildren<TextMeshProUGUI>());
        }

        public void SetOutcomeDisplay() {
            

        }

        public void Choice(int choice) {
            gameEvent.MakeChoice(choice);
        }

        public void CloseEvent() {
            gameEvent.PlayOutcome();
            Destroy(this.gameObject);
        }
}

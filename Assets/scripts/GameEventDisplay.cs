using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using GameEventNS;
using cardNameSpace;
using CardDisplay;

namespace EventDisplay {
    public class GameEventDisplay : DisplayNS.Display {
        public GameEvent gameEvent;
        public TextMeshProUGUI eventText;
        public List<TextMeshProUGUI> optionText;
        public GameObject optionContainer;
        [SerializeField]
        private GameObject choiceSection;
        [SerializeField]
        private GameObject outcomeSection;
        public TextMeshProUGUI outcomeText;

        public void SetEvent(GameEvent _event){
            gameEvent = _event;
            SetChoiceDisplay();
        }
        
        public void SetChoiceDisplay() {
            cardName.text = "" + gameEvent.eventName;
            eventText.text = "" + gameEvent.eventText;
            optionText = new List<TextMeshProUGUI>(optionContainer.GetComponentsInChildren<TextMeshProUGUI>());
            for (int i = 0; i < gameEvent.choices.Count; i++ ) {
                optionText[i].text = gameEvent.choices[i].optionText;
            }
        }

        public void SetOutcomeDisplay() {
            choiceSection.SetActive(false);
            outcomeSection.SetActive(true);
            outcomeText.text = gameEvent.choices[gameEvent.chosenEffect].outcomeText;
        }

        public void Choice(int choice) {
            gameEvent.MakeChoice(choice);
            SetOutcomeDisplay();
        }

        public void CloseEvent() {
            gameEvent.PlayOutcome();
            Destroy(this.gameObject);
        }
    }
}
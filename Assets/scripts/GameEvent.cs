using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using GMNameSpace;

namespace GameEventNS {
    [CreateAssetMenu(fileName = "Events/new Event", menuName = "Event/Default")]
    public class GameEvent : ScriptableObject {
        public string eventName;
        public string eventText;
        [SerializeField]
        public List<GameEventChoice> choices;
        // public string optionOneText;
        // public string optionTwoText;
        // public string outcomeOneText;
        // public string outcomeTwoText;
        // public List<Effect> optioneOneEffect;
        // public List<Effect> optioneTwoEffect;
        public Sprite eventImage;
        public int chosenEffect = 0;

        public void MakeChoice(int choice){
            chosenEffect = choice;
        }

        public void PlayOutcome(){
            if (chosenEffect == 1) {
                GameManager.playEffects.Invoke(choices[1].effect, null);
            } else {
                GameManager.playEffects.Invoke(choices[1].effect, null);
            }
        }
    }
    
    [Serializable]
    public class GameEventChoice {
        [SerializeField]
        public string optionText;
        [SerializeField]
        public string outcomeText;
        [SerializeField]
        public List<Effect> effect;
    }
}
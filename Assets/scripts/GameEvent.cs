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
        public Sprite eventImage;
        public int chosenEffect = 0;

        public void MakeChoice(int choice){
            chosenEffect = choice;
        }

        public void PlayOutcome(){
            GameManager.playEffects.Invoke(choices[chosenEffect].effect, null);
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using factoryNameSpace;
using cardNameSpace;
using TMPro;

namespace RP {
    public class ResourcePanel : MonoBehaviour {
        
        public TextMeshProUGUI balance_text;
        public TextMeshProUGUI pollution_text;
        public TextMeshProUGUI year_text;
        public TextMeshProUGUI turn_text;
        public Image panel;
        

        public void update_text(int money, int funding, int pollution, int maxPollution, int turn, int year) {
            balance_text.text = " " + money + "(+" + funding + ")";
            pollution_text.text = " " + pollution + "/" + maxPollution;
            year_text.text = " " + year;
            turn_text.text = " " + (turn + 1);
        }
    }
}
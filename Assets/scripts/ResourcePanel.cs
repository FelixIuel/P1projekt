using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RP {
    public class ResourcePanel : MonoBehaviour {
        
        public TextMeshProUGUI balance_text;
        public TextMeshProUGUI pollution_text;
        public TextMeshProUGUI year_text;
        public TextMeshProUGUI turn_text;
        public TextMeshProUGUI power_req;
        public TextMeshProUGUI cur_power;
        public TextMeshProUGUI backing;
        public Image powerBar;
        public Image backingBar;

        public void update_text(int money, int baseFunding, int pollution, int maxPollution, int turn, int year, int _powerReq, int _curPower, int _backing) {
            balance_text.text = " " + money + "(+" + baseFunding + ")";
            pollution_text.text = " " + pollution + "/" + maxPollution;
            year_text.text = " " + year;
            turn_text.text = " " + (turn + 1);
            cur_power.text = _curPower.ToString();
            power_req.text = _powerReq.ToString();
            backing.text = _backing.ToString();
            powerBar.fillAmount = Math.Clamp((float)_curPower/(float)_powerReq, 0, 1);
            backingBar.fillAmount = (float)_backing/100f;
        }
    }
}
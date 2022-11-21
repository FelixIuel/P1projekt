using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GMNameSpace;

namespace RP {
    public class ResourcePanel : MonoBehaviour {
        
        public TextMeshProUGUI balance_text;
        public TextMeshProUGUI curPollution;
        public TextMeshProUGUI maxPollution;
        public TextMeshProUGUI year_text;
        public TextMeshProUGUI turn_text;
        public TextMeshProUGUI power_req;
        public TextMeshProUGUI cur_power;
        public TextMeshProUGUI backing;
        public Image pollutionBar;
        public Image powerBar;
        public Image backingBar;
        public Image backingImage;

        public void update_text(int money, int funding, int pollution, int _maxPollution, int turn, int year, 
            int _powerReq, int _curPower, int _backing) {
            
            balance_text.text = " " + money + "(+" + funding + ")";
            curPollution.text = " " + pollution;
            maxPollution.text = " " + _maxPollution;
            pollutionBar.fillAmount = Math.Clamp((float)pollution/(float)_maxPollution, 0, 1);
            year_text.text = " " + year;
            turn_text.text = " " + (turn + 1);
            cur_power.text = _curPower.ToString();
            power_req.text = _powerReq.ToString();
            backing.text = _backing.ToString();
            powerBar.fillAmount = Math.Clamp((float)_curPower/(float)_powerReq, 0, 1);
            backingBar.fillAmount = (float)_backing/100f;

            if (_backing >= GameManager.backingTop){
                backingImage.sprite = Resources.Load<Sprite>("Sprites/Backing_symbol_happy");
            } else if (_backing <= GameManager.backingBottom) {
                backingImage.sprite = Resources.Load<Sprite>("Sprites/Backing_symbol_mad");
            } else {
                backingImage.sprite = Resources.Load<Sprite>("Sprites/Backing_symbol_meh");
            }
        }
    }
}
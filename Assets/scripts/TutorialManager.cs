using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using SceneManagerNS;
public class TutorialManager : MonoBehaviour {
    [SerializeField]
    private GameObject tutorialOverlay;
    [SerializeField]
    private TextMeshProUGUI tutorialSectionText;
    private int currentSection = 1;


    private void Start() {
        foreach (Transform child in tutorialOverlay.transform){
            child.gameObject.SetActive(false);
        }
        tutorialOverlay.transform.GetChild(currentSection-1).gameObject.SetActive(true);
        SectionText();
    }


    public void NextSection(){
        tutorialOverlay.transform.GetChild(currentSection-1).gameObject.SetActive(false);
        currentSection += 1;
        SectionText();
        if (currentSection > tutorialOverlay.transform.childCount) {
            SceneManagement.ChangeScene("SampleScene");
            return;
        }
        tutorialOverlay.transform.GetChild(currentSection-1).gameObject.SetActive(true);
    }

    void SectionText() {
        tutorialSectionText.text = "Tutorial:" + currentSection + "/" + tutorialOverlay.transform.childCount;
    }
}

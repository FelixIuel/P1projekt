using UnityEngine;
using TMPro;
using SceneManagerNS;
public class TutorialManager : MonoBehaviour {
    [SerializeField]
    private GameObject tutorialOverlay;
    [SerializeField]
    private TextMeshProUGUI tutorialSectionText;
    private int currentSection = 0;


    private void Start() {
        foreach (Transform child in tutorialOverlay.transform){
            child.gameObject.SetActive(false);
        }
        tutorialOverlay.transform.GetChild(currentSection).gameObject.SetActive(true);
        SectionText();
    }


    public void ChangeSection(int amount){
        if (currentSection == 0 && amount < 0) {
            return;
        }
        currentSection += amount;
        tutorialOverlay.transform.GetChild(currentSection-amount).gameObject.SetActive(false);
        SectionText();
        if (currentSection > tutorialOverlay.transform.childCount) {
            SceneManagement.ChangeScene("SampleScene");
            return;
        }
        tutorialOverlay.transform.GetChild(currentSection).gameObject.SetActive(true);
    }

    void SectionText() {
        tutorialSectionText.text = "Tutorial:" + currentSection + "/" + tutorialOverlay.transform.childCount;
    }
}

using UnityEngine;

public class ShowTutorialAfterIntro : MonoBehaviour
{
    public GameObject tutorialPanel;
    public PopupManager dialogue;

    void Start()
    {
        tutorialPanel.SetActive(false);
        dialogue.OnDialogueComplete += ShowTutorial;
    }

    void ShowTutorial()
    {
        tutorialPanel.SetActive(true);
    }
}

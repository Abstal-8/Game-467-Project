using UnityEngine;

public class ShowTutorialAfterIntro : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialCanvas;   // whole TutorialCanvas
    public GameObject tutorialPanel;    // Tutorial_Panel (child of canvas)

    [Header("Dialogue")]
    public PopupManager popup;          // PopupManager on PopupSystemRoot

    void Start()
    {
        if (tutorialCanvas != null)
            tutorialCanvas.SetActive(true);   // canvas stays active

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);   // start hidden

        if (popup != null)
        {
            popup.OnDialogueComplete += ShowTutorial;
        }
        else
        {
            Debug.LogError("[ShowTutorialAfterIntro] Popup reference missing!");
        }
    }

    void OnDestroy()
    {
        if (popup != null)
            popup.OnDialogueComplete -= ShowTutorial;
    }

    void ShowTutorial()
    {
        Debug.Log("[ShowTutorialAfterIntro] Showing tutorial panel.");

        if (tutorialCanvas != null)
            tutorialCanvas.SetActive(true);

        if (tutorialPanel != null)
            tutorialPanel.SetActive(true);
    }
}

using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class ShowTutorialAfterIntro : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialCanvas;   // your TutorialCanvas
    public GameObject Panel;    // Tutorial_Panel
    public GameObject SwapPanel;

    [Header("Dialogue")]
    public PopupManager popup;

    private bool hasShown = false;

    void Start()
    {
        if (tutorialCanvas != null)
        {
            Panel.SetActive(true); // <- actually show it
         
        }

        if (Panel != null)
            Panel.SetActive(false);
            SwapPanel.SetActive(false);

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
        // If we already showed it once (after intro), ignore future dialogues
        if (hasShown) return;
        hasShown = true;

        Debug.Log("[ShowTutorialAfterIntro] Showing tutorial panel (first time only).");

        if (tutorialCanvas != null)
        Panel.GetComponent<SetText>().SetMessage("Tutorial: press R to enter Spirit Form");
        tutorialCanvas.SetActive(true);
        Panel.SetActive(true);
        SwapPanel.SetActive(true);

       

        // We don't care about later dialogues (like the cat), so unsubscribe
        if (popup != null)
            popup.OnDialogueComplete -= ShowTutorial;
    }
}

using UnityEngine;
using TMPro;

public class SpiritTutorial : MonoBehaviour
{
    [Header("UI")]
    public GameObject tutorialPanel;        // Background panel or text container
    public TextMeshProUGUI tutorialText;    // Text component ("Press T to enter spirit form")

    [Header("Settings")]
    public KeyCode spiritKey = KeyCode.T;   // Spirit form key
    public float delayBeforeShowing = 1f;   // Optional delay before text appears

    bool hasShown = false;
    bool spiritEntered = false;

    void Start()
    {
        if (tutorialPanel)
            tutorialPanel.SetActive(false);
        Invoke(nameof(ShowTutorial), delayBeforeShowing);
    }

    void Update()
    {
        if (!hasShown) return;

        // Hide tutorial once player enters Spirit Form
        if (Input.GetKeyDown(spiritKey))
        {
            HideTutorial();
        }
    }

    void ShowTutorial()
    {
        if (hasShown || spiritEntered) return;
        if (tutorialPanel)
            tutorialPanel.SetActive(true);
        if (tutorialText)
            tutorialText.text = "Press T to enter spirit form";
        hasShown = true;
    }

    void HideTutorial()
    {
        if (tutorialPanel)
            tutorialPanel.SetActive(false);
        spiritEntered = true;
    }
}

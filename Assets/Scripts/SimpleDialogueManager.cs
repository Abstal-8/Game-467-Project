using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SimpleDialogueManager : MonoBehaviour
{
    public static SimpleDialogueManager Instance;

    [Header("UI References")]
    public TextMeshProUGUI dialogueText;   // TMP text at bottom
    public Image dialoguePanel;            // black background panel (Image component)

    [Header("Settings")]
    public float typingSpeed = 0.03f;
    public float fadeSpeed = 3f;           // how fast panel fades in/out

    private bool isPlaying = false;

    void Awake()
    {
        Instance = this;
        dialogueText.gameObject.SetActive(false);
        if (dialoguePanel) dialoguePanel.gameObject.SetActive(false);
    }

    public void StartDialogue(string[] lines)
    {
        if (!isPlaying)
            StartCoroutine(PlayDialogue(lines));
    }

    private IEnumerator PlayDialogue(string[] lines)
    {
        isPlaying = true;
        dialogueText.text = "";
        dialogueText.gameObject.SetActive(true);
        if (dialoguePanel) dialoguePanel.gameObject.SetActive(true);

        // Fade panel in
        if (dialoguePanel) yield return StartCoroutine(FadePanel(0f, 0.65f));

        foreach (string line in lines)
        {
            dialogueText.text = "";
            foreach (char c in line)
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(typingSpeed);
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // Fade panel out
        if (dialoguePanel) yield return StartCoroutine(FadePanel(0.65f, 0f));

        dialogueText.gameObject.SetActive(false);
        if (dialoguePanel) dialoguePanel.gameObject.SetActive(false);
        isPlaying = false;
    }

    private IEnumerator FadePanel(float from, float to)
    {
        Color c = dialoguePanel.color;
        float t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * fadeSpeed;
            c.a = Mathf.Lerp(from, to, t);
            dialoguePanel.color = c;
            yield return null;
        }
    }
}

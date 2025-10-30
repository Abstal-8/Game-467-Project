using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroDialogue : MonoBehaviour
{
    [Header("UI References")]
    public Image fadePanel;                 // full-screen black Image
    public TextMeshProUGUI dialogueText;    // TMP at bottom

    [Header("Dialogue")]
    [TextArea(3, 10)]
    public string[] lines = {
        "??? : Hey, you're finally awake.",
        "Player : Where am I?",
        "??? : You've been out for hours... didn't think would make it, welcome back.",
        "Player: What Happened?, Where am I?",
        "??? : I would be more worried about what you are, and why they want you",
        "Player : I can't move",
        "??? : Dang they must've banged you up good, you are shackled, I could help you get out... for a small favor",
        "Player : ... Alright",
        ""
    };

    public float typingSpeed = 0.03f;
    public float fadeDuration = 1f;
    public string nextSceneName = "StartingRoom";

    void Awake()
    {
        // Make sure the UI starts fully black
        if (fadePanel != null)
        {
            var c = fadePanel.color;
            c.a = 1f;
            fadePanel.color = c;

            // Persist the entire UI root (Canvas) across scene loads
            DontDestroyOnLoad(fadePanel.transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // fallback
        }
    }

    void Start() => StartCoroutine(Run());

    IEnumerator Run()
    {
        Debug.Log("Intro: PlayDialogue start");
        yield return StartCoroutine(PlayDialogue());
        Debug.Log("Intro: PlayDialogue done");

        Debug.Log("Intro: LoadNextScene start");
        yield return StartCoroutine(LoadNextScene());
        Debug.Log("Intro: LoadNextScene done");

        Debug.Log("Intro: FadeFromBlack start");
        yield return StartCoroutine(FadeFromBlack());
        Debug.Log("Intro: FadeFromBlack done");

        Destroy(gameObject);
    }


    IEnumerator PlayDialogue()
    {
        for (int i = 0; i < lines.Length; i++)
        {
            yield return StartCoroutine(TypeLine(lines[i]));
            if (i < lines.Length - 1)
                yield return new WaitUntil(() =>
                    Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));
        }
    }


    IEnumerator TypeLine(string line)
    {
        dialogueText.text = "";
        foreach (char ch in line)
        {
            dialogueText.text += ch;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    IEnumerator LoadNextScene()
    {
        var op = SceneManager.LoadSceneAsync(nextSceneName);
        while (!op.isDone) yield return null;
    }

    IEnumerator FadeFromBlack()
    {
        Color c = fadePanel.color; // starts at a = 1
        for (float t = 0; t < 1f; t += Time.deltaTime / fadeDuration)
        {
            c.a = 1f - t;
            fadePanel.color = c;
            yield return null;
        }
        c.a = 0f; fadePanel.color = c;
    }
}

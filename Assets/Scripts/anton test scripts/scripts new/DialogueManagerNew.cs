using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager_V2 : MonoBehaviour
{
    // --- UI REFERENCES (DRAG AND DROP IN INSPECTOR) ---
    [Header("UI References")]
    [Tooltip("The root GameObject/Canvas holding the entire dialogue system (V2DialogueSystem object).")]
    public GameObject dialogueRoot; 
    public TextMeshProUGUI dialogueText; 
    public AudioSource audioSource;
    public Image portraitImage;

    [Header("Assets & Configuration")]
    public AudioClip typingSound;
    public Sprite[] characterPortraits; 
    public float typingSpeed = 0.05f;
    
    [Header("Dialogue Content")]
    // Starting with an empty list prevents accidental text showing up.
    public NewDialogueEntry[] dialogueLines = new NewDialogueEntry[0]; 

    // --- Private State ---
    private bool isTyping = false;
    private int currentLineIndex = 0;

    void Start()
    {
        // 1. Hide the entire dialogue system at the start of the scene
        if (dialogueRoot != null)
        {
            dialogueRoot.SetActive(false); 
        }
    }

    void Update()
    {
        // Advance or skip dialogue using the Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines(); 
                dialogueText.text = dialogueLines[currentLineIndex].line;
                isTyping = false;
            }
            else
            {
                AdvanceDialogue();
            }
        }
    }

    // Public call triggered by the NewDialogueTrigger script
    public void StartDialogue()
    {
        if (dialogueLines.Length == 0) return;
        
        if (dialogueRoot != null) dialogueRoot.SetActive(true);
        currentLineIndex = 0;
        
        UpdatePortrait(dialogueLines[currentLineIndex].speakerIndex);
        StartCoroutine(TypeLine(dialogueLines[currentLineIndex].line));
    }

    private void AdvanceDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            UpdatePortrait(dialogueLines[currentLineIndex].speakerIndex);
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex].line));
        }
        else
        {
            // === END OF CONVERSATION LOGIC: CLOSES THE POP-UP ===
            if (dialogueRoot != null)
            {
                dialogueRoot.SetActive(false);
            }
            // Add code here to reactivate the trigger area if needed.
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = ""; 

        foreach (char character in line.ToCharArray())
        {
            dialogueText.text += character; 

            if (char.IsLetterOrDigit(character) && audioSource != null && typingSound != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f); 
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed); 
        }

        isTyping = false;
    }

    void UpdatePortrait(int index)
    {
        if (portraitImage != null && characterPortraits != null && characterPortraits.Length > index && characterPortraits[index] != null)
        {
            portraitImage.sprite = characterPortraits[index];
        }
    }
}
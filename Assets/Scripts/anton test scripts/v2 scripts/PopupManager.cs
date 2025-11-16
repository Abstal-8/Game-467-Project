using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    // --- UI REFERENCES (DRAG AND DROP IN INSPECTOR) ---
    [Header("UI References")]
    [Tooltip("The root GameObject that holds the Canvas/DialogueBox (This object itself).")]
    public GameObject dialogueRoot; 
    public TextMeshProUGUI dialogueText; 
    public AudioSource audioSource;
    public Image portraitImage;

    [Header("Assets & Configuration")]
    public AudioClip typingSound;
    public Sprite[] characterPortraits; 
    public float typingSpeed = 0.05f;
    
    [Header("Dialogue Content (Set this for each NPC instance)")]
    // The list of conversations lines for this specific instance.
    public NPCData[] dialogueLines = new NPCData[0]; 

    // --- Private State ---
    private bool isTyping = false;
    private int currentLineIndex = 0;

    void Start()
    {
        // Hide the entire UI system on scene load.
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

    // Public method called by the Trigger script
    public void StartDialogue()
    {
        // Exit if there's no content
        if (dialogueLines.Length == 0) return;
        
        // Show the entire UI root
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
            // End Conversation: Closes the box without loading a new scene.
            if (dialogueRoot != null)
            {
                dialogueRoot.SetActive(false);
            }
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = ""; 

        foreach (char character in line.ToCharArray())
        {
            dialogueText.text += character; 

            // Play sound with volume boost and pitch variation
            if (char.IsLetterOrDigit(character) && audioSource != null && typingSound != null)
            {
                audioSource.pitch = Random.Range(0.9f, 1.1f); 
                audioSource.PlayOneShot(typingSound, 1.5f); // 1.5f volume boost
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
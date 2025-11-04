using System.Collections;
using UnityEngine;
using TMPro; // Crucial: Import the TextMeshPro library

public class DialogueManager : MonoBehaviour
{
    // --- Public Variables (Drag & Drop in Unity Inspector) ---
    [Header("UI References")]
    public TextMeshProUGUI dialogueText; // Drag your DialogueText TMP object here!
    public GameObject dialoguePanel;      // Drag your DialogueBox Image object here!
    public AudioSource audioSource;       // Drag an AudioSource component here!

    [Header("Dialogue Settings")]
    [Tooltip("The time delay (in seconds) between each character appearing.")]
    public float typingSpeed = 0.05f; 
    
    [Tooltip("The sound clip to play for each character.")]
    public AudioClip typingSound; 

    // --- Private Variables ---
    private bool isTyping = false;
    private string[] dialogueLines; // Array to hold all the lines of dialogue
    private int currentLineIndex = 0;

    void Start()
    {
        // 1. Set up the dialogue lines for the start of the game
        dialogueLines = new string[]
        {
        "Player : Urg my head",
        "??? : Hey, you're finally awake.",
        "??? : You've been out for hours... didn't think would make it, what a shame.",
        "Player: Huh?, What Happened?, Where am I?, Who are you??",
        "??? : You don't remember what happened?",
        "Player : Why can't I move, Why can't I see!?",
        "??? : Calm down were just spirit shackled, not even by a strong one",
        "Player : What the hell are you talking about?",
        "??? : You really don't remember?",
        "Player : ...No",
        "??? : Now <i>that</i> is intriguing",
        "??? : I suppose I can help, for a price",
        "Player : What do you want?",
        "??? : We'll discuss that once you're free, feel the area around you with your mind, you should feel a tug",
        "Player : ...What?",
        "??? : Just do it",
        "Player : Huh, weird I do feel something",
        "??? : Good focus on that and pull",
        ""
        };

        // 2. Hide the dialogue panel on startup
        dialoguePanel.SetActive(false); 
        
        // 3. Begin the dialogue sequence
        StartDialogue();
    }

    void Update()
    {
        // 1. Check for player input to advance or skip text
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // If text is currently typing, skip to the end of the line
                StopAllCoroutines(); 
                dialogueText.text = dialogueLines[currentLineIndex];
                isTyping = false;
            }
            else
            {
                // If text is fully displayed, move to the next line
                AdvanceDialogue();
            }
        }
    }

    // --- Core Dialogue Functions ---

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        currentLineIndex = 0;
        StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
    }

    private void AdvanceDialogue()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
        }
        else
        {
            // End of Dialogue Logic
            dialoguePanel.SetActive(false);
            Debug.Log("Dialogue Ended. Start the Game!");
            // TODO: Add code here to transition to your main game scene/logic
        }
    }

    // This is the Coroutine that creates the character-by-character effect.
    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = ""; // Clear the text before typing a new line

        foreach (char character in line.ToCharArray())
        {
            dialogueText.text += character; // Add the next character

            // Play the "blip" sound
            if (audioSource != null && typingSound != null)
            {
                // Optional: Randomize the pitch for a more "voice-like" effect (like Sans)
                audioSource.pitch = Random.Range(0.9f, 1.1f); 
                audioSource.PlayOneShot(typingSound);
            }

            yield return new WaitForSeconds(typingSpeed); // Wait for the set delay
        }

        isTyping = false;
    }
}
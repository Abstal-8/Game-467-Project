using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Ink.Runtime;
using Unity.VisualScripting;
using System.Collections.Generic;

public class PopupManager : MonoBehaviour
{
    // --- UI REFERENCES (DRAG AND DROP IN INSPECTOR) ---
    [Header("UI References")]
    [Tooltip("The root GameObject that holds the Canvas/DialogueBox (This object itself).")]
    public GameObject dialogueRoot;
    public GameObject choicePanel;
    public GameObject dialoguePanel; 
    public List<Button> buttonChoices;
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
    private bool isChoosing = false;
    private int currentLineIndex = 0;
    private Story currentStory;

    void Start()
    {
        // Hide the entire UI system on scene load.
        if (dialogueRoot != null)
        {
            dialogueRoot.SetActive(false); 
        }

        choicePanel.SetActive(false);
    }

    void Update()
    {
        // Advance or skip dialogue using the Spacebar
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                StopAllCoroutines(); 
                // dialogueText.text = dialogueLines[currentLineIndex].line;
                dialogueText.text = currentStory.currentText;
                isTyping = false;
            }
            else if (!isChoosing)
            {
                AdvanceDialogue();
            }
        }
    }

    public void InitializeDialogue(TextAsset JSON)
    {
        currentStory = new Story(JSON.text);
        dialogueText.text = "";
    }

    // Public method called by the Trigger script
    public void StartDialogue()
    {
        if (currentStory == null)
        {
            Debug.Log("Story Object has not been Initialized. " + currentStory);
        }
        else if (currentStory.currentChoices.Count > 0)
        {
            SetupChoice();
        }

        dialogueRoot.SetActive(true);

        AdvanceDialogue();

        // // Exit if there's no content
        // if (dialogueLines.Length == 0) return;
        
        // // Show the entire UI root
        // if (dialogueRoot != null) dialogueRoot.SetActive(true);
        // currentLineIndex = 0;
        
        // UpdatePortrait(dialogueLines[currentLineIndex].speakerIndex);
        // StartCoroutine(TypeLine(dialogueLines[currentLineIndex].line));
    }

    private void AdvanceDialogue()
    {
        if (currentStory.canContinue)
        {
            StartCoroutine(TypeLine(currentStory.Continue()));
        }
        else if (currentStory.currentChoices.Count > 0)
        {
            SetupChoice();
        }
        else
        {
            dialogueRoot.SetActive(false);
           
        }

        // currentLineIndex++;

        // if (currentLineIndex < dialogueLines.Length)
        // {
        //     UpdatePortrait(dialogueLines[currentLineIndex].speakerIndex);
        //     StartCoroutine(TypeLine(dialogueLines[currentLineIndex].line));
        // }
        // else
        // {
        //     // End Conversation: Closes the box without loading a new scene.
        //     if (dialogueRoot != null)
        //     {
        //         dialogueRoot.SetActive(false);
        //     }
        // }
    }

    private void SetupChoice()
    {
        isChoosing = true;

        // Move the dialogue panel left to make room (optional)
        dialoguePanel.transform.localPosition = new Vector3(
            dialoguePanel.transform.localPosition.x - 400,
            dialoguePanel.transform.localPosition.y,
            dialoguePanel.transform.localPosition.z
        );

        choicePanel.SetActive(true);

        // First, hide all buttons and clear old listeners
        for (int b = 0; b < buttonChoices.Count; b++)
        {
            buttonChoices[b].gameObject.SetActive(false);
            buttonChoices[b].onClick.RemoveAllListeners();
        }

        // Now set up buttons for each current Ink choice
        for (int i = 0; i < currentStory.currentChoices.Count; i++)
        {
            Choice choice = currentStory.currentChoices[i];

            Button btn = buttonChoices[i];
            btn.gameObject.SetActive(true);

            TextMeshProUGUI buttonText = btn.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = choice.text;

            int choiceIndex = i; // capture i for this specific button

            btn.onClick.AddListener(() =>
            {
                isChoosing = false;
                choicePanel.SetActive(false);

                // (optional) move dialogue panel back:
                dialoguePanel.transform.localPosition = new Vector3(
                    dialoguePanel.transform.localPosition.x + 400,
                    dialoguePanel.transform.localPosition.y,
                    dialoguePanel.transform.localPosition.z
                );

                currentStory.ChooseChoiceIndex(choiceIndex);
                AdvanceDialogue();
            });
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
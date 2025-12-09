using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("References")]
    public PopupManager popupManager;      // assign in Inspector
    public TextAsset inkJSON;             // your Ink .json for THIS NPC

    public GameObject promptPanel;

    private bool playerInRange = false;

    void Update()
    {
        // Example: press E to start dialogue when in range
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartConversation();
            promptPanel.SetActive(false);
        }
    }

    private void StartConversation()
    {
        if (popupManager == null || inkJSON == null)
        {
            Debug.LogWarning("DialogueTrigger missing PopupManager or Ink JSON reference.");
            return;
        }

        popupManager.InitializeDialogue(inkJSON);
        popupManager.StartDialogue();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            promptPanel.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            promptPanel.SetActive(false);
        }
    }
}

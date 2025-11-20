using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public PopupManager popupManager;    // drag your PopupManager here
    public TextAsset inkJSON;            // drag Cat.json here

    private bool playerInRange = false;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            popupManager.InitializeDialogue(inkJSON);
            popupManager.StartDialogue();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}

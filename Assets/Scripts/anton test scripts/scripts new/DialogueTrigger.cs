using UnityEngine;

public class DialogueTrigger_V2 : MonoBehaviour
{
    [Tooltip("The tag of the object that should start the dialogue (e.g., 'Player').")]
    public string playerTag = "Player";
    
    // We reference the new manager class name
    private DialogueManager_V2 manager; 

    void Start()
    {
        // Automatically find the new manager instance in the scene
        manager = FindObjectOfType<DialogueManager_V2>();

        if (manager == null)
        {
            Debug.LogError("FATAL ERROR: Could not find DialogueManager_V2 script in the scene! Ensure it is attached to an active GameObject.");
        }
    }

    // This uses 2D physics interaction
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Universal Debug Test
        Debug.Log($"[TRIGGER DETECTED] {other.gameObject.name} entered the area.");
        
        // 1. Check if the object that entered is the player
        if (other.CompareTag(playerTag))
        {
            if (manager != null)
            {
                // 2. Start the dialogue sequence!
                manager.StartDialogue();
                
                // 3. Deactivate the trigger so it only runs once
                gameObject.SetActive(false); 
            }
        }
    }
}
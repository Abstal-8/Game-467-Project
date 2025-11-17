using UnityEngine;
using UnityEngine.UIElements;

public class NPCTrigger : MonoBehaviour
{
    [Tooltip("The tag of the object that should start the dialogue (e.g., 'Player').")]
    public string playerTag = "Player";
    
    // Automatically references the new manager script
    private PopupManager manager;
    public GameObject managerObj; 
    public TextAsset inkJSON;

    void Start()
    {
        // Finds the manager by accessing the script on the Manager object
        manager = managerObj.GetComponent<PopupManager>();

        if (manager == null)
        {
            Debug.LogError("FATAL ERROR: Could not find PopupManager script in the scene! Ensure it is attached to an active GameObject.");
        }
    }

    // Uses 2D physics interaction (required for your 2D project)
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug confirms the trigger fired
        Debug.Log($"[TRIGGER DETECTED] {other.gameObject.name} entered the area.");
        
        // 1. Check if the object that entered is the player
        if (other.CompareTag(playerTag))
        {
            if (manager != null)
            {
                //Initializing Dialogue with relevant file
                manager.InitializeDialogue(inkJSON);

                // 2. Start the dialogue sequence!
                manager.StartDialogue();
                
                // 3. Deactivate the trigger so it only runs once (optional)
                gameObject.SetActive(false); 
            }
        }
    }
}
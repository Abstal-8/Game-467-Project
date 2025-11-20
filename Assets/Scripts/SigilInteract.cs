using UnityEngine;

public class SigilInteract : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptUI;           // existing "Press E" panel
    public GameObject returnToBodyPanel;  // NEW: "Press T to return to your body" panel

    [Header("Spirit")]
    public SpiritFormController player;  // Player (SpiritFormController)
    public string spiritTag = "Spirit";

    [Header("After Unlock Dialogue")]
    public PopupManager popup;           // PopupSystemRoot (PopupManager)
    public TextAsset afterUnlockInk;     // ReturnToBody.json

    private bool inRange = false;
    private bool hasUnlocked = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("[Sigil] OnTriggerEnter2D with " + other.name + " (tag=" + other.tag + ")");

        if (hasUnlocked) return;

        if (other.CompareTag(spiritTag))
        {
            inRange = true;
            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("[Sigil] OnTriggerExit2D with " + other.name + " (tag=" + other.tag + ")");

        if (other.CompareTag(spiritTag))
        {
            inRange = false;
            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (hasUnlocked || !inRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[Sigil] E pressed in range => UnlockMovement()");

            hasUnlocked = true;

            // 1. Unlock movement
            if (player != null)
                player.UnlockMovement();

            // 2. Hide original "Press E" prompt
            if (promptUI != null)
                promptUI.SetActive(false);

            // 3. Show "Press T to return to your body"
            if (returnToBodyPanel != null)
                returnToBodyPanel.SetActive(true);

            // 4. Play the extra Ink line
            if (popup != null && afterUnlockInk != null)
            {
                Debug.Log("[Sigil] Playing after-unlock Ink: " + afterUnlockInk.name);
                popup.InitializeDialogue(afterUnlockInk);
                popup.StartDialogue();
            }

            // 5. Disable collider so this zone can't be used again
            var col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }

    }
}

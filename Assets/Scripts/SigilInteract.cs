using UnityEngine;

public class SigilInteract : MonoBehaviour
{
    [Header("UI")]
    public GameObject promptUI;          // the "Press E to enter spirit form" panel

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

            // 2. Hide prompt
            if (promptUI != null)
                promptUI.SetActive(false);

            // 3. Play the extra Ink line
            if (popup != null && afterUnlockInk != null)
            {
                Debug.Log("[Sigil] Playing after-unlock Ink: " + afterUnlockInk.name);
                popup.InitializeDialogue(afterUnlockInk);
                popup.StartDialogue();
            }
            else
            {
                Debug.LogWarning("[Sigil] Popup or Ink asset missing, cannot play dialogue.");
            }

            // 4. Disable collider so the zone never re-triggers
            var col = GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }
    }
}

using UnityEngine;

public class SpiritUnlock : MonoBehaviour
{
    public SpiritFormController player;   // ✅
    public PopupManager popup;
    public TextAsset afterUnlockInk;
    public string spiritTag = "Spirit";

    private bool used = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (used || player == null || player.HasMovementUnlocked)
            return;

        if (other.CompareTag(spiritTag) && Input.GetKeyDown(KeyCode.E))
        {
            used = true;
            Debug.Log("[SpiritUnlock] E pressed in spirit zone, unlocking movement.");

            // 1. Unlock movement
            player.UnlockMovement();

            // 2. Play the extra line of dialogue
            if (popup != null && afterUnlockInk != null)
            {
                Debug.Log("[SpiritUnlock] Starting after-unlock Ink: " + afterUnlockInk.name);
                popup.InitializeDialogue(afterUnlockInk);
                popup.StartDialogue();
            }
            else
            {
                Debug.LogWarning("[SpiritUnlock] Popup or Ink asset is missing.");
            }

            // 3. Remove this trigger forever
            Destroy(gameObject);
        }
    }
}

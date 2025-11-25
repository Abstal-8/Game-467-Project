using UnityEngine;

public class CatCageUnlock : MonoBehaviour
{
    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;
    public GameObject promptUI;

    [Header("Cat Controller")]
    public FamiliarController familiar;   // ← drag your cat here

    private bool playerInRange = false;
    private PlayerKeyRing keyRing;
    private bool opened = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (opened) return;

        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            keyRing = other.GetComponent<PlayerKeyRing>();

            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (opened) return;

        if (other.CompareTag(playerTag))
        {
            playerInRange = false;
            keyRing = null;

            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }

    private void Update()
    {
        if (opened || !playerInRange || keyRing == null)
            return;

        if (Input.GetKeyDown(interactKey))
        {
            if (!keyRing.HasCatKey)
            {
                Debug.Log("[CatCage] Player has no key.");
                return;
            }

            // Player uses the key
            keyRing.UseCatKey();

            // Open the cage
            UnlockCage();
        }
    }

    private void UnlockCage()
    {
        opened = true;

        Debug.Log("[CatCage] Cage unlocked! Freeing familiar...");

        // Hide the prompt
        if (promptUI != null)
            promptUI.SetActive(false);

        // Disable the cage collider so it no longer blocks anything
        var col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Call the cat's controller to free it
        if (familiar != null)
            familiar.FreeFamiliar();
        else
            Debug.LogWarning("[CatCage] No FamiliarController assigned!");

        // OPTIONAL: disable cage sprite or play animation
        // gameObject.SetActive(false);
    }
}

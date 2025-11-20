using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;
    public GameObject promptUI;  // optional "Press E to pick up key" text

    private bool playerInRange = false;
    private PlayerKeyRing keyRing;

    private void OnTriggerEnter2D(Collider2D other)
    {
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
        if (!playerInRange || keyRing == null) return;

        if (Input.GetKeyDown(interactKey))
        {
            // Give the key to the player
            keyRing.GiveCatKey();

            // Hide prompt
            if (promptUI != null)
                promptUI.SetActive(false);

            // Destroy the key object from the world
            Destroy(gameObject);
        }
    }
}

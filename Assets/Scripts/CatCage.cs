using UnityEngine;

public class CatCageUnlock : MonoBehaviour
{
    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;
    public GameObject promptUI;         // "Press E to unlock cage"

    [Header("Cat")]
    public MonoBehaviour catFollowScript;  // your Familiar_Movement / CatFollow script

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
        if (opened || !playerInRange || keyRing == null) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (!keyRing.HasCatKey)
            {
                Debug.Log("[CatCage] Player has no key, cannot open cage.");
                return;
            }

            // Use key
            keyRing.UseCatKey();

            // Free the cat
            OpenCageAndFreeCat();
        }
    }

    private void OpenCageAndFreeCat()
    {
        opened = true;
        Debug.Log("[CatCage] Cage opened, freeing cat.");

        // Call cat script
        if (catFollowScript != null)
        {
            // Try to call a method named "FreeCat" on whatever script you drop here
            var method = catFollowScript.GetType().GetMethod("FreeCat");
            if (method != null)
                method.Invoke(catFollowScript, null);
            else
                Debug.LogWarning("[CatCage] No FreeCat() method on " + catFollowScript.GetType().Name);
        }

        // Hide the prompt
        if (promptUI != null)
            promptUI.SetActive(false);

        // Optionally, disable this collider & maybe the bars sprite so the cage looks open
        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // If the cage is just a sprite object, you can also disable it or change sprite here
        // gameObject.SetActive(false);  // if you want the whole cage to vanish
    }
}

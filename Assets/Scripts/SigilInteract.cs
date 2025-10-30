using UnityEngine;

public class SigilInteractUI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject promptUI;          // the TMP text GameObject (World-space or Screen-space)
    public SpiritForm player;            // Player object that has SpiritForm
    public string spiritTag = "Spirit";  // tag on your spirit prefab

    bool inRange;

    void Start()
    {
        if (promptUI) promptUI.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log($"[Sigil] OnTriggerEnter2D with {other.name} (tag={other.tag})");
        if (other.CompareTag(spiritTag))
        {
            inRange = true;
            if (promptUI) promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log($"[Sigil] OnTriggerExit2D with {other.name} (tag={other.tag})");
        if (other.CompareTag(spiritTag))
        {
            inRange = false;
            if (promptUI) promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("[Sigil] E pressed in range → UnlockMovement()");
            if (player) player.UnlockMovement();
            GetComponent<DialogueTrigger>()?.TriggerDialogue();
            if (promptUI) promptUI.SetActive(false);
            // Optional: Destroy(gameObject);
        }
    }
}

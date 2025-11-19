using UnityEngine;

public class NoteInteractUI : MonoBehaviour
{
    [Header("Assign in Inspector")]
    public GameObject promptUI;               // the TMP text GameObject (World-space or Screen-space)
    public SpiritFormController controller;   // <-- talk to the new controller
    public string spiritTag = "Spirit";       // tag on your spirit prefab

    bool inRange;

    void Start()
    {
        if (promptUI) promptUI.SetActive(false);

        // Safety: auto-find controller if not set in Inspector
        if (controller == null)
        {
            controller = FindObjectOfType<SpiritFormController>();
            if (controller == null)
            {
                Debug.LogError("[Sigil] No SpiritFormController found in scene!");
            }
        }
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

            if (controller != null)
            {
                controller.UnlockMovement();   
                controller.UnlockSwap();      
               
            }

            if (promptUI) promptUI.SetActive(false);
            
        }
    }
}

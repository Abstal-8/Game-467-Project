using UnityEngine;

public class NoteInteractUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player";      // tag on your spirit prefab
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public GameObject objectGlow;

    private bool inRange;
    private bool show = false;
    private NoteInfoSwitcher infoSwitcher;

    void Start()
    {
        infoSwitcher = GetComponent<NoteInfoSwitcher>();

        if (canvasContainer) canvasContainer.SetActive(false);

        inRange = true;

        // if (objectGlow != null)
        // {
        //     objectGlow.SetActive(false);
        // }
        // HideAllChildren();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"[Sigil] OnTriggerEnter2D with {other.name} (tag={other.tag})");
        if (other.CompareTag(playerTag))
        {
            inRange = true;
            if (objectGlow != null)
            {
                objectGlow.SetActive(true);
                ItemGlow glowScript = objectGlow.GetComponent<ItemGlow>();
                glowScript.inInteraction();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log($"[Sigil] OnTriggerExit2D with {other.name} (tag={other.tag})");
        if (other.CompareTag(playerTag))
        {
            inRange = false;
            if (objectGlow != null)
            {
                ItemGlow glowScript = objectGlow.GetComponent<ItemGlow>();
                glowScript.TurnLightOff();
                // objectGlow.SetActive(false);
            }
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !show)
        {
            // Debug.Log("[Sigil] E pressed in range → UnlockMovement()");
            infoSwitcher.Show();
            show = true;

            if (canvasContainer != null) {
                canvasContainer.SetActive(true);
            }
            if (objectGlow != null)
            {
                objectGlow.SetActive(true);
                ItemGlow glowScript = objectGlow.GetComponent<ItemGlow>();
                if (glowScript != null) {
                    glowScript.inInteraction();
                }
                else
                {
                    Debug.Log("glowScript broken");
                }
            }
        }
        if (show && !inRange)
        {
            infoSwitcher.Hide();
            show = false;

            if (canvasContainer != null) {
                canvasContainer.SetActive(false);
            }
        }
        if (inRange && show && Input.GetKeyDown(KeyCode.Q))
        {
            if (objectGlow != null)
            {
                // objectGlow.SetActive(true);
                // ItemGlow glowScript = objectGlow.GetComponent<ItemGlow>();
                // if (glowScript != null) {
                //     glowScript.inInteraction();
                // }
                // else
                // {
                //     Debug.Log("glowScript broken");
                // }
                infoSwitcher.PrevPage();
            }
        }
        if (inRange && show && Input.GetKeyDown(KeyCode.E))
        {
            if (objectGlow != null)
            {
                // objectGlow.SetActive(true);
                // ItemGlow glowScript = objectGlow.GetComponent<ItemGlow>();
                // if (glowScript != null) {
                //     glowScript.inInteraction();
                // }
                // else
                // {
                //     Debug.Log("glowScript broken");
                // }
                infoSwitcher.NextPage();
            }
        }
        if (inRange && Input.GetKeyDown(KeyCode.Escape))
        {
            infoSwitcher.Hide();

            if (canvasContainer != null && canvasContainer.activeSelf) {
                canvasContainer.SetActive(false);
            }
            if (objectGlow != null)
            {
                ItemGlow glowScript = objectGlow.GetComponent<ItemGlow>();
                glowScript.outInteraction();
            }
        }
    }

    // private void HideAllChildren()
    // {
    //     foreach (Transform child in transform)
    //     {
    //         if (objectGlow != null && child.gameObject == objectGlow)
    //             continue;   // skip light

    //         child.gameObject.SetActive(false);
    //     }
    // }
    // private void HideRecursively(Transform obj)
    // {
    //     if (objectGlow != null && child.gameObject == objectGlow) {}
    //     else { child.gameObject.SetActive(false); }
    //     foreach (Transform child in obj)
    //     {
    //         HideRecursively(child);
    //     }

    // }
}

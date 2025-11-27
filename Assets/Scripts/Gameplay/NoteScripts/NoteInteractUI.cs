using UnityEngine;

public class NoteInteractUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player";      // tag on your spirit prefab
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public GameObject objectGlow;
    // public string closingTrigger = "PlayClosing";
    // public string openingTrigger = "PlayOpening";
    // public string stopAnimation = "PlayNothing";

    private bool inRange;
    private bool show = false;
    private NoteInfoSwitcher infoSwitcher;
    private SpriteRenderer sr;
    private string closing = "BookClosing";
    private string opening = "BookOpening";
    private Animation anim;
    // private Animator anim;
    

    void Start()
    {
        infoSwitcher = GetComponent<NoteInfoSwitcher>();
        if (infoSwitcher == null) { Debug.Log("NoteInfoSwitcher Script not found!");}
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) { Debug.Log("SpriteRenderer Component not found!");}

        anim = GetComponent<Animation>();
        if (anim == null) {Debug.Log("anim not picking up component");}
        else {Debug.Log("animation component found!");}

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
            //TODO If animating a book opening ever comes back up again. look here (I am getting logs that it is playing)
            //TODO most likely problem is that the objectinformation sprites dissapear, but still unknown
            anim.Play(opening);
            // if (anim[opening] != null)
            // {
            //     // Play the animation from the start
            //     anim[opening].wrapMode = WrapMode.Once; // play only once
            //     anim.Play(opening);
            //     Debug.Log("Animation " + opening + " Playing");
            // }
            // else
            // {
            //     Debug.LogWarning("Animation not found: " + opening);
            // }

            // anim.ResetTrigger(closingTrigger);
            // anim.SetTrigger(openingTrigger);
            // Debug.Log("Triggered animation: " + openingTrigger);
            // anim.SetTrigger(stopAnimation);
            infoSwitcher.Show();
            show = true;
            sr.enabled = false;

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
            //TODO If animating a book opening ever comes back up again. look here (I am getting logs that it is playing)
            //TODO most likely problem is that the objectinformation sprites dissapear, but still unknown
            // if (anim[closing] != null)
            // {
            //     // Play the animation from the start
            //     anim[closing].wrapMode = WrapMode.Once; // play only once
            //     anim.Play(closing);
            // }
            // else
            // {
            //     Debug.LogWarning("Animation not found: " + closing);
            // }

            // anim.ResetTrigger(openingTrigger);
            // anim.SetTrigger(closingTrigger);
            // Debug.Log("Triggered animation: " + closingTrigger);
            // anim.SetTrigger(stopAnimation);
            infoSwitcher.Hide();
            sr.enabled = true;

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

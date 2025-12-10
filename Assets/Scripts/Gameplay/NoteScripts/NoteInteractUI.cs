using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NoteInteractUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player";      // tag on your spirit prefab
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public Light2D Glow;
    // public string closingTrigger = "PlayClosing";
    // public string openingTrigger = "PlayOpening";
    // public string stopAnimation = "PlayNothing";

    public bool inRange;
    private bool show = false;
    private NoteInfoSwitcher infoSwitcher;
    private SpriteRenderer sr;
    // private string closing = "BookClosing";
    // private string opening = "BookOpening";
    // private Animation anim;
    // private Animator anim;
    

    void Start()
    {
        infoSwitcher = GetComponent<NoteInfoSwitcher>();
        if (infoSwitcher == null) { Debug.Log("NoteInfoSwitcher Script not found!");}
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) { Debug.Log("SpriteRenderer Component not found!");}
        Glow.enabled = false;
        Glow.color = Color.yellow;

        // anim = GetComponent<Animation>();
        // if (anim == null) {Debug.Log("anim not picking up component");}
        // else {Debug.Log("animation component found!");}

        if (canvasContainer) canvasContainer.SetActive(false);

        inRange = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"[Sigil] OnTriggerEnter2D with {other.name} (tag={other.tag})");
        if (other.CompareTag(playerTag))
        {
            inRange = true;
            if (Glow != null)
            {
                Glow.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Debug.Log($"[Sigil] OnTriggerExit2D with {other.name} (tag={other.tag})");
        if (other.CompareTag(playerTag))
        {
            inRange = false;
            if (Glow != null)
            {
                Glow.enabled = false;
            }
        }
    }

    void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E) && !show)
        {
            //TODO If animating a book opening ever comes back up again. look here (I am getting logs that it is playing)
            //TODO most likely problem is that the objectinformation sprites dissapear, but still unknown
            // anim.Play(opening);
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
            if (Glow != null)
            {
                Glow.color = Color.white;
                Glow.enabled = false;
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
            infoSwitcher.PrevPage();
        }
        if (inRange && show && Input.GetKeyDown(KeyCode.E))
        {
            infoSwitcher.NextPage();
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
            if (Glow != null)
            {
                Glow.enabled = true;
            }
        }
    }
}

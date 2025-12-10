using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SkullInteractUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player";      // tag on your spirit prefab
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public Light2D Glow;

    [Header("Light Source (child to toggle on/off)")]
    public Sprite headlessSprite;
    public Sprite headfulSprite;

    private bool inRange;
    private bool show = false;
    private SpriteRenderer sr;
    public bool skull;
    

    void Start()
    {
        if (sr == null) { Debug.Log("SpriteRenderer Component not found!");}
        Glow.enabled = false;
        sr = GetComponent<SpriteRenderer>();
        Glow.color = Color.yellow;

        // anim = GetComponent<Animation>();
        // if (anim == null) {Debug.Log("anim not picking up component");}
        // else {Debug.Log("animation component found!");}

        if (canvasContainer) canvasContainer.SetActive(false);

        inRange = false;
        skull = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log($"[Sigil] OnTriggerEnter2D with {other.name} (tag={other.tag})");
        if (!skull) {
            if (other.CompareTag(playerTag))
            {
                inRange = true;
                if (Glow != null)
                {
                    Glow.enabled = true;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!skull) {
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
    }

    void Update()
    {
        //if any other object of this type is clicked, the sprite should dissapear as well.
        if (Input.GetKeyDown(KeyCode.E))
        {
            sr.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sr.enabled = true;
        }
        //if user hits "E" key while in range
        if (inRange && Input.GetKeyDown(KeyCode.E) && !show && !skull)
        {
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
        //If user walks out of range the light should go out
        if (show && !inRange)
        {
            show = false;

            if (canvasContainer != null) {
                canvasContainer.SetActive(false);
            }
        }
        if (inRange && show && Input.GetKeyDown(KeyCode.Space))
        {
            show = false;
            skull = true;
            sr.enabled = false;

            if (canvasContainer != null) {
                canvasContainer.SetActive(false);
            }
        }
        //If the user has the note open and presses "Escape" key, leave the book UI
        if (inRange && Input.GetKeyDown(KeyCode.Escape))
        {
            if (!skull) {sr.enabled = true;}

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

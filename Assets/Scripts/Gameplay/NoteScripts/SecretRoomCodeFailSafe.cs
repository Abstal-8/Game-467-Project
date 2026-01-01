using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SecretRoomCodeFailSafe : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player"; //will find player so future scripts that rely on enabling or disabling player scripts can be called directly in file
    private PlayerMovement playerCon;
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public Light2D Glow;

    public bool inRange;
    private bool show = false;
    private SpriteRenderer sr;
    

    void Start()
    {
        playerCon = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();
        if (playerCon == null) { Debug.Log("SpiritFormController Script not found on Player!");}
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) { Debug.Log("SpriteRenderer Component not found!");}
        Glow.enabled = false;
        Glow.color = Color.yellow;

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
        // these two statements are fail safes in case for any reason the e or escape is triggered when "show" or "inRange" have not
        if (Input.GetKeyDown(KeyCode.E))
        {
            sr.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sr.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.E) && inRange && !show)
        {
            show = true;
            playerCon.LockMovement();
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
        if (Input.GetKeyDown(KeyCode.Escape) && inRange)
        {
            show = false;
            playerCon.UnlockMovement();
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

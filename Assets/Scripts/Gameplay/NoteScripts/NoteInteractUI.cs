using UnityEngine;
using UnityEngine.Rendering.Universal;

public class NoteInteractUI : MonoBehaviour
{
    [Header("Containers")]
    public string playerTag = "Player";
    private PlayerMovement playerCon;
    public GameObject canvasContainer;

    [Header("Light Source (child to toggle on/off)")]
    public Light2D Glow;

    public bool inRange = false;
    private bool show = false;
    private NoteInfoSwitcher infoSwitcher;
    private SpriteRenderer sr;
    private bool movementLocked = false;
    private Rigidbody2D rb;
    

    void Start()
    {
        playerCon = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();
        if (playerCon == null) { Debug.Log("PlayerMovementScript not found on Player!");}
        infoSwitcher = GetComponent<NoteInfoSwitcher>();
        if (infoSwitcher == null) { Debug.Log("NoteInfoSwitcher Script not found!");}
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) { Debug.Log("SpriteRenderer Component not found!");}
        Glow.enabled = false;
        Glow.color = Color.yellow;

        if (canvasContainer) canvasContainer.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
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
        //when not inRange
        if (Input.GetKeyDown(KeyCode.E))
        {
            sr.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sr.enabled = true;
        }
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!show) {
                show = true;
                sr.enabled = false;
                playerCon.LockMovement();
                if (canvasContainer != null) {
                    canvasContainer.SetActive(true);
                }
                infoSwitcher.Show();
                if (Glow != null)
                {
                    Glow.color = Color.white;
                    Glow.enabled = false;
                }
            }
            else
            {
                infoSwitcher.NextPage();
            }
        }
        // switching between pages
        if (inRange && show && Input.GetKeyDown(KeyCode.Q))
        {
            infoSwitcher.PrevPage();
        }
        if (show && inRange && Input.GetKeyDown(KeyCode.Escape))
        {
            show = false;
            sr.enabled = true;
            playerCon.UnlockMovement();
            infoSwitcher.Hide();
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


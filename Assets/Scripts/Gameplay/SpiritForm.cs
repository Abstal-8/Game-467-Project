using UnityEngine;

public class SpiritForm : MonoBehaviour
{
    [Header("Prefabs & Visuals")]
    public GameObject spiritPrefab;                 // ghost prefab with tag "Spirit"
    public Color bodyColorInSpirit = Color.blue;

    [Header("References")]
    public MonoBehaviour playerMovementScript;      // your PlayerMovement script
    public MonoBehaviour cameraFollowScript;        // optional camera follow
    public Transform startPointOnTable;             // empty transform on the table

    [Header("Flow")]
    public bool lockAtStart = true;                 // start immobilized on table

    private SpriteRenderer bodySR;
    private PolygonCollider2D poly;
    private Rigidbody2D rb;
    private bool inSpiritForm = false;
    private bool movementLocked = false;
    private GameObject spiritInstance;

    void Awake()
    {
        bodySR = GetComponent<SpriteRenderer>();
        poly = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // Spawn on the table
        if (startPointOnTable) transform.position = startPointOnTable.position;

        if (lockAtStart) LockBody();
    }

    void Update()
    {
        // Only allow toggling spirit when locked or when already in spirit form
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!inSpiritForm && movementLocked) EnterSpiritForm();
            else if (inSpiritForm) ExitSpiritForm(); // optional: allow return any time
        }
    }

    void LockBody()
    {
        movementLocked = true;

        if (playerMovementScript) playerMovementScript.enabled = false;

        if (rb)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false; // freeze physics so the body can't move/fall
        }
    }

    void UnlockBody()
    {
        movementLocked = false;

        if (rb) rb.simulated = true;
        if (playerMovementScript) playerMovementScript.enabled = true;
    }

    void EnterSpiritForm()
    {
        if (inSpiritForm) return;

        // Body stays frozen in place
        bodySR.color = bodyColorInSpirit;

        Vector3 spawnPos = (poly ? poly.bounds.center : transform.position) + new Vector3(0.6f, 0f, 0f);
        spiritInstance = Instantiate(spiritPrefab, spawnPos, Quaternion.identity);
        inSpiritForm = true;
    }

    void ExitSpiritForm()
    {
        if (!inSpiritForm) return;

        bodySR.color = Color.white;

        if (spiritInstance) Destroy(spiritInstance);
        inSpiritForm = false;
    }

    // Called by the unlock zone when the Spirit presses E in the trigger
    public void UnlockMovement()
    {
        // return to body and enable controls
        ExitSpiritForm();
        UnlockBody();
        lockAtStart = false;
    }
}

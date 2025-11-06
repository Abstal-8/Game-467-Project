using System;
using UnityEngine;

public class SpiritForm : MonoBehaviour
{
    [Header("Prefabs & Visuals")]
    public GameObject spiritPrefab;                 // ghost prefab with tag "Spirit"
    public Color bodyColorInSpirit = Color.blue;
    public event Action<bool> OnSpiritStateChanged;   // true = entered, false = exited
    public bool IsInSpirit => inSpiritForm;

    [Header("References")]
    public MonoBehaviour playerMovementScript;      // your PlayerMovement script
    public MonoBehaviour cameraFollowScript;        // optional camera follow
    public Transform startPointOnTable;             // empty transform on the table

    [Header("Flow")]
    public bool lockAtStart = true;                 // start immobilized on table

    [Header("Animation")]
    public Animator animator;

    private SpriteRenderer bodySR;
    private PolygonCollider2D poly;
    private Rigidbody2D rb;
    private bool inSpiritForm = false;
    private bool movementLocked = false;
    private GameObject spiritInstance;
    private bool animatorWasEnabled = true;

    void Awake()
    {
        bodySR = GetComponent<SpriteRenderer>();
        poly = GetComponent<PolygonCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        if (animator)
            animatorWasEnabled = animator.enabled;

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
            if (!inSpiritForm)
                EnterSpiritForm();
            else
                ExitSpiritForm();
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

        // freeze player body in place
        LockBody();
        bodySR.color = bodyColorInSpirit;

        // spawn spirit slightly offset
        Vector3 spawnPos = transform.position + new Vector3(0.6f, 0f, 0f);
        spiritInstance = Instantiate(spiritPrefab, spawnPos, Quaternion.identity);

        inSpiritForm = true;
        OnSpiritStateChanged?.Invoke(true);
        if (animator)
        {
            animatorWasEnabled = animator.enabled;
            animator.enabled = false;
        }
    }


    void ExitSpiritForm()
    {
        if (!inSpiritForm) return;

        // remove spirit, restore player control
        if (spiritInstance) Destroy(spiritInstance);
        bodySR.color = Color.white;
        if (animator)
            animator.enabled = animatorWasEnabled;
        inSpiritForm = false;
        OnSpiritStateChanged?.Invoke(false);
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

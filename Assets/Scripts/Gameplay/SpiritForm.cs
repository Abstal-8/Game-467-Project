using System;
using UnityEngine;

public class SpiritForm : MonoBehaviour
{
    [Header("Prefabs & Visuals")]
    public GameObject spiritPrefab;                 // ghost prefab with tag "Spirit"
    public Color bodyColorInSpirit = Color.blue;
    public event Action<bool> OnSpiritStateChanged; // true = entered, false = exited
    public bool IsInSpirit => inSpiritForm;

    [Header("References")]
    public MonoBehaviour playerMovementScript;      // your PlayerMovement script

    [Header("Flow")]
    public bool lockAtStart = true;                 // start immobilized on table
    public KeyCode spiritToggleKey = KeyCode.T;     // toggle spirit form
    public KeyCode swapKey = KeyCode.F;             // swap body <-> spirit while in spirit form

    [Header("Animation")]
    public Animator animator;

    private SpriteRenderer bodySR;
    private PolygonCollider2D poly;
    private Rigidbody2D rb;
    private bool inSpiritForm = false;
    private bool movementLocked = false;
    private GameObject spiritInstance;
    private bool animatorWasEnabled = true;
    private bool hasUnlockedOnce = false;

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
        if (lockAtStart)
        {
            // First time you load in: stuck on table
            LockBody();
        }
        else
        {
            movementLocked = false;
        }
    }

    void Update()
    {
        // Toggle spirit form (T)
        if (Input.GetKeyDown(spiritToggleKey))
        {
            if (!inSpiritForm)
                EnterSpiritForm();
            else
                ExitSpiritForm();
        }

        // Swap places (F) - only if in spirit form AND after the first puzzle
        if (inSpiritForm && hasUnlockedOnce && Input.GetKeyDown(swapKey))
        {
            SwapBodyAndSpirit();
        }
    }

    // --- Core helpers ---

    void LockBody()
    {
        movementLocked = true;

        if (playerMovementScript)
            playerMovementScript.enabled = false;

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

        if (rb)
            rb.simulated = true;

        if (playerMovementScript)
            playerMovementScript.enabled = true;
    }

    void EnterSpiritForm()
    {
        if (inSpiritForm) return;
        if (!spiritPrefab) return;

        // Freeze body in place
        LockBody();

        if (bodySR)
            bodySR.color = bodyColorInSpirit;

        // Spawn spirit slightly offset from body
        Vector3 spawnPos = transform.position + new Vector3(0.6f, 0f, 0f);
        spiritInstance = Instantiate(spiritPrefab, spawnPos, Quaternion.identity);

        inSpiritForm = true;

        if (animator)
        {
            animatorWasEnabled = animator.enabled;
            animator.enabled = false; // pause body animator
        }

        OnSpiritStateChanged?.Invoke(true);
    }

    void ExitSpiritForm()
    {
        if (!inSpiritForm) return;

        // Remove spirit visual
        if (spiritInstance)
            Destroy(spiritInstance);

        if (bodySR)
            bodySR.color = Color.white;

        if (animator)
            animator.enabled = animatorWasEnabled;

        inSpiritForm = false;

        // Before first unlock: body stays chained
        // After unlock: exiting spirit returns full control to body
        if (hasUnlockedOnce)
        {
            UnlockBody();
        }

        OnSpiritStateChanged?.Invoke(false);
    }

    /// <summary>
    /// Swap the positions of the body and the spirit.
    /// Only makes sense while in spirit form.
    /// </summary>
    void SwapBodyAndSpirit()
    {
        if (!inSpiritForm) return;
        if (!spiritInstance) return;

        // Save current positions
        Vector3 bodyPos = transform.position;
        Vector3 spiritPos = spiritInstance.transform.position;

        // Swap them
        transform.position = spiritPos;
        spiritInstance.transform.position = bodyPos;

        // Note: rb.simulated is false while in spirit form, but you can
        // still teleport the body by setting transform.position.
        // When you later ExitSpiritForm and unlock, the body will be
        // controllable at the new spot.
    }

    // --- Called by puzzle / cutscene to free the body for the first time ---

    public void UnlockMovement()
    {
        hasUnlockedOnce = true;
        lockAtStart = false;

        if (inSpiritForm)
        {
            // If you are currently out of your body when it's "freed",
            // exiting spirit form will now give you full control.
            ExitSpiritForm();
        }
        else
        {
            // If you're currently in your body, just unlock it now.
            UnlockBody();
        }
    }
}

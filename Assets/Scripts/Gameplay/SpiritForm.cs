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

        // Whenever we leave our body, we freeze it in place
        LockBody();

        if (bodySR)
            bodySR.color = bodyColorInSpirit;

        // spawn spirit slightly offset from body
        Vector3 spawnPos = transform.position + new Vector3(0.6f, 0f, 0f);
        spiritInstance = Instantiate(spiritPrefab, spawnPos, Quaternion.identity);

        inSpiritForm = true;

        if (animator)
        {
            animatorWasEnabled = animator.enabled;
            animator.enabled = false;
        }

        OnSpiritStateChanged?.Invoke(true);
    }

    void ExitSpiritForm()
    {
        if (!inSpiritForm) return;

        // remove spirit visual
        if (spiritInstance)
            Destroy(spiritInstance);

        if (bodySR)
            bodySR.color = Color.white;

        if (animator)
            animator.enabled = animatorWasEnabled;

        inSpiritForm = false;

        // IMPORTANT:
        //  - Before the first unlock: body stays locked on table (hasUnlockedOnce == false)
        //  - After unlock: exiting spirit returns full control to body
        if (hasUnlockedOnce)
        {
            UnlockBody();
        }

        OnSpiritStateChanged?.Invoke(false);
    }

    
    public void UnlockMovement()
    {
        
        hasUnlockedOnce = true;
        lockAtStart = false;

        
        if (inSpiritForm)
        {
            
            ExitSpiritForm();
        }
        else
        {
          
            UnlockBody();
        }
    }
}

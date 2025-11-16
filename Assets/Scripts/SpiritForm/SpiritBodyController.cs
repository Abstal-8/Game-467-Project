using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class SpiritBodyController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The SpiritFormController on the player.")]
    [SerializeField] private SpiritFormController controller;

    [Tooltip("Movement script (PlayerMovement)")]
    [SerializeField] private PlayerMovement playerMovementScript;

    [Tooltip("Optional animator for the body sprite.")]
    [SerializeField] private Animator animator;

    [Header("Visuals")]
    [Tooltip("Body color in non-spirit mode.")]
    [SerializeField] private Color normalColor = Color.white;

    [Tooltip("Body color while spirit form is active.")]
    [SerializeField] private Color spiritColor = Color.blue;

    private SpriteRenderer bodySR;
    private Rigidbody2D rb;
    private bool animatorWasEnabled = true;

    private void Awake()
    {
        bodySR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (animator != null)
        {
            animatorWasEnabled = animator.enabled;
        }
    }

    private void OnEnable()
    {
        if (controller != null)
        {
            controller.OnStateChanged += HandleStateChanged;
        }
        else
        {
            Debug.LogError("[SpiritBodyController] No SpiritFormController assigned!");
        }
    }

    private void OnDisable()
    {
        if (controller != null)
        {
            controller.OnStateChanged -= HandleStateChanged;
        }
    }
    
    private void HandleStateChanged(SpiritState newState)
    {
        switch (newState)
        {
            case SpiritState.BodyLocked:
                HandleBodyLocked();
                break;

            case SpiritState.BodyFree:
                HandleBodyFree();
                break;

            case SpiritState.SpiritActive:
                HandleSpiritActive();
                break;
        }
    }

    // === State handlers ===

    private void HandleBodyLocked()
    {
        LockBody();
        SetBodyVisualLocked();
    }

    private void HandleBodyFree()
    {
        UnlockBody();
        SetBodyVisualNormal();
    }

    private void HandleSpiritActive()
    {
        // Freezes Spirit But keeps it visual
        LockBody();
        SetBodyVisualInSpirit();
    }

    // === Movement / physics helpers ===

    private void LockBody()
    {
        // Do NOT disable PlayerMovement; just freeze physics
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;   // physics frozen → PlayerMovement can't move the body
        }
    }

    private void UnlockBody()
    {
        if (rb != null)
        {
            rb.simulated = true;    // physics resumes → PlayerMovement will move again
        }
    }

    // === Visual helpers ===

    private void SetBodyVisualNormal()
    {
        if (bodySR != null)
        {
            bodySR.color = normalColor;
        }

        if (animator != null)
        {
            animator.enabled = animatorWasEnabled;
        }
    }

    private void SetBodyVisualInSpirit()
    {
        if (bodySR != null)
        {
            bodySR.color = spiritColor;
        }

        if (animator != null)
        {
            animatorWasEnabled = animator.enabled;
            animator.enabled = false; // pause animation while spirit is out
        }
    }

    private void SetBodyVisualLocked()
    {
        // For now this is same as normal. Later you can give locked state its own color.
        SetBodyVisualNormal();
    }
}

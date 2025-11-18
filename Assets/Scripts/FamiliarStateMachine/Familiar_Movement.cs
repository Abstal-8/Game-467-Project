using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FamiliarMovement : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform player;
    [SerializeField] private float followRadius = 1.8f;
    [SerializeField] private float maxSpeed = 2.5f;
    [SerializeField] private float smoothing = 5f;

    [Header("Refs")]
    [SerializeField] private FamiliarController controller;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 velocity;
    private bool canMove = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        if (controller == null)
            controller = GetComponent<FamiliarController>();
    }

    private void OnEnable()
    {
        if (controller != null)
            controller.OnStateChanged += HandleStateChanged;
    }

    private void OnDisable()
    {
        if (controller != null)
            controller.OnStateChanged -= HandleStateChanged;
    }

    private void HandleStateChanged(FamiliarState state)
    {
        switch (state)
        {
            case FamiliarState.Caged:
                canMove = false;
                rb.linearVelocity = Vector2.zero;
                if (animator) animator.SetBool("isWalking", false);
                break;

            case FamiliarState.Following:
                canMove = true;
                break;

            case FamiliarState.Idle:
                canMove = false;
                rb.linearVelocity = Vector2.zero;
                if (animator) animator.SetTrigger("IdleVariant"); // yawn/sit/etc
                break;

            case FamiliarState.Special:
                // you can temporarily turn off follow or play a special anim
                canMove = false;
                rb.linearVelocity = Vector2.zero;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (!canMove || player == null) return;

        Vector2 toPlayer = (player.position - transform.position);
        float dist = toPlayer.magnitude;

        if (dist <= followRadius)
        {
            rb.linearVelocity = Vector2.zero;
            //if (animator) animator.SetBool("isWalking", false);
            return;
        }

        Vector2 dir = toPlayer.normalized;
        Vector2 targetVel = dir * maxSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, targetVel, smoothing * Time.fixedDeltaTime);

        //if (animator)
        //{
        //    animator.SetBool("isWalking", true);
        //    animator.SetFloat("moveX", dir.x);
        //    animator.SetFloat("moveY", dir.y);
        //}
    }
}

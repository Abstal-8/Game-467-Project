using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform startPoint;

    private Rigidbody2D rb;
    private Animator animator;
    Vector2 input;
    Vector2 lastDir = new Vector2(0, -1);  // default: facing down

    void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        // Initialize facing direction in animator
        animator.SetFloat("prevInputX", lastDir.x);
        animator.SetFloat("prevInputY", lastDir.y);
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        if (input != Vector2.zero)
        {
            // walking
            animator.SetBool("isWalking", true);

            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);

            // update last direction ONLY when moving
            lastDir = input;
            animator.SetFloat("prevInputX", lastDir.x);
            animator.SetFloat("prevInputY", lastDir.y);
        }
        else
        {
            // idle
            animator.SetBool("isWalking", false);

            // don't touch lastDir here � that�s the whole point
            animator.SetFloat("prevInputX", lastDir.x);
            animator.SetFloat("prevInputY", lastDir.y);
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
        
    }
}

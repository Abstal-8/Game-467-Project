using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Transform startPoint;

    private Rigidbody2D rb;
    private Animator animator;
    Vector2 input;
    Vector2 lastDir = new Vector2(0, -1);  // default: facing down
    //inserting a flag to stop or start this script from updating based on user wasd that can be called from any file
    private bool movementLocked = false;
    public void LockMovement() => movementLocked = true;
    public void UnlockMovement() => movementLocked = false;

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
        if (movementLocked) return;   //if movement bool has been locked by another file do not read anything else in "update()"

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
        if (movementLocked) return;
        rb.linearVelocity = input * moveSpeed;
    }
}

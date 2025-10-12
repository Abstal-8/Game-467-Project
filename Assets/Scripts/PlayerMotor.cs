using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Called from MoveState every frame
    public void Move(Vector2 input)
    {
        // normalize like your original code
        if (input.sqrMagnitude > 1f) input.Normalize();
        rb.linearVelocity = input * moveSpeed;

        
        if (input != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("prevInputX", 0f); 
            animator.SetFloat("prevInputY", 0f);
        }
    }

    // Called from IdleState when entering / or to hard-stop
    public void Stop()
    {
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isWalking", false);
    }
}

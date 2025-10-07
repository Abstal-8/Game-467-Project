using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    private Animator animator;
    Vector2 input;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input.Normalize();

        if (input.y > 0 || input.y < 0 || input.x > 0 || input.x < 0) // RIGHT NOW WE ONLY HAVE UP AND DOWN ANIMATIONS
        {
            animator.SetBool("isWalking", true);
            animator.SetFloat("InputX", input.x);
            animator.SetFloat("InputY", input.y);
        }
        else
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("prevInputX", input.x);
            animator.SetFloat("prevInputY", input.y);
        }
        
        
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}

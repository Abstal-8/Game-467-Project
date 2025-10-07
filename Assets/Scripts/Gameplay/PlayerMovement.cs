using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    [SerializeField] float moveSpeed;
    private Rigidbody2D rb;
    Vector2 input;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        input.Normalize();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = input * moveSpeed;
    }
}

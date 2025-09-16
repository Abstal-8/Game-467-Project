using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float _xDir;
    float _yDir;
    [SerializeField] float moveSpeed;
    private Vector2 _moveDir;
    private Rigidbody2D rb;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float _xDir = Input.GetAxis("Horizontal");
        float _yDir = Input.GetAxis("Vertical");
        _moveDir = new Vector2(_xDir, _yDir).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(_moveDir.x * moveSpeed, _moveDir.y * moveSpeed);
    }
}

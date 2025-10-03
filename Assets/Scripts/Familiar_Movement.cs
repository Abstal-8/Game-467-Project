using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[RequireComponent(typeof(Rigidbody2D))] // Component Needs a RigidBody2D on game object to work
public class Familiar_Movement : MonoBehaviour
{
    // SerializedField just lets us edit the feilds in inspector
    [SerializeField] Transform target; //target -> object going to follow the player
    [SerializeField] Vector2 offset = new Vector2(-1f, 0f); // direction it follows
    [SerializeField] float smoothTime = 0.12f;  // How quickly the familiar reacts, lower = snappier, higher = floatier
    [SerializeField] float maxSpeed = 12f;


    Rigidbody2D rb;
    Vector2 velocity;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;   // not effected by physics 
        if (!target)
            target = GameObject.FindGameObjectWithTag("Player")?.transform; // if no object is assigned player will look for a player tag instead 
    }
    void FixedUpdate()
    {
        if (!target) return;

        Vector2 desired = (Vector2)target.position + offset;
        Vector2 next = Vector2.SmoothDamp(
            rb.position,
            desired,
            ref velocity,
            smoothTime,
            maxSpeed,
            Time.fixedDeltaTime
        );

        rb.MovePosition(next);
    }
}
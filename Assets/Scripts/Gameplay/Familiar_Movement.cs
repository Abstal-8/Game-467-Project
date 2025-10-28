using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Familiar_Movement : MonoBehaviour
{
    [SerializeField] Transform target;            // player
    [SerializeField] float followRadius = 1.8f;   // “cloud” around player
    [SerializeField] float leashRadius = 4.0f;   // hard max distance
    [SerializeField] float smoothTime = 0.35f;  // higher = smoother/slower
    [SerializeField] float maxSpeed = 2.8f;   // cat walk speed
    [SerializeField] float minRetarget = 3f;     // pick new point every 5–10s
    [SerializeField] float maxRetarget = 5f;
    [SerializeField] float slowingRadius = 0.8f;  // ease-in distance
    [SerializeField] bool freed = false;

    Rigidbody2D rb;
    Vector2 velocity;

    // internal state
    Vector2 currentWaypoint;
    float retargetTimer;
    Vector2 lastPlayerPos;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;

        if (!target)
            target = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (target)
            lastPlayerPos = target.position;
        PickNewWaypoint(immediate: true);
    }

    void FixedUpdate()
    {
        if (!freed || !target) return;

        retargetTimer -= Time.fixedDeltaTime;
        float distToWaypoint = Vector2.Distance(rb.position, currentWaypoint);
        float distToTarget = Vector2.Distance(rb.position, (Vector2)target.position);

        // If timer expired, leashed too far, or player moved far from the circle center → pick new point
        float playerShift = Vector2.Distance(lastPlayerPos, target.position);
        if (retargetTimer <= 0f || distToTarget > leashRadius || playerShift > followRadius * 0.5f)
        {
            PickNewWaypoint();
            lastPlayerPos = target.position;
        }

        // smooth movement
        float t = Mathf.Clamp01(distToWaypoint / slowingRadius);
        float speedNow = Mathf.Lerp(0f, maxSpeed, t);

        Vector2 next = Vector2.SmoothDamp(
            rb.position, currentWaypoint, ref velocity,
            smoothTime, speedNow, Time.fixedDeltaTime);

        rb.MovePosition(next);
    }

    public void FreeFamiliar()
    {
        freed = true;
        enabled = true;
        PickNewWaypoint(immediate: true);
    }

    void PickNewWaypoint(bool immediate = false)
    {
        Vector2 center = target ? (Vector2)target.position : rb.position;

        // Pick a point not-too-close and not-too-far
        float minR = Mathf.Min(followRadius * 0.4f, followRadius - 0.05f); // inner band
        float r = Random.Range(minR, followRadius);
        Vector2 dir = Random.insideUnitCircle.normalized;
        currentWaypoint = center + dir * r;

        // Wait 5–10s before the next pick
        retargetTimer = Random.Range(minRetarget, maxRetarget);
        if (immediate) retargetTimer *= 0.5f; // optional shorter first wait
    }

    // Optional helper if you want to tweak radius at runtime
    public void SetFollowRadius(float r)
    {
        followRadius = Mathf.Max(0f, r);
        PickNewWaypoint();
    }

    // Optional: visualize radii in editor
#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        if (!target) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(target.position, followRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position, leashRadius);
    }
#endif
}

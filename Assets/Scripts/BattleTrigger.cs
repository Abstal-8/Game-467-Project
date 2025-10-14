using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyEncounterTrigger : MonoBehaviour
{
    [SerializeField] string playerTag = "Player";
    [SerializeField] bool oneShot = true;

    bool triggered;

    void Reset()
    {
        // Make sure collider is a trigger
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered && oneShot) return;
        if (!other.CompareTag(playerTag)) return;

        triggered = true;

        

        
        UnityEngine.SceneManagement.SceneManager.LoadScene("StartingRoom");

    
    }

   
    void OnDrawGizmosSelected()
    {
        var col = GetComponent<Collider2D>() as CircleCollider2D;
        if (col == null) return;
        Gizmos.color = new Color(1, 0, 0, 0.2f);
        Gizmos.DrawWireSphere(transform.position + (Vector3)col.offset, col.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y));
    }
}

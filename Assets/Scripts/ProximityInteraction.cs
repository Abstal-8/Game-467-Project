using UnityEngine;
using UnityEngine.Events;

public class ProximityInteraction : MonoBehaviour
{
    [SerializeField] GameObject promptUI;    
    [SerializeField] UnityEvent onInteract;  

    bool _playerInside;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = true;
            if (promptUI) promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _playerInside = false;
            if (promptUI) promptUI.SetActive(false);
        }
    }

    void Update()
    {
        if (_playerInside && Input.GetKeyDown(KeyCode.E))
        {
            if (promptUI) promptUI.SetActive(false);
            onInteract?.Invoke();
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        var col = GetComponent<CircleCollider2D>();
        if (col) Gizmos.DrawWireSphere(transform.position, col.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y));
    }
}

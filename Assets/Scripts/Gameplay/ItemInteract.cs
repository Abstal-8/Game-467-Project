using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemInteract : MonoBehaviour, Interaction
{
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;   // assign in Inspector
    [SerializeField] private Item currentItem;      // assign in Inspector

    [Header("Who can pick up?")]
    [SerializeField] private bool allowSpirit = false;   // toggle if Spirit can pick up
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private string spiritTag = "Spirit";

    [Header("Input")]
    [SerializeField] private KeyCode interactKey = KeyCode.F;

    // Don't serialize this — avoids a prefab accidentally saved as 'true'
    private bool _inRange = false;

    private void Awake()
    {
        // Ensure we’re using a trigger collider
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;

        // start safe
        _inRange = false;
    }

    private void Update()
    {
        if (_inRange && Input.GetKeyDown(interactKey))
        {
            TryPickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsValidInteracter(other))
            _inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsValidInteracter(other))
            _inRange = false;
    }

    private bool IsValidInteracter(Collider2D other)
    {
        if (other.CompareTag(playerTag)) return true;
        if (allowSpirit && other.CompareTag(spiritTag)) return true;
        return false;
    }

    private void TryPickup()
    {
        if (inventory == null || currentItem == null)
        {
            Debug.LogWarning($"[{name}] Missing Inventory or Item reference.");
            return;
        }

        inventory.AddToInventory(currentItem);
        currentItem.inInventory = true;
        Destroy(gameObject);
    }
    // put this inside ItemInteract
    public void Interact()   // satisfies Interaction.Interact()
    {
        // If your global interaction system already checks the E key,
        // just do the pickup when in range. Otherwise, you can also
        // check the key here.
        if (_inRange)
            TryPickup();
    }

}

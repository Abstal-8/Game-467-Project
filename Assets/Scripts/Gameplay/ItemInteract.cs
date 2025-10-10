using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemInteract : MonoBehaviour, Interaction
{


    [SerializeField] Inventory inventory;
    [SerializeField] Item currentItem;

    [SerializeField] bool _isInteracable;




    void Update()
    {
        Interact();
    }



    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInteracable = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        _isInteracable = false;
    }
    public void Interact()
    {
        if (_isInteracable && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddToInventory(currentItem);
            currentItem.inInventory = true;
            Destroy(gameObject);
        }
    }
}

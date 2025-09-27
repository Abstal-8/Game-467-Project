using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ItemInteract : MonoBehaviour, Interaction
{


    [SerializeField] bool _isInteracable;

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isInteracable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            _isInteracable = false;
        }
    
    }

    public void Interact(GameObject interactor)
    {
        
    }
}

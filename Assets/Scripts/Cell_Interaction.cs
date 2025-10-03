using UnityEngine;
using TMPro;

public class CellUnlock : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] Inventory inventory;            
    [SerializeField] Item requiredKey;              
    [SerializeField] ProximityInteraction prox;      
    [SerializeField] TMP_Text promptLabel;           
    [SerializeField] Familiar_Movement familiar;     

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        UpdatePrompt();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
    }

    void UpdatePrompt()
    {
        bool hasKey = inventory.HasItem(requiredKey);
        string msg = hasKey ? "Press E to open" : "You need a key";

        if (promptLabel) promptLabel.text = msg;
    }

    
    public void TryOpen()
    {
        bool hasKey = inventory.HasItem(requiredKey);
        if (!hasKey)
        {                 
            UpdatePrompt();
            return;
        }

        // Free/enable follower movement
        familiar.FreeFamiliar();   
       
    }
}

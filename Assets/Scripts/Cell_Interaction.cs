using UnityEngine;
using TMPro;

public class CellUnlock : MonoBehaviour
{
    [Header("Requirements")]
    public Inventory inventory;
    public Item keyItem;

    [Header("Who gets freed")]
    public Familiar_Movement familiar;      // <-- drag your Cat here in Inspector
    public Collider2D cageColliderToDisable; // optional: blocker you want to disable

    [Header("UI (optional)")]
    public TextMeshProUGUI prompt;

    [Header("Settings")]
    public string interactKey = "E";
    public string tagThatCanUnlock = "Spirit";

    bool inRange;

    void Awake()
    {
        if (!inventory) inventory = FindObjectOfType<Inventory>();
        if (!familiar) familiar = FindObjectOfType<Familiar_Movement>();
        if (prompt) prompt.gameObject.SetActive(false);
    }

    void Update()
    {
        if (!inRange) return;

        bool hasKey =
            inventory ? inventory.HasItem(keyItem)
                      : (keyItem && keyItem.inInventory);

        if (hasKey && Input.GetKeyDown(KeyCode.E))
        {
            UnlockCell();
        }
    }

    public void TryOpen()   // for ProximityInteraction event
    {
        bool hasKey =
            inventory ? inventory.HasItem(keyItem)
                      : (keyItem && keyItem.inInventory);

        if (hasKey) UnlockCell();
        else ShowNeedKey();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(tagThatCanUnlock)) return;
        inRange = true;
        UpdatePrompt();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(tagThatCanUnlock)) return;
        inRange = false;
        if (prompt) prompt.gameObject.SetActive(false);
    }

    void UpdatePrompt()
    {
        if (!prompt) return;

        bool hasKey =
            inventory ? inventory.HasItem(keyItem)
                      : (keyItem && keyItem.inInventory);

        prompt.text = hasKey ? "Press E to free the cat"
                             : "Locked. You need a key.";
        prompt.gameObject.SetActive(true);
    }

    void ShowNeedKey()
    {
        if (!prompt) return;
        prompt.text = "Locked. You need a key.";
        prompt.gameObject.SetActive(true);
    }

    void UnlockCell()
    {
        Debug.Log("[CellUnlock] Cell opened.");

        // 1) Free the cat
        if (familiar)
        {
            familiar.FreeFamiliar();
        }
        else
        {
            Debug.LogWarning("[CellUnlock] No Familiar_Movement reference set.");
        }

        // 2) Open the “cell” (optional)
        if (cageColliderToDisable) cageColliderToDisable.enabled = false;

        // 3) Hide prompt
        if (prompt) prompt.gameObject.SetActive(false);

        // 4) If this is a one-shot trigger, you can disable or destroy this
        // enabled = false; // or Destroy(this);
    }
}

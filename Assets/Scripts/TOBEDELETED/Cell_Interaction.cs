using UnityEngine;
using TMPro;

public class CellUnlock : MonoBehaviour
{
    [Header("Who gets freed")]
    public FamiliarController familiarController;     // drag the cat here
    public Collider2D cageColliderToDisable;          // drag the wall collider here

    [Header("UI")]
    public TextMeshProUGUI prompt;                    // "Press E to free the cat"

    [Header("Settings")]
    public KeyCode interactKey = KeyCode.E;
    public string playerTag = "Player";

    private bool inRange = false;

    private void Start()
    {
        if (prompt) prompt.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        inRange = true;
        if (prompt) prompt.gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        inRange = false;
        if (prompt) prompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!inRange) return;

        if (Input.GetKeyDown(interactKey))
            UnlockCell();
    }

    private void UnlockCell()
    {
        Debug.Log("[CellUnlock] Cat freed!");

        // Free the cat
        if (familiarController)
            familiarController.FreeFamiliar();

        // Disable the cage wall collider
        if (cageColliderToDisable)
            cageColliderToDisable.enabled = false;

        // Hide prompt
        if (prompt)
            prompt.gameObject.SetActive(false);

        // Remove this script (one-time use)
        enabled = false;
    }
}

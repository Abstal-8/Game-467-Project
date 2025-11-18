using UnityEngine;

public class CatCageInteract : MonoBehaviour
{
    [SerializeField] private FamiliarController familiar;
    [SerializeField] private GameObject promptUI;
    public KeyCode interactKey = KeyCode.E;

    private bool inRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = true;
        if (promptUI) promptUI.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        inRange = false;
        if (promptUI) promptUI.SetActive(false);
    }

    private void Update()
    {
        if (!inRange) return;

        if (Input.GetKeyDown(interactKey))
        {
            // you can drop in dialogue here; when done:
            familiar.FreeFamiliar();
            if (promptUI) promptUI.SetActive(false);
            // optionally disable cage collider, play open anim, etc.
            gameObject.SetActive(false);
        }
    }
}

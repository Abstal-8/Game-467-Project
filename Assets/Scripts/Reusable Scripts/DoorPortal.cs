using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorPortal : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private string targetSceneName;

    [Header("UI")]
    [SerializeField] private GameObject confirmPanel;

    [Header("Keyboard Input")]
    [SerializeField] private KeyCode yesKey = KeyCode.E;
    [SerializeField] private KeyCode noKey = KeyCode.Escape;

    private bool playerInRange = false;
    private bool confirmOpen = false;

    private void Start()
    {
        if (confirmPanel != null)
            confirmPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = true;
        OpenPanel();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        playerInRange = false;
        ClosePanel();
    }

    private void Update()
    {
        if (!confirmOpen) return;

        // Keyboard shortcuts while the panel is open
        if (Input.GetKeyDown(yesKey))
        {
            OnConfirmYes();
        }
        else if (Input.GetKeyDown(noKey))
        {
            OnConfirmNo();
        }
    }

    private void OpenPanel()
    {
        if (confirmPanel == null) return;

        confirmPanel.SetActive(true);
        confirmOpen = true;
        Debug.Log("[DoorPortal] OpenPanel");
    }

    private void ClosePanel()
    {
        if (confirmPanel == null) return;

        confirmPanel.SetActive(false);
        confirmOpen = false;
        Debug.Log("[DoorPortal] ClosePanel");
    }

    // Called by YES button
    public void OnConfirmYes()
    {
        Debug.Log("[DoorPortal] Confirm YES");
        LoadTargetScene();
    }

    // Called by NO button
    public void OnConfirmNo()
    {
        Debug.Log("[DoorPortal] Confirm NO");
        ClosePanel();
    }

    private void LoadTargetScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogWarning("[DoorPortal] No targetSceneName set on " + name);
            return;
        }

        Debug.Log("[DoorPortal] Loading scene: " + targetSceneName);
        SceneManager.LoadScene(targetSceneName);
    }
}

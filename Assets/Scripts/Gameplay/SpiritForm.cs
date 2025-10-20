using UnityEngine;

public class SpiritForm : MonoBehaviour
{
    [Header("Prefabs & Visuals")]
    public GameObject spiritPrefab;          // Assign a ghost prefab (below)
    public Color bodyColorInSpirit = Color.blue;

    [Header("Optional")]
    public MonoBehaviour playerMovementScript; // Drag your PlayerMovement script here

    private SpriteRenderer bodySR;
    private bool inSpiritForm = false;
    private GameObject spiritInstance;

    void Awake()
    {
        bodySR = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!inSpiritForm) EnterSpiritForm();
            else ExitSpiritForm();
        }
    }

    void EnterSpiritForm()
    {
        // Freeze the world
        Time.timeScale = 0f;

        // Lock the player's normal controls if you have them
        if (playerMovementScript) playerMovementScript.enabled = false;

        // Tint the body
        bodySR.color = bodyColorInSpirit;

        // Spawn spirit slightly offset
        Vector3 spawnPos = transform.position + new Vector3(0.4f, 0f, 0f);
        spiritInstance = Instantiate(spiritPrefab, spawnPos, Quaternion.identity);

        inSpiritForm = true;
    }

    void ExitSpiritForm()
    {
        // Unfreeze the world
        Time.timeScale = 1f;

        // Restore controls
        if (playerMovementScript) playerMovementScript.enabled = true;

        // Reset tint
        bodySR.color = Color.white;

        // Remove spirit
        if (spiritInstance) Destroy(spiritInstance);

        inSpiritForm = false;
    }
}

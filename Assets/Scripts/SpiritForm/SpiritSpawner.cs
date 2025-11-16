using UnityEngine;

public class SpiritSpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpiritFormController controller;

    [Header("Spirit Prefab")]
    [Tooltip("The ghost/spirit prefab to spawn while in SpiritActive.")]
    [SerializeField] private GameObject spiritPrefab;

    [Tooltip("Offset from the body where the spirit appears.")]
    [SerializeField] private Vector3 spawnOffset = new Vector3(0.6f, 0f, 0f);

    public GameObject CurrentSpirit { get; private set; }

    private void OnEnable()
    {
        if (controller != null)
        {
            controller.OnStateChanged += HandleStateChanged;
        }
        else
        {
            Debug.LogError("[SpiritSpawner] No SpiritFormController assigned!");
        }
    }

    private void OnDisable()
    {
        if (controller != null)
        {
            controller.OnStateChanged -= HandleStateChanged;
        }
    }

    private void HandleStateChanged(SpiritState newState)
    {
        if (newState == SpiritState.SpiritActive)
        {
            SpawnSpirit();
        }
        else
        {
            DespawnSpirit();
        }
    }

    private void SpawnSpirit()
    {
        if (CurrentSpirit != null) return;
        if (spiritPrefab == null)
        {
            Debug.LogError("[SpiritSpawner] No spiritPrefab assigned!");
            return;
        }

        Vector3 spawnPos = transform.position + spawnOffset;
        CurrentSpirit = Instantiate(spiritPrefab, spawnPos, Quaternion.identity);
    }

    private void DespawnSpirit()
    {
        if (CurrentSpirit == null) return;

        Destroy(CurrentSpirit);
        CurrentSpirit = null;
    }
}

using UnityEngine;

public class FamiliarSpawnAtPoint : MonoBehaviour
{
    [SerializeField] private Transform startPoint;

    private void Start()
    {
        if (startPoint != null)
        {
            transform.position = startPoint.position;
        }
        else
        {
            Debug.LogWarning("[FamiliarSpawnAtPoint] No startPoint set for " + name);
        }
    }
}

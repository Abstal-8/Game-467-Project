using UnityEngine;

public class PlayerSpawnAtPoint : MonoBehaviour
{
    private void Start()
    {
        if (string.IsNullOrEmpty(SceneSpawnManager.nextSpawnPointName))
        {
            Debug.Log("[PlayerSpawnAtPoint] No spawn point specified. Keeping default position.");
            return;
        }

        Debug.Log("[PlayerSpawnAtPoint] Looking for spawn point: " + SceneSpawnManager.nextSpawnPointName);

        GameObject spawnPoint = GameObject.Find(SceneSpawnManager.nextSpawnPointName);

        if (spawnPoint == null)
        {
            Debug.LogWarning("[PlayerSpawnAtPoint] Could not find spawn point: "
                             + SceneSpawnManager.nextSpawnPointName);
            return;
        }

        Debug.Log("[PlayerSpawnAtPoint] Moving player to: " + spawnPoint.transform.position);
        transform.position = spawnPoint.transform.position;
    }
}

using UnityEngine;

public class PlayerIdentities : MonoBehaviour
{
    public static PlayerIdentities Instance { get; private set; }

    void Awake()
    {
        // Ensure only one player exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Make player persist across scenes
    }
}

//Luke Bonniwell
// using System.Diagnostics;
// using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    [SerializeField]
    private LevelConnection _connection;

    [SerializeField]
    private string _targetSceneName;

    [SerializeField]
    private Transform _spawnPoint;
    private PlayerIdentities player;
    private SafeCrackUI safeCrackUI;

    private void Start()
    {
        if (_connection == LevelConnection.ActiveConnection)
        {
            player.transform.position = _spawnPoint.position;
        }
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find the persistent player
        if (player == null)
            player = PlayerIdentities.Instance;

        if (player == null)
        {
            Debug.LogError("Persistent player not found!");
            return;
        }

        // Find the LevelChanger that matches the ActiveConnection
        LevelChanger[] allChangers = FindObjectsOfType<LevelChanger>();
        foreach (var changer in allChangers)
        {
            if (changer._connection == LevelConnection.ActiveConnection && changer._spawnPoint != null)
            {
                player.transform.position = changer._spawnPoint.position;
                Debug.Log($"Player moved to spawn point: {changer._spawnPoint.position} in scene {scene.name}");
                break; // Only move to the correct LevelChanger's spawn
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var player = other.collider.GetComponent<PlayerIdentities>();
        if (player != null) {
            LevelConnection.ActiveConnection = _connection;
            // SceneSwitch switcher = FindFirstObjectByType<SceneSwitch>(); if "sceneswitch.instance" doesn't work replace all with "switcher" and uncomment this code
            if (SceneSwitch.instance != null)
            {
                if (_targetSceneName == "EndingScreen" && !safeCrackUI.cracked)
                {
                    Debug.Log("Key required to pass");
                    return;
                }
                SceneSwitch.instance.SetScene(_targetSceneName);
                SceneSwitch.instance.SwitchScene();
                UnityEngine.Debug.Log("Switched to scene: " + _targetSceneName);
            }
            else
            {
                UnityEngine.Debug.Log("ERROR manual override of sceneswitcher in levelchanger.cs");
                SceneManager.LoadScene(_targetSceneName);
            }
        }
    }
} 
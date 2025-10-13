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

    [SerializeField]
    private PlayerIdentities player;


//will start the plyaer on spawn point always. Please note findobjectoftype isn't the most
//efficient way, but it is the simplest for what we are trying to do.
    private void Start()
    {
        Debug.Log($"_connection:  {_connection}");
        Debug.Log($"ActiveConnection:  {_connection}");
        Debug.Log($"player:  {_connection}");
        Debug.Log($"_spawnPoint:  {_connection}");

        if (_connection == LevelConnection.ActiveConnection)
        {
            player.transform.position = _spawnPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        var player = other.collider.GetComponent<PlayerIdentities>();
        if (player != null) {
            LevelConnection.ActiveConnection = _connection;
            // SceneSwitch switcher = FindFirstObjectByType<SceneSwitch>(); if "sceneswitch.instance" doesn't work replace all with "switcher" and uncomment this code
            if (SceneSwitch.instance != null)
            {
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

//Luke Bonniwell Code
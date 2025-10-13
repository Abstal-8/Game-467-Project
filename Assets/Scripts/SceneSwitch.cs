using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public static SceneSwitch instance;
    [SerializeField] string sceneName;
    //for whatever object this is attached to, it will persist between scenes. 
    //in particular this will allow the object to continue, but if there are duplicated there won't be any errors. 
    //When we start scenes not from StartingScene I don't want to get errors for this object persisting
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // persist across scenes
        }
        else
        {
            Destroy(gameObject); // destroy duplicates
        }
    }
    public void SetScene(string name) //allows this function to be called in separate files and not just in inspector in unity
    {
        sceneName = name;
    }

    public void SwitchScene()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogWarning("SceneSwitch: No scene name set!");
        }
    }
}
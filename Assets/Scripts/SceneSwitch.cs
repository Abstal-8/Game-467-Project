using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    [SerializeField] string sceneName;

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
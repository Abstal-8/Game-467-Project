using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void SceneChange(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitApp()
    {
        Application.Quit();
    }

}

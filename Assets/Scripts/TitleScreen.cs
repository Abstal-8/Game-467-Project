using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    [Header("Scene to load when Play is pressed")]
    public string sceneToLoad = "StartingRoom"; // set this in Inspector

    public void OnPlayButtonPressed()
    {
        Debug.Log("PLAY BUTTON CLICKED");
        SceneManager.LoadScene(sceneToLoad);
    }
}

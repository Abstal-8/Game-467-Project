using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public GameObject player;
    public Camera mainCam;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(player);
            Destroy(mainCam);
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(mainCam);
        }
    }


}

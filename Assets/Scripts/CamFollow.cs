using UnityEngine;

public class CamFollow : MonoBehaviour
{

    private Camera _cam; 
    public GameObject player;

    Vector2 camFollow;
    Vector2 playerTarget;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame

    void Update()
    {
        camFollow = new(_cam.transform.position.x, _cam.transform.position.y);
        playerTarget = new(player.transform.position.x, player.transform.position.y);
    }
    void LateUpdate()
    {
        Vector2 camVelo = _cam.velocity;

        camFollow = Vector2.SmoothDamp(camFollow, playerTarget, ref camVelo, 0.001f);

        _cam.transform.position = new(camFollow.x, camFollow.y, _cam.transform.position.z);
        
    }
}

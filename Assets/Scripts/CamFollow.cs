using UnityEngine;

public class CamFollow : MonoBehaviour
{

    private Camera _cam; 
    public GameObject player;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector2 camVelo = _cam.velocity;
        _cam.transform.position = Vector2.SmoothDamp(_cam.transform.position, player.transform.position, ref camVelo, 0.001f);
    }
}

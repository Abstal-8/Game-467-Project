using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

public class SpiritController : MonoBehaviour
{
    public float speed = 4f;

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        //movement input
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) dir.x -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) dir.x += 1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) dir.y += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) dir.y -= 1f;

        if (dir.sqrMagnitude > 1f) dir.Normalize();

        //move the spirit
        Vector3 movement = dir * speed * Time.unscaledDeltaTime;
        transform.Translate(movement, Space.World);

        //Clamp spirit position within camera bounds
        ClampToCameraBounds();
    }

    void ClampToCameraBounds()
    {
        if (!mainCam) return;

        // Get world corners of camera view
        Vector3 min = mainCam.ViewportToWorldPoint(new Vector3(0, 0, transform.position.z - mainCam.transform.position.z));
        Vector3 max = mainCam.ViewportToWorldPoint(new Vector3(1, 1, transform.position.z - mainCam.transform.position.z));

        // Clamp position to those bounds
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);
        transform.position = pos;
    }
}

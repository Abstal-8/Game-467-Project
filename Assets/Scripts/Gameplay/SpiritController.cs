using UnityEngine;

public class SpiritController : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) dir.x -= 1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) dir.x += 1f;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) dir.y += 1f;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) dir.y -= 1f;

        if (dir.sqrMagnitude > 1f) dir.Normalize();
        transform.Translate(dir * speed * Time.unscaledDeltaTime);
    }
}

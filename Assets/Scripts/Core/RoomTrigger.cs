using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RoomTrigger : MonoBehaviour
{
    private RoomController room;

    void Awake()
    {
        room = GetComponentInParent<RoomController>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Spirit"))
        {
            room.PlayerEntered(gameObject);
        }
    }

}
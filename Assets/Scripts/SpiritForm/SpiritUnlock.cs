using UnityEngine;

public class SpiritUnlock : MonoBehaviour
{
    public SpiritForm player;           // drag Player (with SpiritForm) here
    public string spiritTag = "Spirit"; // tag set on the spirit prefab

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(spiritTag) && Input.GetKeyDown(KeyCode.E))
        {
            player.UnlockMovement();
            // optional: destroy this zone so it can't be used again
            Destroy(gameObject);
        }
    }
}

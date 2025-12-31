using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class RoomController : MonoBehaviour
{
    [SerializeField] 
    private TilemapRenderer[] covers;
    private bool isRevealed;
    private GameObject currRoom;

    void Awake()
    {
        gameObject.SetActive(true);
        isRevealed = false;
        if (covers == null || covers.Length == 0)
        {
            covers = GetComponentsInChildren<TilemapRenderer>();
        }
        currRoom = covers[0].gameObject;
        currRoom.GetComponent<TilemapRenderer>().enabled = false;
    }

    public void PlayerEntered(GameObject curr)
    {
        currRoom.GetComponent<TilemapRenderer>().enabled = true;
        currRoom = curr;
        currRoom.GetComponent<TilemapRenderer>().enabled = false;
    }

    void RevealRoom()
    {
        foreach (var cover in covers)
        {
            cover.enabled = false;
        }
    }
    void HideRoom()
    {
        foreach (var cover in covers)
        {
            cover.enabled = true;
        }
    }
}
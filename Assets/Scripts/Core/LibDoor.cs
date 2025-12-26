using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LibDoor : MonoBehaviour
{
    [Header("Canvas Object")]
    public GameObject hint;
    [Header("Light Object")]
    public Light2D glow;

    private SafeCrackUI safeCrackUI;

    [HideInInspector]
    public string playerTag = "Player";

    void Start()
    {
        if (!glow) Debug.Log("LibDoor Light Required");
        if (!hint) Debug.Log("LibDoor Hint Canvas Required (check children)");
        hint.SetActive(false);
        glow.enabled = false;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!safeCrackUI.cracked) hint.SetActive(true);
        if (other.CompareTag(playerTag))
        {
            if (glow != null)
            {
                glow.enabled = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!safeCrackUI.cracked) hint.SetActive(false);
        if (other.CompareTag(playerTag))
        {
            if (glow != null)
            {
                glow.enabled = false;
            }
        }
    }
}
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AutoPressESpritePrompt : MonoBehaviour
{
    public SpriteRenderer promptSprite;      // optional: assign existing child
    public Sprite promptSpriteImage;         // assign your E sprite
    public Vector3 offset = new Vector3(0f, 1f, 0f);
    public string playerTag = "Player";

    private void Start()
    {
        var col = GetComponent<Collider2D>();
        col.isTrigger = true;

        if (promptSprite == null)
        {
            GameObject go = new GameObject("PressE_SpritePrompt");
            go.transform.SetParent(transform);
            go.transform.localPosition = offset;

            promptSprite = go.AddComponent<SpriteRenderer>();
            promptSprite.sprite = promptSpriteImage;
            promptSprite.sortingOrder = 10;
        }

        promptSprite.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        promptSprite.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;
        promptSprite.enabled = false;
    }
}

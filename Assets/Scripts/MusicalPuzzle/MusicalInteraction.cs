using UnityEngine;
using TMPro;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class MusicalPadInteract : MonoBehaviour
{
    [Header("Puzzle")]
    public MusicalPuzzleManager puzzleManager;
    public int padId;                      // unique id for this pad

    [Header("Audio")]
    public AudioSource audioSource;        // will auto-fill
    public AudioClip noteClip;

    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;
    public TextMeshProUGUI promptText;

    [Header("Visual Feedback")]
    public GameObject glowObject;

    private bool inRange = false;

    public void SetGlow(bool on)
    {
        if (glowObject)
        {
            Debug.Log($"[Pad {padId}] Glow = {on}");
            glowObject.SetActive(on);
        }
    }

    private void Awake()
    {
        if (!audioSource)
            audioSource = GetComponent<AudioSource>();

        var col = GetComponent<Collider2D>();
        col.isTrigger = true;
    }

    private void Start()
    {
        if (promptText)
            promptText.gameObject.SetActive(false);

        // always start with glow off
        if (glowObject)
            glowObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        inRange = true;
        Debug.Log($"[Pad {padId}] Player entered trigger");

        if (promptText)
        {
            promptText.text = "Press E to play";
            promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag(playerTag)) return;

        inRange = false;
        Debug.Log($"[Pad {padId}] Player left trigger");

        if (promptText)
            promptText.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Debug.Log($"[Pad {padId}] E pressed, inRange={inRange}");

            if (!inRange) return;

            if (!audioSource || !noteClip)
                return;

            PlayNote();

            if (puzzleManager)
                puzzleManager.RegisterHit(this);   // 👈 pass the pad itself
        }
    }

    private void PlayNote()
    {
        Debug.Log($"[Pad {padId}] Playing note");
        audioSource.PlayOneShot(noteClip);
    }
}

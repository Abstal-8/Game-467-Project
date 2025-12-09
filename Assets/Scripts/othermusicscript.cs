using System.Collections;
using UnityEngine;
using TMPro;

public class SequentialMusicTeacher : MonoBehaviour
{
    [Header("Sources in order")]
    public AudioSource[] sources;

    [Header("Timings (seconds)")]
    public float pauseBetweenNotes = 1f;

    [Header("Interaction")]
    public string playerTag = "Player";
    public KeyCode interactKey = KeyCode.E;

    [Header("UI")]
    public GameObject promptUI;   // e.g., “Press E to Play”

    private bool inRange = false;
    private Coroutine playRoutine;

    private void Start()
    {
        if (promptUI)
            promptUI.SetActive(false);
    }

    private void Update()
    {
        // Manage prompt visibility
        UpdatePrompt();

        if (!inRange) return;

        // Only start if not already playing
        if (Input.GetKeyDown(interactKey) && playRoutine == null)
        {
            PlaySequenceOnce();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
            inRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            inRange = false;
            UpdatePrompt(); // hide immediately
        }
    }

    private void UpdatePrompt()
    {
        if (!promptUI) return;

        // Show prompt ONLY when:
        // - Player is in range
        // - Music is NOT playing
        bool shouldShow = inRange && playRoutine == null;

        promptUI.SetActive(shouldShow);
    }

    public void PlaySequenceOnce()
    {
        if (playRoutine != null) return;
        playRoutine = StartCoroutine(PlaySequence());
        UpdatePrompt(); // hide while playing
    }

    private IEnumerator PlaySequence()
    {
        foreach (var src in sources)
        {
            if (src == null || src.clip == null)
                continue;

            src.loop = false;
            src.Stop();
            src.time = 0f;

            src.Play();

            yield return new WaitForSeconds(src.clip.length + pauseBetweenNotes);
        }

        // Finished → allow another interaction
        playRoutine = null;

        UpdatePrompt(); // show prompt again if player is still nearby
    }
}

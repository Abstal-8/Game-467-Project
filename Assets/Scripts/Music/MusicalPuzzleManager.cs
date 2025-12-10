using UnityEngine;
using System.Collections;

public class MusicalPuzzleManager : MonoBehaviour
{
    [Header("Puzzle Setup")]
    [Tooltip("Order of padIds that solves the puzzle, e.g. [0,1,2,3]")]
    public int[] correctSequence;      // uses padId from each pad

    private MusicalPadInteract[] pads; // auto-filled
    private int currentStep = 0;
    private bool isSolved = false;

    [Header("Optional Feedback")]
    public AudioSource audioSource;
    public GameObject successPanel;
    public GameObject failPanel;
    public GameObject notificationPanel;

    [Header("What to unlock on success")]
    public GameObject doorToUnlock;
    public Collider2D colliderToDisable;
    public GameObject extraFX;
    public GameObject enemySpawn;

    private void Awake()
    {
        // find all pads in the scene so you don't have to assign them by hand
        pads = FindObjectsOfType<MusicalPadInteract>();
    }

    private void Start()
    {
        currentStep = 0;
        isSolved = false;
        ResetGlows();
    }

    public void RegisterHit(MusicalPadInteract pad)
    {
        if (isSolved) return;

        if (correctSequence == null || correctSequence.Length == 0)
        {
            Debug.LogWarning("[MusicalPuzzleManager] correctSequence not set.");
            return;
        }

        int padId = pad.padId;
        Debug.Log($"[Puzzle] Got pad {padId}, expected {correctSequence[currentStep]} at step {currentStep}");

        if (padId == correctSequence[currentStep])
        {
            // correct pad → turn its glow on
            pad.SetGlow(true);

            currentStep++;

            if (currentStep >= correctSequence.Length)
            {
                PuzzleSolved();
            }
        }
        else
        {
            // wrong pad → reset everything
            PuzzleFailed();
        }
    }

    private void ResetGlows()
    {
        if (pads == null) return;

        Debug.Log("[Puzzle] Resetting all glows");
        foreach (var p in pads)
        {
            if (p != null)
                p.SetGlow(false);
        }
    }

    private void PuzzleSolved()
    {
        isSolved = true;
        Debug.Log("[Puzzle] SOLVED!");
        StartCoroutine("Success");

        if (doorToUnlock)
            doorToUnlock.SetActive(false);

        if (colliderToDisable)
            colliderToDisable.enabled = false;

        if (extraFX)
            extraFX.SetActive(true);

        if (enemySpawn)
            enemySpawn.SetActive(true);
    }

    private void PuzzleFailed()
    {
        Debug.Log("[Puzzle] Wrong order, resetting.");
        currentStep = 0;
        StartCoroutine("Failure");
        ResetGlows();
    }

    IEnumerator Success()
    {
        successPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        successPanel.SetActive(false);

        notificationPanel.SetActive(true);
        yield return new WaitForSeconds(2f);
        notificationPanel.SetActive(false);
    }

    IEnumerator Failure()
    {
        failPanel.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        failPanel.SetActive(false);
    }
}
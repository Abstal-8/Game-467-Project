using System.Collections;
using UnityEngine;
using TMPro;

public class TipOnWake : MonoBehaviour
{
    [Header("Refs")]
    public SpiritForm spirit;                 // Drag the Player's SpiritForm here
    public CanvasGroup group;                 // CanvasGroup on this panel
    public TextMeshProUGUI tipText;           // The TMP text component

    [Header("Timing")]
    public float delayBeforeShow = 0.25f;     // small delay so it appears after your fade
    public float fadeIn = 0.2f;
    public float fadeOut = 0.2f;

    bool shown;

    void Awake()
    {
        if (!group) group = GetComponent<CanvasGroup>();
        if (group) group.alpha = 0f;          // start hidden
    }

    void OnEnable()
    {
        if (spirit) spirit.OnSpiritStateChanged += OnSpiritChanged;
    }
    void OnDisable()
    {
        if (spirit) spirit.OnSpiritStateChanged -= OnSpiritChanged;
    }

    void Start()
    {
        // Show the tip right after the scene finishes fading in
        StartCoroutine(ShowTipAfterDelay());
    }

    IEnumerator ShowTipAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShow);
        if (tipText) tipText.text = "Press T to enter spirit form";
        yield return StartCoroutine(FadeTo(1f, fadeIn));
        shown = true;
    }

    void OnSpiritChanged(bool entered)
    {
        if (!shown) return;
        if (entered) StartCoroutine(HideAndDisable());
    }

    IEnumerator HideAndDisable()
    {
        yield return StartCoroutine(FadeTo(0f, fadeOut));
        gameObject.SetActive(false); // remove the hint
    }

    IEnumerator FadeTo(float target, float dur)
    {
        if (!group || dur <= 0f) { if (group) group.alpha = target; yield break; }

        float start = group.alpha;
        float t = 0f;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;   // UI fade unaffected by pause/slow-mo
            group.alpha = Mathf.Lerp(start, target, t / dur);
            yield return null;
        }
        group.alpha = target;
    }
}

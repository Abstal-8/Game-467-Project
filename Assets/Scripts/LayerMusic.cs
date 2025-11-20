using UnityEngine;

public class LayeredRoomMusic : MonoBehaviour
{
    [Tooltip("All AudioSources that should loop in sync (piano, harp, flute, violin, etc.)")]
    public AudioSource[] layers;

    void Start()
    {
        if (layers == null || layers.Length == 0) return;

        // Start them all at the same dspTime so they loop in sync
        double startTime = AudioSettings.dspTime + 0.1f;

        foreach (var src in layers)
        {
            if (src == null) continue;
            src.loop = true;
            src.PlayScheduled(startTime);
        }
    }
}

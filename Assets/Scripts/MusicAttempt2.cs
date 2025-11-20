//using System.Collections;
//using UnityEngine;

//public class SequentialMusicTeacher : MonoBehaviour
//{
//    [Header("Sources in order")]
//    public AudioSource[] sources;          // 0 = Flute, 1 = Harp, 2 = Piano, 3 = Violin

//    [Header("Timings (seconds)")]
//    public float pauseBetweenNotes = 1f;   // gap between instruments
//    public float pauseBetweenLoops = 2f;   // gap after the full pattern

//    public bool playOnStart = true;

//    private Coroutine loopRoutine;

//    private void Start()
//    {
//        if (playOnStart)
//        {
//            StartTeaching();
//        }
//    }

//    public void StartTeaching()
//    {
//        if (loopRoutine != null)
//            StopCoroutine(loopRoutine);

//        loopRoutine = StartCoroutine(PlaySequenceLoop());
//    }

//    public void StopTeaching()
//    {
//        if (loopRoutine != null)
//        {
//            StopCoroutine(loopRoutine);
//            loopRoutine = null;
//        }
//    }

//    private IEnumerator PlaySequenceLoop()
//    {
//        while (true)
//        {
//            // go through the list: Flute → Harp → Piano → Violin
//            foreach (var src in sources)
//            {
//                if (src == null || src.clip == null)
//                    continue;

//                // make sure it starts from the beginning and is not looping
//                src.loop = false;
//                src.Stop();
//                src.time = 0f;

//                Debug.Log($"[Teacher] Playing {src.gameObject.name}");

//                src.Play();

//                // wait for this note to finish + a small pause
//                yield return new WaitForSeconds(src.clip.length + pauseBetweenNotes);
//            }

//            // pause before starting over
//            yield return new WaitForSeconds(pauseBetweenLoops);
//        }
//    }
//}

// using System.Drawing;
// using System.Numerics;
// using System.Runtime.CompilerServices;
// using System.Security;
// using System.Threading.Tasks.Dataflow;
using UnityEngine;

public class JournalDetector : MonoBehaviour
{
    [Header("Detector Features")]
    [SerializeField] private float detectDistance = 5;

    private NoteController _noteController;
    [SerializeField] private PlayerMovement player;

    // [Header("Crosshair")]
    // [SerializeField] private Image crosshair;

    [Header("Input Key")]
    [SerializeField] private KeyCode interactKey;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < detectDistance)
        {
            var nearbyNote = GetComponent<NoteController>();

            if (nearbyNote != null)
            {
                // _notecontroller = nearbyNote;
                _noteController.GlowNote();
            }
            else
            {
                ClearNote();
            }
        }
        else
        {
            ClearNote();
        }

        if (_noteController != null)
        {
            if (Input.GetKeyDown(interactKey))
            {
                _noteController.ShowNote();
                // _noteController.SetHighlight();
            }
        }
    }

    void ClearNote()
    {
        if (_noteController != null)
        {
            _noteController.GlowNote();
            _noteController = null;
        }
        else
        {
            // crosshair.color = Color.White;
        }
    }
}

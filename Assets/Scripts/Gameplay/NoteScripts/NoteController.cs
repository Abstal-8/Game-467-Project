using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
// using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Rendering.Universal;

public class NoteController : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private KeyCode closeKey;

    [Space(10)]
    [SerializeField] private PlayerMovement player;

    [Header("UI Text")]
    [SerializeField] private GameObject noteCanvas;
    [SerializeField] private TMP_Text noteTextAreaUI;

    [Space(10)]
    [SerializeField][TextArea] private string noteText;

    [Space(10)]
    [SerializeField][TextArea] private UnityEvent openEvent;
    private bool isOpen = false;
    [Header("Light Settings")]
    [SerializeField] private Light2D noteLight;
    private bool isLit = false;


    // public void SetHighlight()
    // {
    //     isLit = true;
    //     if (noteLight != null)
    //     {
    //         noteLight.enabled = active;
    //     }
    // }
    public void ShowNote()
    {
        noteTextAreaUI.text = noteText;
        noteCanvas.SetActive(true);
        openEvent.Invoke();
        DisablePlayer(true);
        isOpen = true;
    }

    public void GlowNote()
    {
        isLit = true;
        if (noteLight != null)
            {
                noteLight.enabled = isLit;
            }
    }

    void DisableNote()
    {
        noteCanvas.SetActive(false);
        DisablePlayer(false);
        isOpen = false;
    }

    void DisablePlayer(bool disable)
    {
        player.enabled = !disable;
    }

    private void Update()
    {
        if (isOpen)
        {
            if (Input.GetKeyDown(closeKey))
            {
                DisableNote();
            }
        }
    }
}

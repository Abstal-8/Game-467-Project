using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
// using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Rendering.Universal;

public class NoteInfoSwitcher : MonoBehaviour
{
    [Header("Page Objects")]
    [SerializeField] private TMP_Text page1Text;
    [SerializeField] private TMP_Text page2Text;
    [SerializeField] private TMP_Text page3Text;
    [SerializeField] private TMP_Text page4Text;

    [Header("Settings")]
    [SerializeField] private int maxCharacters = 333;
    private bool showingPage1;
    private bool showingPage2;
    private bool showingPage3;
    private bool showingPage4;

    private void Start()
    {
        if (page1Text != null) page1Text.gameObject.SetActive(true);
        if (page1Text != null) page2Text.gameObject.SetActive(false);
        if (page1Text != null) page3Text.gameObject.SetActive(false);
        if (page1Text != null) page4Text.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            NextPage();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            PrevPage();
        }
    }
    private void NextPage()
    {
        if (showingPage4) return;

        if (showingPage1)
        {
            if (page2Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(true);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                showingPage2 = true;
                showingPage1 = false;
            }
        }
        else if (showingPage2)
        {
            if (page3Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(true);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                showingPage2 = false;
                showingPage3 = true;
            }
        }
        else if (showingPage3)
        {
            if (page4Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(true);
                showingPage3 = false;
                showingPage4 = true;
            }
        }
    }
    private void PrevPage()
    {
        if (showingPage1) return;

        if (showingPage2)
        {
            if (page1Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(true);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                showingPage1 = true;
                showingPage2 = false;
            }
        }
        else if (showingPage3)
        {
            if (page2Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(true);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                showingPage2 = true;
                showingPage3 = false;
            }
        }
        else if (showingPage4)
        {
            if (page3Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(true);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                showingPage3 = true;
                showingPage4 = false;
            }
        }
    }
}
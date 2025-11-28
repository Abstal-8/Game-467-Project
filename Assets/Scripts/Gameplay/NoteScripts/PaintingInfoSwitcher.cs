using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
// using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.Rendering.Universal;

public class PaintingInfoSwitcher : MonoBehaviour
{
    [Header("Page Objects")]
    [SerializeField] private TMP_Text page1Text;
    [SerializeField] private TMP_Text page2Text;
    [SerializeField] private TMP_Text page3Text;
    [SerializeField] private TMP_Text page4Text;
    [SerializeField] private TMP_Text page5Text;
    [SerializeField] private TMP_Text page6Text;
    [SerializeField] private TMP_Text page7Text;
    [SerializeField] private TMP_Text page8Text;

    private bool showingPage1;
    private bool showingPage2;
    private bool showingPage3;
    private bool showingPage4;
    private bool showingPage5;
    private bool showingPage6;
    private bool showingPage7;
    private bool showingPage8;

    private void Start()
    {
        if (page1Text != null) page1Text.gameObject.SetActive(false);
        if (page2Text != null) page2Text.gameObject.SetActive(false);
        if (page3Text != null) page3Text.gameObject.SetActive(false);
        if (page4Text != null) page4Text.gameObject.SetActive(false);
        if (page5Text != null) page5Text.gameObject.SetActive(false);
        if (page6Text != null) page6Text.gameObject.SetActive(false);
        if (page7Text != null) page7Text.gameObject.SetActive(false);
        if (page8Text != null) page8Text.gameObject.SetActive(false);
        showingPage1 = true;
        showingPage2 = false;
        showingPage3 = false;
        showingPage4 = false;
        showingPage5 = false;
        showingPage6 = false;
        showingPage7 = false;
        showingPage8 = false;
    }
    public void Show()
    {
        if (page1Text != null) page1Text.gameObject.SetActive(true);
        if (page2Text != null) page2Text.gameObject.SetActive(false);
        if (page3Text != null) page3Text.gameObject.SetActive(false);
        if (page4Text != null) page4Text.gameObject.SetActive(false);
        if (page5Text != null) page5Text.gameObject.SetActive(false);
        if (page6Text != null) page6Text.gameObject.SetActive(false);
        if (page7Text != null) page7Text.gameObject.SetActive(false);
        if (page8Text != null) page8Text.gameObject.SetActive(false);
        showingPage1 = true;
        showingPage2 = false;
        showingPage3 = false;
        showingPage4 = false;
        showingPage5 = false;
        showingPage6 = false;
        showingPage7 = false;
        showingPage8 = false;
    }
    public void Hide()
    {
        if (page1Text != null) page1Text.gameObject.SetActive(false);
        if (page1Text != null) page2Text.gameObject.SetActive(false);
        if (page1Text != null) page3Text.gameObject.SetActive(false);
        if (page1Text != null) page4Text.gameObject.SetActive(false);
        if (page1Text != null) page5Text.gameObject.SetActive(false);
        if (page1Text != null) page6Text.gameObject.SetActive(false);
        if (page1Text != null) page7Text.gameObject.SetActive(false);
        if (page1Text != null) page8Text.gameObject.SetActive(false);
        showingPage1 = false;
        showingPage2 = false;
        showingPage3 = false;
        showingPage4 = false;
        showingPage5 = false;
        showingPage6 = false;
        showingPage7 = false;
        showingPage8 = false;
    }
    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         NextPage();
    //     }
    //     else if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         PrevPage();
    //     }
    // }
    public void NextPage()
    {
        if (showingPage8) return;

        if (showingPage1)
        {
            if (page2Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(true);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                if (page5Text != null) page5Text.gameObject.SetActive(false);
                if (page6Text != null) page6Text.gameObject.SetActive(false);
                if (page7Text != null) page7Text.gameObject.SetActive(false);
                if (page8Text != null) page8Text.gameObject.SetActive(false);
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
                if (page5Text != null) page5Text.gameObject.SetActive(false);
                if (page6Text != null) page6Text.gameObject.SetActive(false);
                if (page7Text != null) page7Text.gameObject.SetActive(false);
                if (page8Text != null) page8Text.gameObject.SetActive(false);
                showingPage3 = true;
                showingPage2 = false;
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
                if (page5Text != null) page5Text.gameObject.SetActive(false);
                if (page6Text != null) page6Text.gameObject.SetActive(false);
                if (page7Text != null) page7Text.gameObject.SetActive(false);
                if (page8Text != null) page8Text.gameObject.SetActive(false);
                showingPage4 = true;
                showingPage3 = false;
            }
        }
        else if (showingPage4)
        {
            if (page5Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                if (page5Text != null) page5Text.gameObject.SetActive(true);
                if (page6Text != null) page6Text.gameObject.SetActive(false);
                if (page7Text != null) page7Text.gameObject.SetActive(false);
                if (page8Text != null) page8Text.gameObject.SetActive(false);
                showingPage5 = true;
                showingPage4 = false;
            }
        }
        else if (showingPage5)
        {
            if (page6Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                if (page5Text != null) page5Text.gameObject.SetActive(false);
                if (page6Text != null) page6Text.gameObject.SetActive(true);
                if (page7Text != null) page7Text.gameObject.SetActive(false);
                if (page8Text != null) page8Text.gameObject.SetActive(false);
                showingPage6 = true;
                showingPage5 = false;
            }
        }
        else if (showingPage6)
        {
            if (page7Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                if (page5Text != null) page5Text.gameObject.SetActive(false);
                if (page6Text != null) page6Text.gameObject.SetActive(false);
                if (page7Text != null) page7Text.gameObject.SetActive(true);
                if (page8Text != null) page8Text.gameObject.SetActive(false);
                showingPage7 = true;
                showingPage6 = false;
            }
        }
        else if (showingPage7)
        {
            if (page8Text == null) return;
            else
            {
                if (page1Text != null) page1Text.gameObject.SetActive(false);
                if (page2Text != null) page2Text.gameObject.SetActive(false);
                if (page3Text != null) page3Text.gameObject.SetActive(false);
                if (page4Text != null) page4Text.gameObject.SetActive(false);
                if (page5Text != null) page5Text.gameObject.SetActive(false);
                if (page6Text != null) page6Text.gameObject.SetActive(false);
                if (page7Text != null) page7Text.gameObject.SetActive(false);
                if (page8Text != null) page8Text.gameObject.SetActive(true);
                showingPage8 = true;
                showingPage7 = false;
            }
        }
    }

//     public void PrevPage()
//     {
//         if (showingPage1) return;

//         if (showingPage2)
//         {
//             if (page1Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(true);
//                 if (page2Text != null) page2Text.gameObject.SetActive(false);
//                 if (page3Text != null) page3Text.gameObject.SetActive(false);
//                 if (page4Text != null) page4Text.gameObject.SetActive(false);
//                 if (page5Text != null) page5Text.gameObject.SetActive(false);
//                 if (page6Text != null) page6Text.gameObject.SetActive(false);
//                 if (page7Text != null) page7Text.gameObject.SetActive(false);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage1 = true;
//                 showingPage2 = false;
//             }
//         }
//         else if (showingPage3)
//         {
//             if (page2Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(false);
//                 if (page2Text != null) page2Text.gameObject.SetActive(true);
//                 if (page3Text != null) page3Text.gameObject.SetActive(false);
//                 if (page4Text != null) page4Text.gameObject.SetActive(false);
//                 if (page5Text != null) page5Text.gameObject.SetActive(false);
//                 if (page6Text != null) page6Text.gameObject.SetActive(false);
//                 if (page7Text != null) page7Text.gameObject.SetActive(false);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage2 = true;
//                 showingPage3 = false;
//             }
//         }
//         else if (showingPage4)
//         {
//             if (page3Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(false);
//                 if (page2Text != null) page2Text.gameObject.SetActive(false);
//                 if (page3Text != null) page3Text.gameObject.SetActive(true);
//                 if (page4Text != null) page4Text.gameObject.SetActive(false);
//                 if (page5Text != null) page5Text.gameObject.SetActive(false);
//                 if (page6Text != null) page6Text.gameObject.SetActive(false);
//                 if (page7Text != null) page7Text.gameObject.SetActive(false);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage3 = true;
//                 showingPage4 = false;
//             }
//         }
//         else if (showingPage5)
//         {
//             if (page4Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(false);
//                 if (page2Text != null) page2Text.gameObject.SetActive(false);
//                 if (page3Text != null) page3Text.gameObject.SetActive(false);
//                 if (page4Text != null) page4Text.gameObject.SetActive(true);
//                 if (page5Text != null) page5Text.gameObject.SetActive(false);
//                 if (page6Text != null) page6Text.gameObject.SetActive(false);
//                 if (page7Text != null) page7Text.gameObject.SetActive(false);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage4 = true;
//                 showingPage5 = false;
//             }
//         }
//         else if (showingPage6)
//         {
//             if (page5Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(false);
//                 if (page2Text != null) page2Text.gameObject.SetActive(false);
//                 if (page3Text != null) page3Text.gameObject.SetActive(false);
//                 if (page4Text != null) page4Text.gameObject.SetActive(false);
//                 if (page5Text != null) page5Text.gameObject.SetActive(true);
//                 if (page6Text != null) page6Text.gameObject.SetActive(false);
//                 if (page7Text != null) page7Text.gameObject.SetActive(false);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage5 = true;
//                 showingPage6 = false;
//             }
//         }
//         else if (showingPage7)
//         {
//             if (page6Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(false);
//                 if (page2Text != null) page2Text.gameObject.SetActive(false);
//                 if (page3Text != null) page3Text.gameObject.SetActive(false);
//                 if (page4Text != null) page4Text.gameObject.SetActive(false);
//                 if (page5Text != null) page5Text.gameObject.SetActive(false);
//                 if (page6Text != null) page6Text.gameObject.SetActive(true);
//                 if (page7Text != null) page7Text.gameObject.SetActive(false);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage6 = true;
//                 showingPage7 = false;
//             }
//         }
//         else if (showingPage8)
//         {
//             if (page7Text == null) return;
//             else
//             {
//                 if (page1Text != null) page1Text.gameObject.SetActive(false);
//                 if (page2Text != null) page2Text.gameObject.SetActive(false);
//                 if (page3Text != null) page3Text.gameObject.SetActive(false);
//                 if (page4Text != null) page4Text.gameObject.SetActive(false);
//                 if (page5Text != null) page5Text.gameObject.SetActive(false);
//                 if (page6Text != null) page6Text.gameObject.SetActive(false);
//                 if (page7Text != null) page7Text.gameObject.SetActive(true);
//                 if (page8Text != null) page8Text.gameObject.SetActive(false);
//                 showingPage7 = true;
//                 showingPage8 = false;
//             }
//         }
//     }
}
using System.Collections;
using UnityEngine;

public class IntroDialogueStarter : MonoBehaviour
{
    public PopupManager popupManager;
    public TextAsset introInkJSON;

    IEnumerator Start()
    {
        // Wait one frame so all other Start() methods (like PopupManager) have run
        yield return null;

        if (popupManager == null)
        {
            Debug.LogError("IntroDialogueStarter: popupManager is not assigned!");
            yield break;
        }

        if (introInkJSON == null)
        {
            Debug.LogError("IntroDialogueStarter: introInkJSON is not assigned!");
            yield break;
        }

        Debug.Log("IntroDialogueStarter: starting intro dialogue.");
        popupManager.InitializeDialogue(introInkJSON);
        popupManager.StartDialogue();
    }
}

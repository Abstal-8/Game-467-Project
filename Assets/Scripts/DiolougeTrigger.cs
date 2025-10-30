using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueLines;

    private bool hasPlayed = false;

    public void TriggerDialogue()
    {
        if (hasPlayed) return;
        hasPlayed = true;

        SimpleDialogueManager.Instance.StartDialogue(dialogueLines);
    }
}

using UnityEngine;
using System;

[Serializable]
public class NewDialogueEntry
{
    [TextArea(3, 5)] 
    public string line;
    public int speakerIndex; 
}
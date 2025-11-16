using UnityEngine;
using System;

// Data structure to hold one line of dialogue and who is speaking.
[Serializable]
public class NPCData
{
    [TextArea(3, 5)] 
    public string line;
    [Tooltip("Index corresponding to the character's portrait in the Manager's Sprite array.")]
    public int speakerIndex; 
}
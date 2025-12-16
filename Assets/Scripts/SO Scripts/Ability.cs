using System;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Abilities", menuName = "ScriptableObjects/Ability", order = 3)]
public class Ability : ScriptableObject
{
    public string abilityName;
    [TextArea]
    public string abilityDescription;

    [SerializeField]
    public int abilityDMG;

    // Value to set in inspector
    [SerializeField]
    public int baseDMG;

    void OnValidate()
    {
        abilityDMG = baseDMG;
    }


}

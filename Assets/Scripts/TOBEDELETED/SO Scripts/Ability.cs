using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;


[System.Serializable]
[CreateAssetMenu(fileName = "Ability", menuName = "ScriptableObjects/Abilities", order = 3)]
public class Ability : ScriptableObject
{
    public string abilityName;
    public Element abilityType;
    public int currentCooldown;
    public AbilityStats abilityStats;

    [TextArea]
    public string abilityDescription;
}
public class AbilityStats
{
    public int baseDamage;
    public int baseCooldown;
    //TODO: add more stats for abilities
}

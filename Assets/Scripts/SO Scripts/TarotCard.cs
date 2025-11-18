using UnityEngine;




[CreateAssetMenu(fileName = "Tarot Card", menuName = "ScriptableObjects/Tarot Card", order = 2)]
public class TarotCard : ScriptableObject
{
    // TODO:
    // Could need variable for more detailed card art
    // public List<Ability> cardAbilities;

    public BaseCardStats cardStats;
    public Sprite cardSprite;
    public string cardName;
    public Element baseType;
    public bool _currentlyEquipped;

    [TextArea]
    public string cardDescription;
}

public class BaseCardStats
{
    public int health;
    public int baseDamage;
    public int baseDefense;
    public Element elementResistance;
    public Element elementWeakness;
    public int elementDamBonus;
    public int elementDamReduc;
    public int elementDefBonus;
    public int elementDefReduc;
}

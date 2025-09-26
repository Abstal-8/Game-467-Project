using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    /*

    
    public List<TarotCard> Tarot_cards;

    
    */
    public List<Item> Key_items = new List<Item>();
    public Action onAddToInv;

    public void AddToInventory(Item item)
    {
        if (item != null)
        {
            Key_items.Add(item);
        }
        onAddToInv?.Invoke();
    }

    

}

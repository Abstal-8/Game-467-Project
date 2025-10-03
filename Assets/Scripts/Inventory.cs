using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Inventory", menuName = "ScriptableObjects/Inventory", order = 1)]
public class Inventory : ScriptableObject
{
    /*

    
    public List<TarotCard> Tarot_cards;

    
    */

    
    public Action onAddToInv;

       public List<Item> Key_items;



    public void AddToInventory(Item item)
    {
        if (item != null && !item.inInventory)
        {
            Key_items.Add(item);
            Debug.Log("[Inventory] Added item: " + item.itemName);
        }
    }

    public bool HasItem(Item item)
    {
        return Key_items.Contains(item);
    }

    

}

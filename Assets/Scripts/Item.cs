using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 0)]
public class Item : ScriptableObject
{
    public Inventory inventory;
    public GameObject itemPrefab;
    public Sprite itemSprite;
    public String itemName;
    public bool _isKeyitem;
    public bool _inInventory;


    // private bool _isUsed; Consumables and other usable items
    // Potentially could add: animation, sound, ... 
    

    public virtual void Use()
    {
        Debug.Log("Item has been used!");
    }

    public void Equip()
    {
        inventory.onAddToInv += Equip;
        inventory.AddToInventory(this);

        _inInventory = true;
        inventory.onAddToInv -= Equip;

    }

}

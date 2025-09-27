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
    public bool isKeyitem;
    public bool inInventory;


    // private bool _isUsed; Consumables and other usable items
    // Potentially could add: animation, sound, ... 

    void OnEnable()
    {
        inventory.onAddToInv += InvCheck;
    }

    void OnDisable()
    {
        inventory.onAddToInv -= InvCheck;
    }

    public virtual void Use()
    {
        Debug.Log("Item has been used!");
    }

    public void Equip()
    {
        inventory.AddToInventory(this);
    }

    private void InvCheck()
    {
        inInventory = true;
    }

}

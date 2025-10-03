using System;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObjects/Item", order = 0)]
public class Item : ScriptableObject
{
    public GameObject itemPrefab;
    public Sprite itemSprite;
    public String itemName;
    public bool isKeyitem;
    public bool inInventory;


    // private bool _isUsed; Consumables and other usable items
    // Potentially could add: animation, sound, ... 

    // TESTING PURPOSES ONLY
    void OnApplicationQuit()
    {
        inInventory = false;
    }


}

using System;
using System.Collections;
using UnityEngine;

public class ScriptableObjectManager : MonoBehaviour
{

    // Script is currently going to be used for testing purposes

    // add singleton/instance


    [SerializeField] Inventory inventorySO;
    [SerializeField] Item itemSO;
    [SerializeField] bool _performDataClear;

    void OnApplicationQuit()
    {
        if (_performDataClear)
        {
            inventorySO.InventoryReset();
            itemSO.ItemReset();
        }
    }

}

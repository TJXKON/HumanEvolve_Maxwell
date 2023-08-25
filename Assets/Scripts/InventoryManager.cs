using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private List<string> inventory = new List<string>();

    public void AddKeyToInventory()
    {
       
        string keyIdentifier = "Key";

        if (!inventory.Contains(keyIdentifier))
        {
            inventory.Add(keyIdentifier);
            Debug.Log("Key added to inventory.");
        }
        else
        {
            Debug.Log("Key is already in inventory.");
        }
    }

}
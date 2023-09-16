using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
     public static InventoryManager instance;

    private List<GameObject> inventory = new List<GameObject>();

    private void Awake()
{
    if (instance == null)
    {
        instance = this;
        inventory = new List<GameObject>(); // Initialize the inventory list
    }
    else
    {
        Destroy(gameObject);
    }
}

    public void AddToInventory(GameObject item)
    {
        inventory.Add(item);
        item.SetActive(false); // Deactivate the item in the scene
        Debug.Log(item.name + " added to inventory.");
    }

    public bool HasKey()
{
    foreach (GameObject item in inventory)
    {
        if (item.CompareTag("Key")) 
        {
            Debug.Log("HasKey : true");
            return true;
        }
    }
    Debug.Log("HasKey : false");
    return false;
}

}
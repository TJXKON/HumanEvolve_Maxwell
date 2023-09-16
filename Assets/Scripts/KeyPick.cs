using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPick : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("Player"))
    {
        HandleKeyPickup();
    }
}

private void HandleKeyPickup()
{
    InventoryManager.instance.AddToInventory(gameObject); // Add key to inventory
}
}

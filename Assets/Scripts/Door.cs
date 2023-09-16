using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
     public GameObject lockedIndicator; // An object indicating that the door is locked
    public GameObject unlockedIndicator; // An object indicating that the door is unlocked

    private bool isLocked = true;

    private void Start()
    {
        UpdateIndicators();
    }

    public void TryOpenDoor()
{
    Debug.Log("Trying to open door. Locked: " + isLocked + " HasKey: " + InventoryManager.instance.HasKey());
    if (isLocked && InventoryManager.instance.HasKey())
    {
        isLocked = false;
        UpdateIndicators();
        Debug.Log("Door unlocked and opened.");
        // Implement any logic to transition to the next stage or level here
    }
    else
    {
        Debug.Log("Door is locked.");
    }
}

    private void UpdateIndicators()
    {
        lockedIndicator.SetActive(isLocked);
        unlockedIndicator.SetActive(!isLocked);
    }
}

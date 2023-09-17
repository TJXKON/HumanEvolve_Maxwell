using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
     public GameObject lockedIndicator; 
    public GameObject unlockedIndicator; 

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

        // Load the next scene here
        LoadNextScene();
    }
    else
    {
        Debug.Log("Door is locked.");
    }
}
private void LoadNextScene()
{
    // Load the scene named "Boss Room"
    SceneManager.LoadScene("Boss Room");
}

    private void UpdateIndicators()
    {
        lockedIndicator.SetActive(isLocked);
        unlockedIndicator.SetActive(!isLocked);
    }


}

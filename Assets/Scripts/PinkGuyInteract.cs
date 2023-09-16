using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinkGuyInteract : MonoBehaviour
{
    public GameObject keyObject; // Reference to the key GameObject
    private bool hasKey;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasKey)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpKey();
            }
        }
    }

    private void PickUpKey()
    {
        hasKey = true;
        keyObject.SetActive(false); // Deactivate the key GameObject
       
    }
}

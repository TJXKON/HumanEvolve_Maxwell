using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformButton : MonoBehaviour
{
     public ControlPlatform platform; 

    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            platform.MoveLeft(false); 
            platform.MoveRight(true); // Move the platform right
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPressed)
        {
            isPressed = false;
            platform.MoveRight(false); // Stop moving the platform right
        }
    }
}

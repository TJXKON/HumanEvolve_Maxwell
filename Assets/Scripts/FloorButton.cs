using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorButton : MonoBehaviour
{
      public ControlPlatform platform; // Reference to the platform script

    private bool isPressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPressed)
        {
            isPressed = true;
            platform.MoveLeft(true); // Move the platform left
            platform.MoveRight(false); // Ensure the platform isn't moving right
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isPressed)
        {
            isPressed = false;
            platform.MoveLeft(false); // Stop moving the platform left
        }
    }
}

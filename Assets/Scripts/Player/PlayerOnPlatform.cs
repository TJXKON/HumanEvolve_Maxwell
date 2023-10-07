using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnPlatform : MonoBehaviour
{
    private bool isOnPlatform = false;
    private Transform platform;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {     
            platform = collision.transform;
            transform.SetParent(platform);

            isOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {     
            transform.SetParent(null);
 
            isOnPlatform = false;
        }
    }

    
    private void FixedUpdate()
    {
        if (isOnPlatform)
        {
            Vector3 relativePosition = transform.position - platform.position;

            transform.position = platform.position + relativePosition;
        }
    }
}

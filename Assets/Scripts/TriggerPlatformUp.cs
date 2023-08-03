using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerPlatformUp : MonoBehaviour
   {
    public float moveSpeed = 3f;
    public float maxDistance = 5f; 

    private bool playerOnPlatform = false;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        if (playerOnPlatform)
        {
            MovePlatformUp();
        }
    }

    private void MovePlatformUp()
    {
        float distance = transform.position.y - initialPosition.y;
        if (distance < maxDistance)
        {
           
            Vector3 newPosition = transform.position + Vector3.up * moveSpeed * Time.deltaTime;
            transform.position = newPosition;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
        }
    }
}

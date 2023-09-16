using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlatform : MonoBehaviour
{
   public float floatDistance = 1.0f;
    public float floatSpeed = 1.0f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timer;
    private bool isPlayerOnPlatform = false;
    private Transform playerTransform;

    private bool moveLeft = false;
    private bool returnToStart = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition; 
    }

    void Update()
    {
        if (returnToStart)
        {
            // Move the platform back to the start position
            float step = floatSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);

            if (transform.position == startPosition)
            {
                returnToStart = false;
            }
        }
        else if (moveLeft)
        {
            timer += Time.deltaTime * floatSpeed;
            float xPosition = startPosition.x + Mathf.Sin(timer) * floatDistance;
            Vector3 newPosition = new Vector3(xPosition, transform.position.y, transform.position.z);
            transform.position = newPosition;

            if (isPlayerOnPlatform && playerTransform != null)
            {
                Vector3 playerRelativePosition = playerTransform.position - transform.position;
                playerTransform.position = newPosition + playerRelativePosition;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = true;
            playerTransform = collision.transform;
            playerTransform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerOnPlatform = false;
            playerTransform.SetParent(null);
        }
    }

    public void MoveLeft(bool shouldMoveLeft)
    {
        moveLeft = shouldMoveLeft;

        // If the button on the platform is pressed, return to the start position
        if (!moveLeft)
        {
            returnToStart = true;
        }
    }
}

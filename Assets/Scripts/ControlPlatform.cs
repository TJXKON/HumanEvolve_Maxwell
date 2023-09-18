using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPlatform : MonoBehaviour
{
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    public float moveSpeed = 5.0f; 
    public float moveDistance = 5.0f; 

    private float initialPositionX; 
    private float currentDistance = 0.0f; 

    
    private Transform playerTransform;

    private void Start()
    {
        initialPositionX = transform.position.x;
    }

    private void Update()
    {
        if (isMovingLeft)
        {
            MoveLeft();
        }
        else if (isMovingRight)
        {
            MoveRight();
        }

        // Check if the platform has moved the desired distance
        if (Mathf.Abs(transform.position.x - initialPositionX) >= moveDistance)
        {
            // Stop the platform
            isMovingLeft = false;
            isMovingRight = false;
        }

        // Check if the player is on the platform and update their position
        if (playerTransform != null)
        {
            Vector3 playerRelativePosition = playerTransform.position - transform.position;
            playerTransform.position = transform.position + playerRelativePosition;
        }
    }

    public void MoveLeft()
    {
        // Move the platform to the left
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        currentDistance += moveSpeed * Time.deltaTime;
    }

    public void MoveRight()
    {
        // Move the platform to the right
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        currentDistance += moveSpeed * Time.deltaTime;
    }

    public void MoveLeft(bool move)
    {
        isMovingLeft = move;
    }

    public void MoveRight(bool move)
    {
        isMovingRight = move;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            playerTransform = collision.transform;
            playerTransform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            playerTransform.SetParent(null);
            playerTransform = null;
        }
    }
}
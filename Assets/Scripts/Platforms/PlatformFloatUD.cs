using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFloatUD : MonoBehaviour
{
   public float floatDistance = 1.0f;
public float floatSpeed = 1.0f;

private Vector3 startPosition;
private float timer;
private bool isPlayerOnPlatform = false;
private Transform playerTransform;

void Start()
{
    startPosition = transform.position;
}

void Update()
{
    timer += Time.deltaTime * floatSpeed;
    float yPosition = startPosition.y + Mathf.Sin(timer) * floatDistance;
    Vector3 newPosition = new Vector3(transform.position.x, yPosition, transform.position.z);

   
    transform.position = newPosition;

    
    if (isPlayerOnPlatform && playerTransform != null)
    {
        Vector3 playerRelativePosition = playerTransform.position - transform.position;
        playerTransform.position = newPosition + playerRelativePosition;
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fatbird : MonoBehaviour
  {
    public float flySpeed = 10.0f;
    public float dropSpeed = 5.0f;
    public float dropHeight = 5.0f;
    public int damageAmount = 10;

    private Vector3 originalPosition;
    private bool isFlying = false;

    private void Start()
    {
        originalPosition = transform.position;
        StartFlying(); // Start flying when the game starts
    }

    private void Update()
    {
        if (isFlying)
        {
            // Move the trap upwards
            transform.Translate(Vector3.up * flySpeed * Time.deltaTime);

            // Check if the trap has reached the desired height
            if (transform.position.y >= originalPosition.y + dropHeight)
            {
                // Drop the trap back down
                transform.Translate(Vector3.down * dropSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isFlying && other.CompareTag("Player"))
        {
            // Check if the player has invincibility frames
            PlayerStatusManager playerStatusManager = other.GetComponent<PlayerStatusManager>();
            if (playerStatusManager != null && !playerStatusManager.Iframe)
            {
                // Damage the player and activate their invincibility frames
                playerStatusManager.takeDamage(damageAmount);

                // Destroy the trap
                Destroy(gameObject);
            }
        }
    }

    // Call this method to start the flying behavior
    public void StartFlying()
    {
        isFlying = true;
    }
}

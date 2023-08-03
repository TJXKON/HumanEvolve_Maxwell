using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
     public float activeDuration = 1f; // The duration the trap is active in seconds
    public float inactiveDuration = 2f; // The duration the trap is inactive in seconds
    public int damageAmount = 20; // Amount of damage dealt to the player when active

    private Animator animator;
    private BoxCollider boxCollider;

    private bool isActive = false;
    private float timer = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        // Timer for switching between active and inactive states
        timer += Time.deltaTime;

        if (isActive)
        {
            // Check if the active duration is over
            if (timer >= activeDuration)
            {
                // Deactivate the trap
                DeactivateTrap();
            }
        }
        else
        {
            // Check if the inactive duration is over
            if (timer >= inactiveDuration)
            {
                // Activate the trap
                ActivateTrap();
            }
        }
    }

    void ActivateTrap()
    {
        isActive = true;
        timer = 0f;
        animator.SetTrigger("Activate");
        boxCollider.enabled = true;
    }

    void DeactivateTrap()
    {
        isActive = false;
        timer = 0f;
        animator.SetTrigger("Deactivate");
        boxCollider.enabled = false;
    }

     private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>();

             Debug.Log("Player hit by fire.");
            if (playerStatus != null)
            {
                playerStatus.health -= damageAmount;
            }
        }
    }
}

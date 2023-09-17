using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
      public int damageAmount = 10;
    public float knockbackForce = 40f;
    public float hitCooldown = 0.5f; // Cooldown period in seconds
    public string collisionMessage = "Player collided with spike.";


    private float lastHitTime = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastHitTime + hitCooldown)
        {
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>();

            Debug.Log(collisionMessage);
            if (playerStatus != null)
            {
                playerStatus.currentHP -= damageAmount;

                Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }

                lastHitTime = Time.time;
            }
        }
    }
   
}

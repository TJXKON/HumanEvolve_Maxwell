using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHeadUD : MonoBehaviour
{
    public int damageAmount = 10;
    public float knockbackForce = 20f;
    public float hitCooldown = 0.5f; // Cooldown time in seconds

    private bool canDamage = true; // Flag to check if the enemy can damage the player
    private float lastHitTime; // Timestamp of the last hit

    public float moveSpeed = 2f;
    public float moveDistance = 2f;
    public float pauseDuration = 0.5f;

    private Vector3[] moveDirections; // Array of movement directions
    private int directionIndex = 0; // Index of the current direction

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canDamage)
        {
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>();

            Debug.Log("Player collided with spikehead.");
            if (playerStatus != null)
            {
                playerStatus.currentHP -= damageAmount;

                // Apply knockback force to the player's Rigidbody
                Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }

                canDamage = false;
                lastHitTime = Time.time;
                StartCoroutine(ResetCooldown());
            }
        }
    }

    private IEnumerator ResetCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        canDamage = true;
    }

    private void Start()
    {
        moveDirections = new Vector3[] {
            Vector3.up, Vector3.down
        };

        StartCoroutine(MovementSequence());
    }

    IEnumerator MovementSequence()
    {
        while (true)
        {
            Vector3 startPosition = transform.position;
            Vector3 targetPosition = startPosition + moveDirections[directionIndex] * moveDistance;

            float startTime = Time.time;

            while (Time.time < startTime + (moveDistance / moveSpeed))
            {
                float normalizedTime = (Time.time - startTime) * moveSpeed / moveDistance;
                transform.position = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
                yield return null;
            }

            transform.position = targetPosition;

            // Pause briefly between movements
            yield return new WaitForSeconds(pauseDuration);

            // Move to the next direction
            directionIndex = (directionIndex + 1) % moveDirections.Length;
        }
    }
}

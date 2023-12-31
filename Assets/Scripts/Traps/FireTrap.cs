using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public float activeDuration = 1f;
    public float inactiveDuration = 2f;

    public int damageAmount = 10;

    private Animator anim;
    private BoxCollider boxCollider;
    private bool isActive = false;
    private float timer = 0f;
    public float knockbackForce = 20f;

    public float startDelay = 0f; // Add this for starting delay

    void Start()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();

        // Start the FireTrap with a delay
        StartCoroutine(StartTrapWithDelay());
    }

    IEnumerator StartTrapWithDelay()
    {
        yield return new WaitForSeconds(startDelay);
        ActivateTrap(); // Start the trap after the delay
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isActive)
        {
            if (timer >= activeDuration)
            {
                DeactivateTrap();
            }
        }
        else
        {
            if (timer >= inactiveDuration)
            {
                ActivateTrap();
            }
        }
    }

    void ActivateTrap()
    {
        isActive = true;
        timer = 0f;
        anim.SetBool("activated", true);
        boxCollider.enabled = true;
    }

    void DeactivateTrap()
    {
        isActive = false;
        timer = 0f;
        anim.SetBool("activated", false);
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
                playerStatus.currentHP -= damageAmount;
                Rigidbody playerRigidbody = collision.gameObject.GetComponent<Rigidbody>();
                if (playerRigidbody != null)
                {
                    Vector3 knockbackDirection = (collision.transform.position - transform.position).normalized;
                    playerRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                }
            }
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHeal : MonoBehaviour
{
     public int healAmount = 20;
    public Rigidbody character;
    public Transform[] respawnLocations;
    public float respawnTime = 30.0f;

    private bool isPickedUp = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isPickedUp && other.CompareTag("Player"))
        {
            PlayerStatusManager playerStatus = character.GetComponent<PlayerStatusManager>();

            Debug.Log("Heal.");

            if (playerStatus != null)
            {
                playerStatus.currentHP = Mathf.Min(playerStatus.currentHP + healAmount, playerStatus.maxHP);
                isPickedUp = true;
                StartCoroutine(RespawnAfterDelay());
            }
        }
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnTime);

        // Randomly select one of the respawn locations
        int randomIndex = Random.Range(0, respawnLocations.Length);
        Transform selectedLocation = respawnLocations[randomIndex];

        // Move the Heal Cherry to the selected location and reset the flag
        transform.position = selectedLocation.position;
        isPickedUp = false;
    }
}

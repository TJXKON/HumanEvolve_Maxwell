using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnHeal : MonoBehaviour
{
     public int healAmount = 20;
    public Rigidbody character;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStatusManager playerStatus = character.GetComponent<PlayerStatusManager>();

            Debug.Log("Heal.");

            if (playerStatus != null)
            {
                playerStatus.currentHP = Mathf.Min(playerStatus.currentHP + healAmount, playerStatus.maxHP);
                gameObject.SetActive(false); // Deactivate the cherry
                CherryRespawnController.Instance.StartRespawnTimer(); // Tell the respawn controller to start the timer
            }
        }
    }
}

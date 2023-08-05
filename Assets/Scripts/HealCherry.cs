using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealCherry : MonoBehaviour
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
                Destroy(gameObject); 
            }
        }
    }
}

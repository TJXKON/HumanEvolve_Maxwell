using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
     public int damageAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {

            
        if (collision.gameObject.CompareTag("Player"))
        {
           
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>();

             Debug.Log("Player collided with spike.");
            if (playerStatus != null)
            {
                playerStatus.currentHP -= damageAmount;
            }
        }
    }
   
}

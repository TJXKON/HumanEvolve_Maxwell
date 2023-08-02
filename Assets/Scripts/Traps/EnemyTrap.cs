using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrap : MonoBehaviour
{
     public int damageAmount = 10;

    private void OnCollisionEnter(Collision collision)
    {

            Debug.Log("Collision with spike detected.");
        if (collision.gameObject.CompareTag("Player"))
        {
           
            PlayerStatusManager playerStatus = collision.gameObject.GetComponent<PlayerStatusManager>();

             Debug.Log("Player collided with spike.");
            if (playerStatus != null)
            {
                playerStatus.health -= damageAmount;
            }
        }
    }
   
}

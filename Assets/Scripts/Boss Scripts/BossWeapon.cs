using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public int swordDamage = 20;
    private Enemy bossHealth;
    public float attackRange;

    void Start()
    {
        bossHealth = GetComponent<Enemy>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<PlayerStatusManager>().takeDamage(swordDamage);

            if(bossHealth.currentHP <= 70)
            {
                FindObjectOfType<PlayerStatusManager>().takeDamage(swordDamage + 2);
            }
        }
    }

    public void Attack()
    {
        
    }

    public void EnragedAttack()
    {

    }

}

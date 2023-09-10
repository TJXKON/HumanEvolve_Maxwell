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
    private bool playerIframe = false;

    void Start()
    {
        bossHealth = GetComponent<Enemy>();
    }

    void Update()
    {

        playerIframe = GameObject.Find("Player").GetComponent<PlayerStatusManager>().Iframe;
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
}

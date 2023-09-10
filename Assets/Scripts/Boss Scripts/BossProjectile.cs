using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    private float timeBetweenShot;
    public float startTimeBetweenShot;
    public float enemyCooldownTime;
    private int attackCount;
    public bool isCooldown = false;
    public GameObject projectile;
    private Enemy bossHealth;
    private bool playerIframe = false;

    void Start()
    {
        timeBetweenShot = startTimeBetweenShot;
        bossHealth = GetComponent<Enemy>();
    }

    void Update()
    {
        playerIframe = GameObject.Find("Player").GetComponent<PlayerStatusManager>().Iframe;
    }

    public void ProjectileAttack()
    {
        if (bossHealth.currentHP > 70)
        {
            if (attackCount < 2 && !isCooldown)
            {
                //Projectile Attack
                if (timeBetweenShot <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBetweenShot = startTimeBetweenShot;
                    attackCount++;
                }
                else
                {
                    timeBetweenShot -= Time.deltaTime;
                }
            }

            if (attackCount == 2 && !isCooldown)
                StartCoroutine(AttackCooldown());
        }
        else
        {
            if (attackCount < 3 && !isCooldown)
            {
                //Projectile Attack
                if (timeBetweenShot <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBetweenShot = startTimeBetweenShot;
                    attackCount++;
                }
                else
                {
                    timeBetweenShot -= Time.deltaTime;
                }
            }

            if (attackCount == 3 && !isCooldown)
                StartCoroutine(AttackCooldown());
        }
    }

    IEnumerator AttackCooldown()
    {
        isCooldown = true;
        yield return new WaitForSeconds(enemyCooldownTime);
        isCooldown = false;
        attackCount = 0;
    }
}

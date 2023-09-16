using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLifeState : MonoBehaviour
{
    public bool isBossInvinsible = false;
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        //Only for Boss
        if (isBossInvinsible)
            return;

        //Only for Boss
        if (enemy.currentHP <= 70)
        {
            GetComponent<Animator>().SetBool("IsEnraged", true);
        }
    }
}

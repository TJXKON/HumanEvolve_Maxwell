using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaEnemy : Enemy
{
    private Arena arena;
    void Awake()
    {
        currentHP = maxHP;
        arena = GameObject.Find("Arena").GetComponent<Arena>();
    }
    override protected void defeat(){
        if (arena!=null){
            arena.defeatCount();
        }
        thisEnemy = gameObject;
        Destroy(thisEnemy);

}
     
}

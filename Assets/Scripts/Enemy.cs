using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int maxHP = 20;
    int currentHP;

    public int hitDamage = 10;
    public GameObject dropItem;
    private bool playerIframe = false;
    private static GameObject thisEnemy;

    void Awake()
    {
        currentHP = maxHP;
    }
    // Update is called once per frame
    void Update()
    {
        if (currentHP<=0){
            defeat();
        }
    }

    //Hit Damage
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag=="Player" && !playerIframe){
            playerIframe = true;
            Debug.Log("Player hurted by enemy collision!");
            FindObjectOfType<PlayerStatusManager>().takeDamage(hitDamage);
            StartCoroutine(Cooldown(1f));
        }
    }

    public void takeDamage(int dmg){
        currentHP-=dmg;
    }

    void defeat(){
        thisEnemy = gameObject;
        Destroy(thisEnemy);
       if (dropItem!=null){
        Instantiate(dropItem, transform.position, Quaternion.identity);
       }

    }

    IEnumerator Cooldown(float time){
        yield return new WaitForSeconds(time);
        playerIframe = false;
    }

    void OnDestroy(){
        if (thisEnemy == gameObject){
            thisEnemy = null;
        }
    }



     
}

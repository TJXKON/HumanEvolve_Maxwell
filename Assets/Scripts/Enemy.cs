using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health = 20;
    public int hitDamage = 10;
    public GameObject dropItem;
    private bool Iframe = false;

    // Update is called once per frame
    void Update()
    {
        if (health<=0){
            defeat();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag=="Player" && !Iframe){
            Iframe = true;
            StartCoroutine(cooldown(1f));
            Debug.Log("Player hitted with enemy!");
            FindObjectOfType<PlayerStatusManager>().health-=hitDamage;
        }
    }

    void defeat(){
       Destroy(gameObject);
       if (dropItem!=null){
        Instantiate(dropItem, transform.position, Quaternion.identity);
       }

    }

    IEnumerator cooldown(float time){

        yield return new WaitForSeconds(time);
        Iframe = false;
    }
}

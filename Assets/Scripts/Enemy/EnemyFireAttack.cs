using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireAttack : MonoBehaviour
{
    public int hitDamage;
    private bool playerIframe = false;

    // Start is called before the first frame update
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player") && !playerIframe)
        {
            FindObjectOfType<PlayerStatusManager>().takeDamage(hitDamage);
            GameObject.Find("Player").GetComponent<PlayerStatusManager>().Iframe = false;
        }
       
    }
}

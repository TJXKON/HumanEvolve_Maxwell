using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{

    [SerializeField] public int dmg = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy!=null&&enemy.CompareTag("Enemy")){
                Debug.Log(enemy.gameObject.name+"Enemy hitted!");
                enemy.gameObject.GetComponent<Enemy>().takeDamage(dmg);
            }

    }
}

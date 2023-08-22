using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : MonoBehaviour
{

    [SerializeField] public int dmg = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider enemy)
    {
        if (enemy!=null&&enemy.CompareTag("Enemy")){
                Debug.Log(enemy.gameObject.name+"Enemy hitted!");
                enemy.gameObject.GetComponent<Enemy>().takeDamage(dmg);
            }

    }
}

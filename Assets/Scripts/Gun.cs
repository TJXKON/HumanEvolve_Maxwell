using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed;
    public int hitDamage = 5;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(wait());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * Time.deltaTime * speed;

    }

    void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            enemy.GetComponent<Enemy>().takeDamage(hitDamage);

            DestroyProjectile();
        }
        else if (enemy.CompareTag("Ground"))
            DestroyProjectile();

    }
    IEnumerator wait(){
        yield return new WaitForSeconds(0.5f);
        DestroyProjectile();
    }


    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float speed;
    public int hitDamage = 5;
    [SerializeField] public GameObject impact;

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

            GameObject.Find("GameManager").GetComponent<Scoring>().addScore(hitDamage);
            
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
        Transform tr = this.gameObject.transform;
        GameObject go = Instantiate(impact, tr.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

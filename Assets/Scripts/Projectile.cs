using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public int hitDamage = 5;
    private Transform player;
    private Vector2 target;
    private bool playerIframe = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                DestroyProjectile();
            }
            playerIframe = GameObject.Find("Player").GetComponent<PlayerStatusManager>().Iframe;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")&&!playerIframe)
        {
            FindObjectOfType<PlayerStatusManager>().takeDamage(hitDamage);
            //GameObject.Find("Player").GetComponent<PlayerStatusManager>().Iframe=false;
            DestroyProjectile();
        }
        else if (collider.CompareTag("Ground"))
            DestroyProjectile();
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}

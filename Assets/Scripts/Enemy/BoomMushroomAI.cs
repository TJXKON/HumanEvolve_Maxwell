using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class BoomMushroomAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   // Movement speed of the enemy.
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject boomCountdownEffect;
    [SerializeField] private GameObject enemySprite;
    [SerializeField] public GameObject boomEffect;
    [SerializeField] private float countdownTime;
    [SerializeField] private GameObject dropItem;
    [SerializeField] private Material flashMaterial;
    private Animator anim;

    private Transform player;   // Reference to the player's Transform component.
    private float pastPosition;
    public float waitTime;
    public Transform[] patrolDestination;
    private int currentPointIndex;
    private bool callOnce = false;
    private bool isBoomTrigger = false;
    private Transform sprite;
    private Vector3 localScale;
    Material defaultMaterial;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.Find("MushroomEnemySprite").gameObject.transform;
        localScale = sprite.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;   // Assuming the player tag is set to "Player".
        anim = enemySprite.GetComponent<Animator>();
        defaultMaterial = enemySprite.GetComponent<SpriteRenderer>().material;
    }


    // Update is called once per frame
    void Update()
    {
        pastPosition = transform.position.x;
        //Set a fixed distance for enemy sense player
        if (player != null && Vector2.Distance(transform.position, player.position) < 8f && !isBoomTrigger)
        {
            isBoomTrigger = true;
        }
        else if (isBoomTrigger)
        {
            BoomCountdown();
        }
        else
        {
            //Start patrol if enemy are not sense player
            if (transform.position != patrolDestination[currentPointIndex].position)
            {
                anim.SetBool("walk", true);
                transform.position = Vector2.MoveTowards(transform.position, patrolDestination[currentPointIndex].position, moveSpeed * Time.deltaTime);
            }
            else
            {
                if (callOnce == false)
                {
                    callOnce = true;
                    StartCoroutine(Wait());
                }
            }
        }
        CheckMoveDirection();
    }

    void CheckMoveDirection()
    {

        if (transform.position.x > pastPosition)
        {
            //transform.localScale = new Vector3(-1f, 1f, 1f);
            //Modified to avoid warning message, the scale is changed through sprite object and not the enemy main object to avoid problems with box collider
            sprite.localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
        }
        else if (transform.position.x < pastPosition)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            sprite.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
    }

    void AttackingMoveDirection()
    {
        if (transform.position.x - player.position.x >= 0)
        {
            sprite.localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
        }
        else
        {
            sprite.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }

    }

    void BoomCountdown()
    {
        anim.SetBool("walk", true);
        //Apporach player
       
        transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        GameObject go = Instantiate(boomCountdownEffect, firePoint.position + firePoint.up * 1.3f, Quaternion.identity);
        Destroy(go, 0.5f);

        countdownTime -= Time.deltaTime;
        if (countdownTime <= 0) 
        {
            countdownTime = 0;
        }

        if(countdownTime == 0)
        {
            Destroy();
        }
        AttackingMoveDirection();
        StartCoroutine(flash());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy();
        }
    }
    void Destroy()
    {
        GameObject go = Instantiate(boomEffect, firePoint.position + firePoint.up * 1.3f, Quaternion.identity);
        Destroy(go, 0.5f);
        Destroy(gameObject);
        if (dropItem != null)
        {
            Instantiate(dropItem, transform.position, Quaternion.identity);
        }
    }

    IEnumerator Wait()
    {
        anim.SetBool("walk", false);
        yield return new WaitForSeconds(waitTime);

        currentPointIndex++;
        if (currentPointIndex >= patrolDestination.Length)
        {
            currentPointIndex = 0;
        }
        callOnce = false;
    }

    IEnumerator keepFlash()
    {
        yield return new WaitForSeconds(0.15f);
        StartCoroutine(flash());
    }

    IEnumerator flash()
    {
        enemySprite.GetComponent<SpriteRenderer>().material = flashMaterial;
        yield return new WaitForSeconds(0.15f);
        enemySprite.GetComponent<SpriteRenderer>().material = defaultMaterial;
        StartCoroutine(keepFlash());
    }
}

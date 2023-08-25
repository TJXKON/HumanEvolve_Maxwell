using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiremanAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   // Movement speed of the enemy.
    [SerializeField] private float stoppingDistance;   // Distance at which the enemy stops moving towards the player.
    [SerializeField] private float retreatDistance; // Backward when player apporoach
    [SerializeField] private Transform firePoint;
    //[SerializeField] private GameObject enemyAnimation;
    [SerializeField] public GameObject fireEffect;
    [SerializeField] private GameObject flipFirePoint;
    //private Animator anim;

    private Transform player;   // Reference to the player's Transform component.
    private float pastPosition;
    private bool isRight = true;
    public float waitTime; 
    private float timeBetweenShot;
    public float startTimeBetweenShot;
    public Transform[] patrolDestination;
    private int currentPointIndex;
    private bool callOnce = false;
    private Transform sprite;
    private Vector3 localScale;

    // Start is called before the first frame update
    void Start()
    {
        sprite = transform.Find("EnemySprite").gameObject.transform;
        localScale = sprite.localScale;
        player = GameObject.FindGameObjectWithTag("Player").transform;   // Assuming the player tag is set to "Player".
        //anim = enemyAnimation.GetComponent<Animator>();
        timeBetweenShot = startTimeBetweenShot;
    }


    // Update is called once per frame
    void Update()
    {
        pastPosition = transform.position.x;

        //Set a fixed distance for enemy sense player
        if (player != null && Vector2.Distance(transform.position, player.position) < 12f)
        {
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
               //anim.SetBool("walk", true);
                //Apporach player
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                //anim.SetBool("walk", false);
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                //anim.SetBool("walk", true);
                //Away from distance if distance between them is too short
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
            }

            if (timeBetweenShot <= 0)
            {
                GameObject go = Instantiate(fireEffect, firePoint.position + firePoint.right * 4f + firePoint.up * 0.5f, Quaternion.identity);
                Destroy(go, 0.25f);
                timeBetweenShot = startTimeBetweenShot;
            }
            else
            {
                timeBetweenShot -= Time.deltaTime;
            }
            AttackingMoveDirection();
            //Attack Mode of fireman
        }
        else
        {
            //Start patrol if enemy are not sense player
            if (transform.position != patrolDestination[currentPointIndex].position)
            {
                //anim.SetBool("walk", true);
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
            sprite.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else if (transform.position.x < pastPosition)
        {
            //transform.localScale = new Vector3(1f, 1f, 1f);
            sprite.localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
        }
    }

    void AttackingMoveDirection()
    {
        if (transform.position.x - player.position.x >= 0)
        {
            sprite.localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
            if (isRight)
            {
                flipFirePoint.transform.Rotate(0f, 180f, 0f);
                isRight = false;
            }
        }
        else
        {
            sprite.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
            if(!isRight)
            {
                flipFirePoint.transform.Rotate(0f, 180f, 0f);
                isRight = true;
            }
        }
            
    }

    IEnumerator Wait()
    {
        //anim.SetBool("walk", false);
        yield return new WaitForSeconds(waitTime);

        currentPointIndex++;
        if (currentPointIndex >= patrolDestination.Length)
        {
            currentPointIndex = 0;
        }
        callOnce = false;
    }
}

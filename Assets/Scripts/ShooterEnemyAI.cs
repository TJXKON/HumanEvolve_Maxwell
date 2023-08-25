using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShooterEnemyAI : MonoBehaviour
{
    [SerializeField] private float moveSpeed;   // Movement speed of the enemy.
    [SerializeField] private float stoppingDistance;   // Distance at which the enemy stops moving towards the player.
    [SerializeField] private float retreatDistance; // Backward when player apporoach 
    //[SerializeField] private GameObject enemyAnimation;
    //private Animator anim;

    private Transform player;   // Reference to the player's Transform component.

    private float timeBetweenShot;
    public float startTimeBetweenShot;
    public GameObject projectile;

    private float pastPosition;
    public float waitTime;
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
        timeBetweenShot = startTimeBetweenShot;
       //anim = enemyAnimation.GetComponent<Animator>();
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

            //Attack Mode for Shooter
            if (timeBetweenShot <= 0)
            {
                Instantiate(projectile, transform.position, Quaternion.identity);
                timeBetweenShot = startTimeBetweenShot;
            }
            else
            {
                timeBetweenShot -= Time.deltaTime;
            }
            AttackingMoveDirection();
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
            CheckMoveDirection();
        }
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
        if(transform.position.x - player.position.x >= 0) 
        {
            sprite.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }
        else
            sprite.localScale = new Vector3(-(localScale.x), localScale.y, localScale.z);
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

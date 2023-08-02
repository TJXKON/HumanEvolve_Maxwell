using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public Transform firepoint;
    [SerializeField] public Animator animator;
    [SerializeField] public float attackRate = 1f;
    [SerializeField] public float charge = 1f;
    [SerializeField] public float globalcooldown = 0.5f;

    public bool isAttacking = false;
    private float cooldown = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !(GetComponent<PlayerMovement>().IsCrouching)){
            if (Input.GetButtonDown("Fire1")){

                isAttacking = true;
                cooldown = globalcooldown;    //Set cooldown
                Attack();
            }
            else if (Input.GetButtonDown("Fire2")){
                isAttacking = true;
                cooldown = globalcooldown+charge;
                StartCoroutine(SpecialAttack());
            }
        }
        else{
            //Reduce cooldown
            cooldown -= Time.deltaTime*attackRate;

            //Reset attack condition
            if (cooldown<=0f){
                isAttacking = false;
                animator.speed = 1;
            }
        }

    }

    void Attack(){

        //animator.SetTrigger("attack");
        animator.speed = attackRate;    //Animation speed based on attack rate
        //Perform attack
        switch (FindObjectOfType<PlayerStatusManager>().style)
        {
            case "Normal":
                FindObjectOfType<PlayerStyles>().Normal();
                break;
            case "Fire":
                FindObjectOfType<PlayerStyles>().Fire();
                break;
            case "Laser":
                FindObjectOfType<PlayerStyles>().Laser();
                break;
            case "Gun":
                FindObjectOfType<PlayerStyles>().Gun();
                break;
            case "Magic":
                FindObjectOfType<PlayerStyles>().Magic();
                break;
        }
    }

    IEnumerator SpecialAttack(){
        Debug.Log("Charge start");
        yield return new WaitForSeconds(charge);
        Debug.Log("Charge end");

        //animator.SetTrigger("specialAttack");
        animator.speed = attackRate;    //Animation speed based on attack rate
        Debug.Log("SpecialAttack");

        switch (FindObjectOfType<PlayerStatusManager>().style)
        {
            case "Normal":
                FindObjectOfType<PlayerStyles>().NormalSpecial();
                break;
            case "Fire":
                FindObjectOfType<PlayerStyles>().FireSpecial();
                break;
            case "Laser":
                FindObjectOfType<PlayerStyles>().LaserSpecial();
                break;
            case "Gun":
                FindObjectOfType<PlayerStyles>().GunSpecial();
                break;
            case "Magic":
                FindObjectOfType<PlayerStyles>().MagicSpecial();
                break;
        }
    }


}

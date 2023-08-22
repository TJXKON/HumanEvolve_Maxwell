using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public Transform firepoint;
    [SerializeField] public Animator animator;
    [SerializeField] public float attackRate = 1f;
    [SerializeField] public float charge = 1f;
    [SerializeField] public float GlobalcastTime = 0.5f;
    [SerializeField] public float normalMaxCooldown = 0.5f;
    [SerializeField] public float specialMaxCooldown = 1f;
    

    public bool isAttacking = false;
    public  float timer = 0f;
    [HideInInspector] public float normalCD = 0f;
    [HideInInspector] public float specialCD = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAttacking && !(GetComponent<PlayerMovement>().IsCrouching)){
            if (Input.GetButtonDown("Fire1")&& normalCD<=0f){

                isAttacking = true;
                timer = GlobalcastTime;    //Set cooldown
                Attack();
            }
            else if (Input.GetButtonDown("Fire2") && specialCD<=0f){
                isAttacking = true;
                timer = GlobalcastTime+charge;
                StartCoroutine(SpecialAttack());
            }
        }
        else if (timer>=0f){
            //Reduce cooldown
            timer -= Time.deltaTime*attackRate;

            //Reset attack condition
            if (timer<=0f){
                isAttacking = false;
                animator.speed = 1;
            }
        }
        
        if (normalCD>=0f){
            normalCD -= Time.deltaTime * 1f;
        }
        if (specialCD>=0f){
            specialCD -= Time.deltaTime * 1f;
        }

    }

    void Attack(){

        //animator.SetTrigger("attack");
        animator.speed = attackRate;    //Animation speed based on attack rate
        normalCD = normalMaxCooldown;
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
        specialCD = specialMaxCooldown;

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerAttack : MonoBehaviour
{

    [SerializeField] public Transform firepoint;
    [SerializeField] public GameObject chargeEffect;
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

    //UIs
    [Header("UIs")]
    public Image specialIcon;
   

    // Start is called before the first frame update
    void Start()
    {
        specialIcon.fillAmount = 0;
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

                specialIcon.fillAmount = 1;

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
            specialIcon.fillAmount -= Time.deltaTime*1f/specialMaxCooldown;

            if(specialIcon.fillAmount<=0){
                specialIcon.fillAmount=0;
            }
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

    Collider[] hits = Physics.OverlapSphere(firepoint.position + firepoint.right * 0.5f, 1.7f);

    foreach (Collider hit in hits)
    {
        if (hit.CompareTag("WoodBox"))
        {
            WoodBox boxController = hit.GetComponent<WoodBox>();
            if (boxController != null)
            {
                boxController.OnPlayerAttack();
            }
        }
    }
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
        //GameObject go = Instantiate(chargeEffect, this.gameObject.transform.position+this.gameObject.transform.up * -2f, Quaternion.identity);
        GameObject go = Instantiate(chargeEffect, this.gameObject.transform.position+this.gameObject.transform.up * -2f, Quaternion.identity) as GameObject; 
        go.transform.parent = GameObject.Find("Player").transform;

        yield return new WaitForSeconds(charge);
        Debug.Log("Charge end");
         Destroy(go);

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

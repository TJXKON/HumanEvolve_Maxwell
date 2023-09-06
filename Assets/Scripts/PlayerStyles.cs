using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStyles : MonoBehaviour
{
    

     private Scoring scoring;
    [SerializeField] public Transform firePoint;
    [SerializeField] private LayerMask enemyLayerMask;

    [Header("Normal")]
    [SerializeField] public GameObject punchEffect;
    [SerializeField] private int normalDmg = 5;
    [SerializeField] private int normalSpecialDmg = 10;

    [Header("Fire")]
    [SerializeField] public GameObject fireEffect;
    [SerializeField] public GameObject fireSpecialEffect;


    [Header("Laser")]
    [SerializeField] public GameObject laser;
    [SerializeField] private int laserDmg = 10;

    [Header("Gun")]
    [SerializeField] public GameObject gunBullet;

    [Header("Magic")]
    [SerializeField] public GameObject MagicEffect;


    void Start() {
        scoring = GameObject.Find("GameManager").GetComponent<Scoring>();
    }


    public void Normal(){
        Debug.Log("Cast Normal style attack");
        //animator trigger
        
        GameObject go = Instantiate(punchEffect, firePoint.position + firePoint.right * 0.5f, Quaternion.identity);

        Collider[] hitEnemies = Physics.OverlapSphere(firePoint.position + firePoint.right * 0.5f, 1.7f);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")){
                Debug.Log(enemy.gameObject.name+"Enemy hitted!");
                enemy.gameObject.GetComponent<Enemy>().takeDamage(normalDmg);

                scoring.addScore(normalDmg);
            }

        }

        Destroy(go,0.25f);
    }


    public void Fire(){
        Debug.Log("Cast Fire style attack");
        GameObject go = Instantiate(fireEffect, firePoint.position + firePoint.right * 2.5f + firePoint.up * 0.5f, Quaternion.identity);
        Destroy(go,0.25f);
    }

    public void Laser(){
        Debug.Log("Cast Laser style attack");
        GameObject go = Instantiate(laser,firePoint.position,Quaternion.identity);
        LineRenderer line = go.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.right, out hit, 10f, enemyLayerMask)){
            if (hit.collider.CompareTag("Enemy")){
                GameObject enemy = hit.collider.gameObject;
                enemy.GetComponent<Enemy>().takeDamage(laserDmg);
                scoring.addScore(normalSpecialDmg);

                line.SetPosition(0,firePoint.position);
                line.SetPosition(1,hit.point);

            }
        }
        else{
            line.SetPosition(0,firePoint.position);
            line.SetPosition(1,firePoint.position+firePoint.right*10);

        }

        Destroy(go,0.25f);
    }

    public void Gun(){
        Debug.Log("Cast Gun style attack");
        Instantiate(gunBullet,firePoint.position,firePoint.rotation);
        
    }
    
    public void Magic(){
        Debug.Log("Cast Magic style attack");
        GameObject go = Instantiate(MagicEffect, this.gameObject.transform.position, Quaternion.identity);
        Destroy(go,0.25f);
    }



    public void NormalSpecial(){
        Debug.Log("Cast Normal style special attack");
        //animator trigger
        
        GameObject go = Instantiate(punchEffect, firePoint.position + firePoint.right * 0.5f, firePoint.rotation);

        Collider[] hitEnemies = Physics.OverlapSphere(firePoint.position + firePoint.right * 0.5f, 1.7f);

 

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")){
                Debug.Log(enemy.gameObject.name+"Enemy hitted!");
                enemy.gameObject.GetComponent<Enemy>().takeDamage(normalSpecialDmg);
                scoring.addScore(normalSpecialDmg);
            }

        }

        Destroy(go,0.25f);

        Destroy(go,0.25f);

       
    }


    public void FireSpecial(){
        Debug.Log("Cast Fire style special attack");
        RaycastHit hit;
        if (Physics.Raycast(firePoint.position, firePoint.right, out hit, 10f, enemyLayerMask)){
            if (hit.collider.CompareTag("Enemy")&&hit.collider!=null){
                Transform target = hit.collider.transform;
                GameObject go = Instantiate(fireSpecialEffect, target.position + target.up * 1f, Quaternion.identity);
                Destroy(go,0.25f);
            }
        }
        else{
            //If no enemy found, cast directly in front
            GameObject go = Instantiate(fireSpecialEffect, firePoint.position + firePoint.right * 2.5f + firePoint.up * 1f, firePoint.rotation);
            Destroy(go,0.4f);
        }
    }

    public void LaserSpecial(){
        Debug.Log("Cast Laser style special attack");

        GameObject go = Instantiate(laser,firePoint.position,Quaternion.identity);
        LineRenderer line = go.transform.GetChild(0).gameObject.GetComponent<LineRenderer>();

        RaycastHit[] hits = Physics.RaycastAll(firePoint.position, transform.right, 15f,enemyLayerMask);
        foreach (RaycastHit hit in hits){
            if (hit.collider.CompareTag("Enemy")){
                GameObject enemy = hit.collider.gameObject;
                enemy.GetComponent<Enemy>().takeDamage(laserDmg);
                scoring.addScore(normalSpecialDmg);
            }
        }
        line.startWidth=0.3f;
        line.endWidth=1f;
        line.SetPosition(0,firePoint.position);
        line.SetPosition(1,firePoint.position+firePoint.right*15);
        Destroy(go,0.25f);

    }

    public void GunSpecial(){
        Debug.Log("Cast Gun style special attack");
        float angle = 0f;
        for (int i=0;i<4;i++){
            Instantiate(gunBullet,firePoint.position,firePoint.rotation* Quaternion.Euler (0f, 0f, angle));
            angle+=15f;
        }
    }

    
    public void MagicSpecial(){
        Debug.Log("Cast Magic style special attack");

        GameObject go = Instantiate(MagicEffect, this.gameObject.transform.position, Quaternion.identity);
        Destroy(go,0.25f);
        
        Collider[] hitEnemies = Physics.OverlapSphere(this.gameObject.transform.position,10f);
        foreach (Collider enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy")){
                    Transform target = enemy.gameObject.transform;
                    GameObject go2 = Instantiate(MagicEffect, target.position + target.up * 1f, Quaternion.identity);
                    Destroy(go2,0.25f);
                }

            }


    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStyles : MonoBehaviour
{
    [SerializeField] public Transform firePoint;
    [SerializeField] private LayerMask enemyLayerMask;

    [SerializeField] public GameObject punchEffect;
    [SerializeField] public GameObject fireEffect;
    [SerializeField] public GameObject laser;
    [SerializeField] public GameObject gunBullet;
    //[SerializeField] public GameObject MagicEffect;

    [SerializeField] public GameObject fireSpecialEffect;

    [SerializeField] public float NormalDmg;


    public void Normal(){
        Debug.Log("Cast Normal style attack");
        //animator trigger
        
        GameObject go = Instantiate(punchEffect, firePoint.position + firePoint.right * 0.5f, Quaternion.identity);

        Collider[] hitEnemies = Physics.OverlapSphere(firePoint.position + firePoint.right * 0.5f, 1.7f);

        foreach (Collider enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy")){
                Debug.Log(enemy.gameObject.name+"Enemy hitted!");
                enemy.gameObject.GetComponent<Enemy>().takeDamage(5);
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
                enemy.GetComponent<Enemy>().takeDamage(10);
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
                enemy.gameObject.GetComponent<Enemy>().takeDamage(10);
            }

        }

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
            Destroy(go,0.25f);
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
                enemy.GetComponent<Enemy>().takeDamage(10);
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
        for (int i=0;i<3;i++){
            Instantiate(gunBullet,firePoint.position,firePoint.rotation* Quaternion.Euler (0f, 0f, angle));
            angle+=15f;
        }
        StartCoroutine(secondShot());
    }

    IEnumerator secondShot(){
        yield return new WaitForSeconds(0.1f);
        float angle = 0f;
        for (int i=0;i<5;i++){
            Instantiate(gunBullet,firePoint.position,firePoint.rotation* Quaternion.Euler (0f, 0f, angle));
            angle+=15;
        }
    }
    
    public void MagicSpecial(){
        Debug.Log("Cast Magic style special attack");
    }


}

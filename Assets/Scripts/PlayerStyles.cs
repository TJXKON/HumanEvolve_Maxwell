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
    [SerializeField] public GameObject MagicEffect;

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
    }

    public void Gun(){
        Debug.Log("Cast Gun style attack");
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
            if (hit.collider.CompareTag("Enemy")){
                Debug.Log("fire!");
                Transform target = hit.collider.transform;
                GameObject go = Instantiate(fireSpecialEffect, target.position + target.up * 1f, Quaternion.identity);
                Destroy(go,0.25f);
            }
        }
        else{
            //If no enemy found, cast directly in front
            GameObject go = Instantiate(fireSpecialEffect, firePoint.position + firePoint.right * 2.5f + firePoint.up * 1f, Quaternion.identity);
            Destroy(go,0.25f);
        }
    }

    public void LaserSpecial(){
        Debug.Log("Cast Laser style special attack");
    }

    public void GunSpecial(){
        Debug.Log("Cast Gun style special attack");
    }
    
    public void MagicSpecial(){
        Debug.Log("Cast Magic style special attack");
    }
}

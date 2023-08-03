using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStyles : MonoBehaviour
{

    [SerializeField] public GameObject punchEffect;
    [SerializeField] public GameObject fireEffect;
    [SerializeField] public GameObject laser;
    [SerializeField] public GameObject gunBullet;
    [SerializeField] public GameObject MagicEffect;
    [SerializeField] public Transform firePoint;



    public void Normal(){
        Debug.Log("Cast Normal style attack");
    }


    public void Fire(){
        Debug.Log("Cast Fire style attack");
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
    }    

    public void FireSpecial(){
        Debug.Log("Cast Fire style special attack");
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

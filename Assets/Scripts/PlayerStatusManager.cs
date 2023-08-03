using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{

    public int maxHP = 100;
    [HideInInspector] public int currentHP;
    public bool Iframe = false;

    public string style = "Normal";

    void Start(){
        currentHP = maxHP;
    }
    void Update() {
        if (currentHP<=0){
            FindObjectOfType<GameManager>().gameOver();
        }
    }

    public void takeDamage(int dmg){
        currentHP-=dmg;

    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusManager : MonoBehaviour
{

    public int health = 100;

    public string style = "Normal";

    void Update() {
        if (health<=0){
            FindObjectOfType<GameManager>().gameOver();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().name=="Debug Room"){
            FindObjectOfType<DebugManager>().enabled = true;
        }
        else{
            FindObjectOfType<DebugManager>().enabled = false;
        }
    }


    void Update()
    {

    }

    public void gameOver(){
        Debug.Log("Game Over!");
        Destroy(GameObject.Find("Player"));
    }
}

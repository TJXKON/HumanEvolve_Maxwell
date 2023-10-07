using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
    public static float gameSpeed=1.0f;

    void Awake()
    {
        Time.timeScale = GameManager.gameSpeed;

        if (SceneManager.GetActiveScene().name=="Monster Room")
        {
            FindObjectOfType<DebugManager>().enabled = true;
        }else
        {
            FindObjectOfType<DebugManager>().enabled = false;
        }
    }
}

   
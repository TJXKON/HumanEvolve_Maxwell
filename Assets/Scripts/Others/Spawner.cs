using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spawner : MonoBehaviour
{
    public GameObject arenaEnemy;
    public float interval = 4f;

    public float offset = 0f;
    private float timer = 0f;




    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer>=interval){
            Instantiate(arenaEnemy, this.gameObject.transform.position, Quaternion.identity);
            timer=0f;   //Reset Timer
        }
    }

    void OnEnable()
    {
         timer-=offset;
    }





}

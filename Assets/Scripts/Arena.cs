using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Arena : MonoBehaviour

{
    public int counter = 50;
    public Text ArenaText;
    public Text counterText;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        counterText.text=counter+" left";

        if(counter<=0){
            ArenaText.enabled=false;
            counterText.enabled=false;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider c)
    {

        if (c.tag=="Player"){

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            ArenaText.enabled=true;
            ArenaText.text="Welcome to Arena!"+"\n"+"Defeat "+counter+" Enemies to Clear!";
            counterText.enabled=true;
            GetComponent<BoxCollider>().enabled=false;
            
     
        }
    }

    public void defeatCount(){
        counter-=1;
    }

}

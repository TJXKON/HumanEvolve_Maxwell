using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DebugManager : MonoBehaviour
{


    void Start(){
        Debug.Log("**Developer mode** \nSwap Style: \n1 - Normal \n2 - Fire \n3 - Laser \n4 - Gun \n5 - Magic");
    }
    // Update is called once per frame
    void Update()
    {


        if(Input.GetKeyDown(KeyCode.Alpha1)){
            ChangeStyle("Normal");
        }
        if(Input.GetKeyDown(KeyCode.Alpha2)){
            ChangeStyle("Fire");
        }
        if(Input.GetKeyDown(KeyCode.Alpha3)){
            ChangeStyle("Laser");
        }
        if(Input.GetKeyDown(KeyCode.Alpha4)){
            ChangeStyle("Gun");
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            ChangeStyle("Magic");
        }       
    }

    void ChangeStyle(string style){

        FindObjectOfType<PlayerStatusManager>().style=style;
        Debug.Log("Debug Mode: Change style to "+style);

    }
}

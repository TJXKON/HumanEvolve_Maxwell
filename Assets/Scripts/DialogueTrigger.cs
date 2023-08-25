using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{

    public Dialogue dialogueScript;
    private bool playerDetected;
   

   private void OnTriggerEnter(Collider collision)
   {
        if(collision.tag =="Player")
        {
            playerDetected = true;
            dialogueScript.ToogleIndicator(playerDetected);
        }
   }

   private void OnTriggerExit(Collider collision)
   {
        if(collision.tag =="Player")
        {
            playerDetected = false;
            dialogueScript.ToogleIndicator(playerDetected);
        }
   }

   private void Update()
    {
        if(playerDetected && Input.GetKeyDown(KeyCode.E))
        {
            dialogueScript.StartDialogue();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public GameObject window;

    public GameObject indicator;

    public List<string> dialogues;

    public TMP_Text dialogueText;

    private int index;

    public float writtingSpeed;

    private int charIndex;

    private bool started;

    private bool waitForNext;

    private void Awake()
    {
        ToogleIndicator(false);
        ToogleWindow(false);
    }

    private void ToogleWindow(bool show)
    {
        window.SetActive(show);
    }

    
    public void ToogleIndicator(bool show)
    {
        indicator.SetActive(show);
    }

    public void StartDialogue()
    {
        if(started)
        return;
        started = true;
       ToogleWindow(true);
       ToogleIndicator(false);
      GetDialogue(0);
    }

    private void GetDialogue(int i)
    {
       index = i;
       charIndex = 0;
       dialogueText.text = string.Empty;
      StartCoroutine(Writting());
    }

    public void EndDialogue()
    {
        ToogleWindow(false);
    }

 IEnumerator Writting()
    {
        string currentDialogue = dialogues[index];
        dialogueText.text += currentDialogue[charIndex];
        charIndex++;
        if(charIndex < currentDialogue.Length)
        {
            yield return new WaitForSeconds(writtingSpeed);
        StartCoroutine(Writting());
        }
        else
        {
            waitForNext=true;
        }

    }

    private void Update()
    {
        if(!started)
        return;
        
        if(waitForNext && Input.GetKeyDown(KeyCode.E))
        {
            waitForNext = false;
            index++;
            if(index < dialogues.Count)
            {
                GetDialogue(index);
            }
            else
            {
                EndDialogue();
            }
            
        }
    }
}

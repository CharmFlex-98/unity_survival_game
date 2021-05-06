using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
  
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            if (gameObject.transform.parent.CompareTag("Object"))
            {
                if (GameManager.instance.mode == GameManager.Mode.InHouse)
                {
                    DialogueManager.instance.ExitButton.SetActive(true);
                    GameManager.instance.ActiveButton = DialogueManager.instance.ExitButton;
                }
            }
            
            else if (gameObject.transform.parent.CompareTag("Cave"))
            {
                if (GameManager.instance.mode == GameManager.Mode.Task || GameManager.instance.mode == GameManager.Mode.InHouse)
                {
                    if (GameManager.instance.inNewPlace == false)
                    {
                        DialogueManager.instance.EnterButton.SetActive(true);
                        GameManager.instance.ActiveButton = DialogueManager.instance.EnterButton;
                    }
                    else
                    {
                        DialogueManager.instance.ExitCaveButton.SetActive(true);
                        GameManager.instance.ActiveButton = DialogueManager.instance.ExitCaveButton;
                    }
                }
                else if (GameManager.instance.mode == GameManager.Mode.Adventure)
                {
                    if (GameManager.instance.inNewPlace == true)
                    {
                        DialogueManager.instance.ExitCaveButton.SetActive(true);
                        GameManager.instance.ActiveButton = DialogueManager.instance.ExitCaveButton;
                    }
                }
            }
            else if (gameObject.transform.parent.CompareTag("HomeArea")==false)
            {
                if (GameManager.instance.mode != GameManager.Mode.Adventure)
                {
                    DialogueManager.instance.TalkButton.SetActive(true);
                    GameManager.instance.ActiveButton = DialogueManager.instance.TalkButton;
                    DialogueManager.instance.ActiveNPC = transform.parent.gameObject;
                }
            }
        }

        else if (other.transform.parent.CompareTag("TargetNPC"))
        {
            

            if (gameObject.transform.parent.CompareTag("HomeArea"))
            {
                DialogueManager.instance.TaskComplete();
            }
        }
       
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.ActiveButton != null)
            {
                GameManager.instance.ActiveButton.SetActive(false);
                DialogueManager.instance.ActiveNPC = null;
            }
            else
            {
                return;
            }

            
        }
    }

}

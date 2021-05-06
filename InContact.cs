using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InContact : MonoBehaviour
{
    
    public enum QuestType { ok, okk, okkk };
    public QuestType QuestState;
    public GameObject AskButton;



    public void ShowAskingButton()
    {
        AskButton.SetActive(true);
        AskButton.GetComponent<StoryType>().InfoTransfer(this);
        
    }

    public void DisablingAskingButton()
    {
        AskButton.SetActive(false);
    }

    public void ShowQuest()
    {
        if (QuestState == QuestType.ok)
        {
            //Debug.Log("ok");
        }

        else if (QuestState == QuestType.okk)
        {
            //Debug.Log("okk");
        }

        else if (QuestState == QuestType.okkk)
        {
            //Debug.Log("okkk");
        }
    }
        
           
        
      
    

 
}

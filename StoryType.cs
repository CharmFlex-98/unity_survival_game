using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryType : MonoBehaviour
{
    InContact InContact;
    public GameObject Story;
    public void InfoTransfer(InContact _inContact)
    {
        
        InContact = _inContact;
    }

    public void Press()
    {
        
        if (InContact.QuestState == InContact.QuestType.ok)
        {
            Story.transform.GetChild(0).gameObject.SetActive(true);
        }

        else if (InContact.QuestState == InContact.QuestType.okk)
        {
            Story.transform.GetChild(1).gameObject.SetActive(true);
        }

        else if (InContact.QuestState == InContact.QuestType.okkk)
        {
            Story.transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseChat : MonoBehaviour
{
    public GameObject Story;
    Transform StoryTransform;

    private void Start()
    {
        StoryTransform = Story.transform;
    }

    public void closeChat()
    {
        for (int i = 0; i <StoryTransform.childCount; i++)
        {
            StoryTransform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

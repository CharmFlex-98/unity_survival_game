using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAIRoute : MonoBehaviour
{
    public GameObject Route;
    int CheckPointNum = 0;
    public string Tag;
   


    private void Start()
    {
       this.transform.position = Route.transform.GetChild(0).transform.position;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag))
        {
           
            Vector3 pos = Route.transform.GetChild(CheckPointNum).transform.position;
            this.transform.position = pos;
            CheckPointNum++;
        }
    }
}

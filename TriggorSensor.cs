using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggorSensor : MonoBehaviour
{
    public GameObject Zombi;
    GameObject MainChar;
    EnemyPathFinding EPF;
    


    private void Start()
    {
        MainChar = Zombi.GetComponent<EnemyPathFinding>().MainChar;
        EPF = Zombi.GetComponent<EnemyPathFinding>();
        StartCoroutine("CheckUpdate");
    }
    IEnumerator CheckUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Mathf.Abs((MainChar.transform.position - transform.position).magnitude) < 30)
            {
                Zombi.SetActive(true);
                EPF.Activate();
                EPF.StartCoroutine("CheckUpdate");
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Zombi.GetComponent<EnemyPathFinding>().Target = other.GetComponent<FPS_Controller>().FocusPoint.transform;
        }
    }

    
   
}

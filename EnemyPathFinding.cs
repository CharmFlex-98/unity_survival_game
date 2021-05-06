using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyPathFinding : MonoBehaviour
{
    NavMeshAgent agent;
    public Transform Target;
    [HideInInspector]public Transform OriginalTarget;
    public GameObject MainChar;
    bool reactivated = false;
    ZombieManager ZM;
    
    void Start()
    {
        OriginalTarget = Target;
        agent= GetComponent<NavMeshAgent>();
        agent.enabled = true;
        ZM = GetComponent<ZombieManager>();
        StartCoroutine("CheckUpdate");
        
       
    }

    // Update is called once per frame
    IEnumerator CheckUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (Target == null)
            {
               
                yield return null;
            }
            else
            {
                
                if (GameManager.instance.mode != GameManager.Mode.Daily)
                {
                    
                    if (Mathf.Abs((this.transform.position - MainChar.transform.position).magnitude) > 50)
                    {
                        ZM.Moaning = false;
                        this.gameObject.SetActive(false);
                        reactivated = false;
                        Target = OriginalTarget;
                        this.gameObject.transform.position = ZM.OriginPos;
                        this.gameObject.transform.rotation = ZM.OriginRot;
                    }
                }
                else
                {
                    
                    agent.SetDestination(Target.position);
                }
            }
            if (reactivated == true)
            {
                
                agent.SetDestination(Target.position);
            }
        }
    }

    public void Activate()
    {
        reactivated = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Follower : MonoBehaviour
{
    NavMeshAgent agent;
    public GameObject target;
    [HideInInspector]public Vector3 InitialPos;
    [HideInInspector]public GameObject _target;


    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InitialPos = transform.position;
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        else
        {
            agent.SetDestination(_target.transform.position);
        }
    }

    public void Target()
    {
        _target = target;
    }
}

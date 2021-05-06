using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedController : MonoBehaviour
{
    public float multiplier;
    float _multiplier;
    [HideInInspector]public Animator anim;
    [HideInInspector]public bool die=false;

    private void Start()
    {
        anim=GetComponent<Animator>();
        _multiplier = multiplier;
    }

    private void Update()
    {
        if (die == false)
        {
            anim.speed = _multiplier;
        }
        else
        {
            anim.speed = 1;
        }
    }

    void SpeedControl()
    {
        
    }
}

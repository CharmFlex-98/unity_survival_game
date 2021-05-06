using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartRecord : MonoBehaviour
{
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.enabled = true;
        }
    }
}

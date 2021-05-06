using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnim : MonoBehaviour
{
    Animator anim;
    float time;

    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("startAnim");
        time = Random.Range(0, 1.5f);


    }

    IEnumerator startAnim()
    {
        while (Time.time <= time)
        {
            yield return null;
        }
        anim.enabled = true;
    }


}

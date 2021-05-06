using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombiAttack : MonoBehaviour
{
    public Animator anim;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<FPS_Controller>().PlayerDie();
            anim.SetBool("ZombieBite", true);
            GameManager.instance.DyingPanelActive();
        }
    }
}

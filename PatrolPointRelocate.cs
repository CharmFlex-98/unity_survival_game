using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolPointRelocate : MonoBehaviour
{
    public float limz, limx;
    float z, x;
    public GameObject Zombi;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            z = Random.Range(-limz, limz);
            x = Random.Range(-limx, limx);
            transform.localPosition = new Vector3(x, transform.localPosition.y, z);
        }
    }
}

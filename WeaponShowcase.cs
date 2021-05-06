using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShowcase : MonoBehaviour
{
    Rigidbody rb;
    public float RotationSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        rb.angularVelocity = new Vector3(0,RotationSpeed,0);
    }
}

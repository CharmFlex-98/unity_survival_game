using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDamage : MonoBehaviour
{
    [HideInInspector] public float Damage;
    public float ShootForce;
    Rigidbody rb;


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

   
    



   
    public void Shot(Transform hitTarget, Vector3 hitpoint)
    {
        if (hitTarget.CompareTag("Enemy"))
        {
            hitTarget.GetComponent<ZombieManager>().TakeDamage(Damage);
            gameObject.SetActive(false);
        }
        
        transform.position = hitpoint;
        transform.SetParent(hitTarget);
    }

   



    


}

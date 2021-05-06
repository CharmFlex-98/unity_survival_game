using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    MyWeaponInfo weapon;
    private void Start()
    {
        weapon = GetComponent<MyWeaponInfo>();
    }
    private void Update()
    {
        if (weapon.WeaponInfo.OnEquip == true)
        {
            gameObject.SetActive (true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}

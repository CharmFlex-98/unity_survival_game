using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Weapon",menuName ="Weapon")]
public class MyWeapon : ScriptableObject
{
    public string WeaponName;
    public float Damage;
    public string Description;
    public int Gold;
    public int Rope;
    public int Branches;
    public bool Owned;
    public bool OnEquip;
    public float CameraXPos;
    public float InvCameraXPos;


}

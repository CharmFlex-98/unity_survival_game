using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponButtonTrigger : MonoBehaviour
{
    public MyWeapon weaponInfo;
    WeaponInfoDisplay _weaponinfodisplay;
    Image image;
    public TextMeshProUGUI Nametext;
   

    private void Start()
    {
        image = GetComponent<Image>();
        _weaponinfodisplay = transform.parent.GetComponent<WeaponInfoDisplay>();
        
    }
    public void OnClick()
    {
       _weaponinfodisplay.DisSelect();
       _weaponinfodisplay.InfoDisplay(weaponInfo);
       image.color = new Color(image.color.r, image.color.g, image.color.b, 145);
    }

   

}

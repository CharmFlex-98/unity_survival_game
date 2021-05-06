using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class inventoryButton2 : MonoBehaviour
{
    [HideInInspector]public MyWeapon weaponInfo;
    InventoryItemDisplay _InventoryItemDisplay;
    public TextMeshProUGUI NameText;

    Image image;
   


    private void Start()
    {
        image = GetComponent<Image>();
        _InventoryItemDisplay = transform.parent.GetComponent<InventoryItemDisplay>();

    }
    public void OnClick()
    {
        _InventoryItemDisplay.DisSelect();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 145);
       _InventoryItemDisplay.InfoDisplay(weaponInfo);
        _InventoryItemDisplay.SelectedButton(gameObject);
    }

}

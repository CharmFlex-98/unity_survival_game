using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemDisplay : MonoBehaviour
{
    public GameObject WeaponList;
    public GameObject InventoryButtonPref;
    Image image;
    inventoryButton2 button2;
    public Camera UICamera;
    public TextMeshProUGUI DamageText;
    public Color[] color;
    public GameObject EquippedText;
    public GameObject EquipButton;
    MyWeapon activeWeapon;
    GameObject Selectedbutton;
    private void Start()
    {
        for (int i = 0; i < WeaponList.transform.childCount; i++)
        {
            if (WeaponList.transform.GetChild(i).GetComponent<MyWeaponInfo>().WeaponInfo.Owned == true)
            {
                GameObject Button = Instantiate(InventoryButtonPref, transform);
                button2 = Button.GetComponent<inventoryButton2>();
                button2.weaponInfo = WeaponList.transform.GetChild(i).GetComponent<MyWeaponInfo>().WeaponInfo;
                button2.NameText.text = button2.weaponInfo.WeaponName;
                if (WeaponList.transform.GetChild(i).GetComponent<MyWeaponInfo>().WeaponInfo.OnEquip == true)
                {
                    button2.NameText.color = color[1];
                    DisSelect();
                    Button.GetComponent<Image>().color = new Color(image.color.r, image.color.g, image.color.b, 145);
                    GetComponent<InventoryItemDisplay>().InfoDisplay(button2.weaponInfo);
                }
                else
                {
                    button2.NameText.color = color[0];
                   
                }

            }
        }
    }

    public void AddItem(MyWeapon info)
    {
        GameObject Button = Instantiate(InventoryButtonPref, transform);
        Button.GetComponent<inventoryButton2>().weaponInfo = info;
        Button.GetComponent<inventoryButton2>().NameText.text = info.WeaponName;
        Button.GetComponent<inventoryButton2>().NameText.color = color[0];

    }

    public void DisSelect()
    {
        for (int i = 0; i <transform.childCount; i++)
        {
            image = transform.GetChild(i).GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }

    public void InfoDisplay(MyWeapon info)
    {
        activeWeapon = info;
        UICamera.transform.position = new Vector3(info.InvCameraXPos, UICamera.transform.position.y, UICamera.transform.position.z);
        DamageText.text =info.Damage.ToString();
        if (info.OnEquip == true)
        {
            EquippedText.SetActive(true);
            EquipButton.SetActive(false);
        }
        else
        {
            EquippedText.SetActive(false);
            EquipButton.SetActive(true);
        }
    }

    public void EquipItem()
    {
        UnEquipAll();
        activeWeapon.OnEquip = true;
        EquippedText.SetActive(true);
        EquipButton.SetActive(false);
        Selectedbutton.GetComponent<inventoryButton2>().NameText.color = color[1];
        GameManager.instance.BuyEquipIDGenerator();
        GameManager.instance.SaveGame();
    }
    void UnEquipAll()
    {
        for (int i = 0; i < WeaponList.transform.childCount; i++)
        {
            WeaponList.transform.GetChild(i).GetComponent<MyWeaponInfo>().WeaponInfo.OnEquip = false;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<inventoryButton2>().NameText.color= color[0];
        }
    }

    public void SelectedButton(GameObject button)
    {
        Selectedbutton = button;
    }

    public void itemupdate()
    {
        GameManager.instance.ItemUpdated = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponInfoDisplay : MonoBehaviour
{
    MyWeapon SelectedWeapon;
    public TextMeshProUGUI DescriptionText, RequiredGoldText, RequiredRopeText, RequiredBranchesText;
    Image image;
    public Camera UICamera;
    public Scrollbar scrollbar;
    public GameObject WeaponList;
    public GameObject ButtonPrefab;
    WeaponButtonTrigger weaponButtonTrigger;
    public GameObject BuyButton;
    public Color[] color;
    public GameObject ComfirmationPanel;
    public TextMeshProUGUI comfirmationText;
    string SelectedWeaponName;
    public GameObject Inventory;

    private void Start()
    {
        for (int i = 0; i < WeaponList.transform.childCount; i++)
        {
            GameObject Button = Instantiate(ButtonPrefab, transform);
            weaponButtonTrigger = Button.GetComponent<WeaponButtonTrigger>();
            weaponButtonTrigger.weaponInfo = WeaponList.transform.GetChild(i).GetComponent<MyWeaponInfo>().WeaponInfo;
            weaponButtonTrigger.Nametext.text = weaponButtonTrigger.weaponInfo.WeaponName;
            
        }
        scrollbar.value = 1;
    }

    public void InfoDisplay(MyWeapon weaponInfo)
    {
        SelectedWeapon = weaponInfo;
        SelectedWeaponName = weaponInfo.WeaponName;
        if (weaponInfo.Owned == false)
        {
            DescriptionText.text = weaponInfo.Description+"\nDamage: "+weaponInfo.Damage;
        }
        else
        {
            DescriptionText.text = weaponInfo.Description + " (Owned)" + "\nDamage: " + weaponInfo.Damage;
            
        }
        RequiredGoldText.text = "<sprite=0> " + weaponInfo.Gold.ToString();
        if (GameManager.instance.Gold < weaponInfo.Gold)
        {
            RequiredGoldText.color = color[1];
        }
        else
        {
            RequiredGoldText.color = color[0];
        }
        RequiredRopeText.text = "<sprite=1> " + weaponInfo.Rope.ToString();
        if (GameManager.instance.Rope < weaponInfo.Rope)
        {
            RequiredRopeText.color = color[1];
        }
        else
        {
            RequiredRopeText.color = color[0];
        }
        RequiredBranchesText.text = "<sprite=2> " + weaponInfo.Branches.ToString();
        if (GameManager.instance.Branches < weaponInfo.Branches)
        {
            RequiredBranchesText.color = color[1];
        }
        else
        {
            RequiredBranchesText.color = color[0];
        }
        UICamera.transform.position = new Vector3(weaponInfo.CameraXPos, UICamera.transform.position.y, UICamera.transform.position.z);
        if (weaponInfo.Owned == false)
        {
            BuyButton.SetActive(true);
        }
        else
        {
            BuyButton.SetActive(false);
        }
        if (weaponInfo.Gold > GameManager.instance.Gold || weaponInfo.Rope > GameManager.instance.Rope || weaponInfo.Branches > GameManager.instance.Branches)
        {
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            BuyButton.GetComponent<Button>().interactable = true;
        }
    }

    public void DisSelect()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            image = transform.GetChild(i).GetComponent<Image>();
            image.color = new Color(image.color.r, image.color.g, image.color.b, 0);
        }
    }

    public void BuyComfirmation()
    {
        ComfirmationPanel.SetActive(true);
        comfirmationText.text = "Sure want to buy " + SelectedWeaponName + " ?";
    }

    public void comfirmBuy()
    {
        SelectedWeapon.Owned = true;
        Inventory.GetComponent<InventoryItemDisplay>().AddItem(SelectedWeapon);
        InfoDisplay(SelectedWeapon);
        GameManager.instance.Gold -= SelectedWeapon.Gold;
        GameManager.instance.Rope -= SelectedWeapon.Rope;
        GameManager.instance.Branches -= SelectedWeapon.Branches;
        GameManager.instance.BuyEquipIDGenerator();
        GameManager.instance.SaveGame();
    }
}
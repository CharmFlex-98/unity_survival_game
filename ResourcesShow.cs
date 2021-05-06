using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResourcesShow : MonoBehaviour
{
    public TextMeshProUGUI GoldText, BranchText, RopeText;
    public void ResourcePanelUpdate()
    {
        GoldText.text = "<sprite=0>" + GameManager.instance.Gold;
        BranchText.text = "<sprite=2>" + GameManager.instance.Branches;
        RopeText.text = "<sprite=1>" + GameManager.instance.Rope;
    }
}

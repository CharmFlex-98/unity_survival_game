using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{
    public int PlayerGold;
    public int PlayerRope;
    public int PlayerBranches;
    public int Day;
    public string PlayerBuyEquipID;
    public PlayerData(GameManager data)
    {
        PlayerGold = data.Gold;
        PlayerRope = data.Rope;
        PlayerBranches = data.Branches;
        Day = data.Day;
        PlayerBuyEquipID = data.BuyEquipID;
    }
}

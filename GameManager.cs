using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int Gold,BountyGold;
    public int Rope,BountyRope;
    public int Branches,BountyBranch;
    [HideInInspector] public bool ItemUpdated = false;
    public enum Mode{InHouse,Task,Daily,Adventure};
    public Mode mode;
    [HideInInspector] public int Day = 1;
    public GameObject Inventory;
    public GameObject GoldText, RopeText, BranchText;
    public GameObject EffectCanvas;
    [HideInInspector]public bool inNewPlace = false;
    [HideInInspector] public GameObject ActiveButton;
    public MyWeapon[] weapon;
    public string BuyEquipID="";
    int PoolNum = 5;
    List<GameObject> ArrowPool;
    public GameObject ArrowPrefab;
    public GameObject DyingPanel;
    public ResourcesShow RS;
   
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            
        }
        
    }

    private void Start()
    {
        LoadGame();
       
        
    }



    public void DailyScene()
    {
        SceneManager.LoadScene(2);
        mode = Mode.Daily;
       
    }

    public void InHouseScene()
    {
        SceneManager.LoadScene(1);
        mode = Mode.InHouse;
        
    }

   

   public void OpenInventory()
    {
        Inventory.SetActive(true);
        RS.ResourcePanelUpdate();

    }

    public void GoldEarned()
    {
        Gold += BountyGold;
        GameObject Text=Instantiate(GoldText, EffectCanvas.transform);
        Text.GetComponent<TextMeshProUGUI>().text = "<sprite=0>+" + BountyGold.ToString();
    }


    public void RopeEarned()
    {
        Rope += BountyRope;
        GameObject Text=Instantiate(RopeText, EffectCanvas.transform);
        Text.GetComponent<TextMeshProUGUI>().text ="<sprite=1>+" + BountyRope.ToString();
    }
        

    public void BranchesEarned()
    {
        Branches += BountyBranch;
        GameObject Text=Instantiate(BranchText, EffectCanvas.transform);
        Text.GetComponent<TextMeshProUGUI>().text = "<sprite=2>+" + BountyBranch.ToString();
    }

    public void SaveGame()
    {
        SaveSystem.Save(this);
    }

    public void LoadGame()
    {
        PlayerData loadData = SaveSystem.Load();
        Gold = loadData.PlayerGold;
        Rope = loadData.PlayerRope;
        Branches = loadData.PlayerBranches;
        Day = loadData.Day;
        
        if (loadData.PlayerBuyEquipID.Length!=0)
        {
            BuyEquipID = loadData.PlayerBuyEquipID;
            int counter = 0;
            for (int i = 0; i < weapon.Length; i++)
            {
                if (BuyEquipID[counter].Equals('1'))
                {
                    
                    weapon[i].Owned = true;
                }
                else
                {
                    
                    weapon[i].Owned = false;
                }
                counter++;
                

                if (BuyEquipID[counter].Equals('1'))
                {
                   
                    weapon[i].OnEquip = true;
                }
                else
                {
                    
                    weapon[i].OnEquip = false;
                }
                counter++;
                
            }
            
        }
        else
        {

            return;
        }
        
        
    }

    public void BuyEquipIDGenerator()
    {
        BuyEquipID = null;
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i].Owned)
            {
                BuyEquipID = BuyEquipID + "1";
            }
            else
            {
                BuyEquipID = BuyEquipID + "0";
            }

            if (weapon[i].OnEquip)
            {
                BuyEquipID = BuyEquipID + "1";
            }
            else
            {
                BuyEquipID = BuyEquipID + "0";
            }

        }
    }

    public void ArrowPooling()
    {
        ArrowPool = new List<GameObject>();
        for (int i = 0; i <PoolNum; i++)
        {
            GameObject Arrow = Instantiate(ArrowPrefab);
            Arrow.SetActive(false);
            ArrowPool.Add(Arrow);
        }
    }

    public GameObject DrawArrowFromPool()
    {
        for (int i = 0; i < ArrowPool.Count; i++)
        {
            if (!ArrowPool[i].activeInHierarchy)
            {
                if (i != 4)
                {
                    ArrowPool[i + 1].SetActive(false);
                }
                else
                {
                    ArrowPool[0].SetActive(false);
                }
                return ArrowPool[i];
            }
        }
        return null;
        
    }

    public void DyingPanelActive()
    {
        DyingPanel.SetActive(true);
    }

   

}

  

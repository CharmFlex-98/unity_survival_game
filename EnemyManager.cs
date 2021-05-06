using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    Transform[] _EnemySpawnSpot=new Transform[8];
    float CD = 5;
    public float CDMax, CDMin;
    enum State { Peace,CountDown, Refresh };
    State state;
    public GameObject Enemy;
    public Transform target;
    public int ZombieMaxNum;
    int ZombieOutNum = 0;
    [HideInInspector] public int ZombieDieNum = 0;
    bool ZombieAllOut,_LevelComplete;
    public GameObject LevelCompleteMenu;
    public FPS_Controller fpscontroller;
    public TextMeshProUGUI ZombiesNumText;
    
    


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            _EnemySpawnSpot[i] = transform.GetChild(i).transform;
        }
        if (GameManager.instance.mode == GameManager.Mode.Daily)
        {
            state = State.CountDown;
        }
        else
        {
            state = State.Peace;
        }
        LeftZombiesCount();

    }
    private void Update()
    {
        if (!ZombieAllOut)
        { 
            if (state == State.Refresh)
            {
                int x = Random.Range(0, transform.childCount);
                GameObject Zombies = Instantiate(Enemy, _EnemySpawnSpot[x].position, Quaternion.identity);
                Zombies.GetComponent<EnemyPathFinding>().Target = target;
                ZombieOutNum++;
                //Zombies.GetComponent<ZombieManager>().target = target;
                CD = Random.Range(CDMax, CDMin);
                state = State.CountDown;
            }

            if (state == State.CountDown)
            {
                CD -= Time.deltaTime;
                if (CD <= 0)
                {
                    state = State.Refresh;
                }
            }

          
        }
        if (ZombieOutNum == ZombieMaxNum)
        {
            ZombieAllOut=true;
        }
        if (!_LevelComplete)
        {
            if (ZombieDieNum == ZombieOutNum && ZombieAllOut)
            {
                _LevelComplete = true;
                StartCoroutine("LevelComplete");

            }
        }
       

      


    }
    IEnumerator LevelComplete()
    {
        if (fpscontroller.Playerdied == true)
        {
            yield return null;
        }
        else
        {
            GameManager.instance.Day++;
            yield return new WaitForSeconds(2);
            LevelCompleteMenu.SetActive(true);
            GameManager.instance.GoldEarned();
            GameManager.instance.SaveGame();
        }
        
        
     
    }

    public void LeftZombiesCount()
    {
        int zombiesNum = ZombieMaxNum - ZombieDieNum;
        ZombiesNumText.text ="Enemy: "+zombiesNum.ToString();
    }

}

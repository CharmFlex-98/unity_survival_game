using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ZombieManager : MonoBehaviour
{
    public float MaxHealth;
    float health;
    Animator anim;
    public Slider HealthBar;
    //public Transform target;
    public GameObject Canvas;
    Camera cam;
    public float HealthMultiplier = 2;
    bool died;
    [HideInInspector]public Vector3 OriginPos;
    [HideInInspector]public Quaternion OriginRot;
    public float stunTime;
    [HideInInspector]public bool Moaning =false;
    ZombieAudio zombieAudio;
    public GameObject[] HandAttack;


    private void Start()
    {
        OriginPos = transform.position;
        OriginRot = transform.rotation;
        MaxHealth = MaxHealth + (GameManager.instance.Day - 1) * HealthMultiplier;
        health = MaxHealth;
        anim=GetComponent<Animator>();
        cam = Camera.main;
        zombieAudio = GetComponent<ZombieAudio>();
    }
    private void Update()
    {
        
        if (GameManager.instance.mode == GameManager.Mode.Daily)
        {
            if (!died)
            {
                if (health <= 0)
                {
                    ZombieDie();
                }
                HealthBar.value = (health / MaxHealth);
            }
        }

        else
        {
            if (!died)
            {
                if (health <= 0)
                {
                    died = true;
                    zombieAudio.ZombieMoaning.Stop();
                    zombieAudio.ZombieDying.Play();
                    Moaning = false;
                    anim.SetBool("Zombie_isAlive", false);
                    for (int i = 0; i < HandAttack.Length; i++)
                    {
                        HandAttack[i].SetActive(false);
                    }
                    GetComponent<CapsuleCollider>().enabled = false;
                    GetComponent<EnemyPathFinding>().enabled = false;
                    GetComponent<NavMeshAgent>().enabled = false;
                    GetComponent<AnimSpeedController>().die = true;
                    HealthBar.gameObject.SetActive(false);
                    GameManager.instance.BranchesEarned();
                    StartCoroutine("ZombiRevive");
                }
                HealthBar.value = (health / MaxHealth);

            }
        }

        if (Moaning == false)
        {
            if (died==false)
            {
                zombieAudio.ZombieMoaning.Play();
                Moaning = true;
            }
            
            
        }
    }

    private void FixedUpdate()
    {
        Quaternion Look = Quaternion.LookRotation(cam.transform.forward);
        Canvas.transform.rotation = Look;
    }


    public void TakeDamage(float damage)
    {
        
        health -= damage;
        StartCoroutine("BeingAttacked");

    }

    void ZombieDie()
    {

        zombieAudio.ZombieMoaning.Stop();
        zombieAudio.ZombieDying.Play();
        Moaning = false;
        died = true;
        EnemyManager.instance.ZombieDieNum++;
        EnemyManager.instance.LeftZombiesCount();
        anim.SetBool("Zombie_isAlive",false);
        HealthBar.gameObject.SetActive(false);
        for (int i = 0; i < HandAttack.Length; i++)
        {
            HandAttack[i].SetActive(false);
        }
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<EnemyPathFinding>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<AnimSpeedController>().die = true;
        GameManager.instance.BranchesEarned();
    }

    IEnumerator ZombiRevive()
    {

        yield return new WaitForSeconds(10);
        anim.SetBool("Zombie_isAlive", true);
        died = false;
        transform.position = OriginPos;
        transform.rotation = OriginRot;
        for (int i = 0; i < HandAttack.Length; i++)
        {
            HandAttack[i].SetActive(true);
        }
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<EnemyPathFinding>().Target = GetComponent<EnemyPathFinding>().OriginalTarget;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<EnemyPathFinding>().enabled = true;
        GetComponent<AnimSpeedController>().die = false;
        MaxHealth = MaxHealth + (GameManager.instance.Day - 1) * HealthMultiplier;
        health = MaxHealth;
        HealthBar.gameObject.SetActive(true);

    }

    IEnumerator BeingAttacked()
    {
        float time = Time.time;
        while (Time.time - time <= stunTime)
        {
            GetComponent<NavMeshAgent>().enabled = false;
            anim.SetBool("ZombiesBeingAttacked",true);
            yield return null;
        }
        anim.SetBool("ZombiesBeingAttacked", false);
        if (died == false)
        {
           
            GetComponent<NavMeshAgent>().enabled = true;
            anim.enabled = true;
        }
       
    }
}

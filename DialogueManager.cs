using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject DialogueCanvas;
    public GameObject NPCPanel,TargetNPCPanel;
    public GameObject TalkButton;
    [HideInInspector] public GameObject ActiveNPC;
    public GameObject TaskButton, ChatButton;
    public GameObject ExitButton;
    public GameObject EnterButton, ExitCaveButton;
    public GameObject FadeScreen;
    public GameObject CharTeleport;
    public Transform TeleportInPoint,TeleportOutPoint;
    public GameObject NewPlace;
    public GameObject MaterialGirlInnew;
    public GameObject MaterialGirl;
    public GameObject MainCharacter;
    public GameObject AcceptTaskButton;
    public GameObject TaskAcceptedText;

    
    public GameObject Content;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (GameManager.instance.mode == GameManager.Mode.Task)
        {
            AcceptTaskButton.SetActive(false);
            TaskAcceptedText.SetActive(true);
        }
        else if (GameManager.instance.mode != GameManager.Mode.Daily)
        {
            AcceptTaskButton.SetActive(true);
            TaskAcceptedText.SetActive(false);
        }
    }
    public void TalkButtonPressed()
    {
        if (ActiveNPC.CompareTag("NPC"))
        {
            NPCPanel.SetActive(true);
            
        }
        else if (ActiveNPC.CompareTag("TargetNPC"))
        {
            TargetNPCPanel.SetActive(true);
            if (GameManager.instance.mode == GameManager.Mode.Task)
            {
                TaskButton.SetActive(true);
                ChatButton.SetActive(false);
            }
            else
            {
                TaskButton.SetActive(false);
                ChatButton.SetActive(true);
            }
        }
        else
        {
            return;
        }
    }
    public void InhouseScene()
    {
        GameManager.instance.InHouseScene();
    }
    public void DailyScene()
    {
        GameManager.instance.DailyScene();
    }

    public void Fade()
    {
        Instantiate(FadeScreen, DialogueCanvas.transform);
        StartCoroutine("Teleport");
    }

    public void AdventureMode()
    {
        GameManager.instance.mode = GameManager.Mode.Adventure;
    }

    public void TaskComplete()
    {
        Instantiate(FadeScreen, DialogueCanvas.transform);
        StartCoroutine("Complete");
    }

    public void TaskAccepted()
    {
        GameManager.instance.mode = GameManager.Mode.Task;
    }



    IEnumerator Teleport()
    {
        yield return new WaitForSeconds(1);
        if (GameManager.instance.mode == GameManager.Mode.Task || GameManager.instance.mode == GameManager.Mode.InHouse)
        {
            if (GameManager.instance.inNewPlace == false)
            {
                NewPlace.SetActive(true);
                GameManager.instance.inNewPlace = true;
                CharTeleport.transform.position = TeleportInPoint.position;
                CharTeleport.transform.rotation = TeleportInPoint.rotation;
            }
            else
            {
                NewPlace.SetActive(false);
                GameManager.instance.inNewPlace = false;
                CharTeleport.transform.position = TeleportOutPoint.position;
                CharTeleport.transform.rotation = TeleportOutPoint.rotation;
            }
        }
        else if (GameManager.instance.mode == GameManager.Mode.Adventure)
        {
            NewPlace.SetActive(false);
            GameManager.instance.inNewPlace = false;
            CharTeleport.transform.position = TeleportOutPoint.position;
            CharTeleport.transform.rotation = TeleportOutPoint.rotation;
            MaterialGirlInnew.GetComponent<Follower>()._target = null;
            MaterialGirlInnew.transform.position=MaterialGirlInnew.GetComponent<Follower>().InitialPos;
            MaterialGirl.SetActive(true);
            MaterialGirl.GetComponent<Follower>()._target = MaterialGirl.GetComponent<Follower>().target;
        }
    }

    IEnumerator Complete()
    {
        yield return new WaitForSeconds(1);
        MaterialGirl.GetComponent<Follower>()._target = null;
        MaterialGirl.transform.position = MaterialGirl.GetComponent<Follower>().InitialPos;
        MaterialGirl.SetActive(false);
        StartCoroutine("RopeEarned");
        GameManager.instance.mode = GameManager.Mode.InHouse;
        MainCharacter.GetComponent<FPS_Controller>().Back2Origin();
    }

    IEnumerator RopeEarned()
    {
        yield return new WaitForSeconds(2);
        GameManager.instance.RopeEarned();
        GameManager.instance.SaveGame();
    }
}

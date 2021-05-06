using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveAndSave : MonoBehaviour
{
    public void SaveLeave()
    {
        GameManager.instance.SaveGame();
        Application.Quit();
        
    }
}

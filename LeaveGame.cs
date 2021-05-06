using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveGame : MonoBehaviour
{
    public GameObject ExitGamePanel;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ExitGamePanel.SetActive(true);
        }

        
    }
}

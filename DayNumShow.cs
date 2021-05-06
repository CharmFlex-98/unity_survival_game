using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DayNumShow : MonoBehaviour
{
    public TextMeshProUGUI DayText;
    int DayCleared;

    private void Start()
    {
        DayCleared = GameManager.instance.Day - 1;
        DayText.text = "Day " + DayCleared.ToString();
    }
}

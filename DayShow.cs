
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DayShow : MonoBehaviour
{
    public TextMeshProUGUI text;
 
    void Start()
    {
        text.text = "Day " + GameManager.instance.Day;
    }

    
}

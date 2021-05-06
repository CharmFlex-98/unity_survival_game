using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasEffects : MonoBehaviour
{
    public static CanvasEffects instance;
    
    public void Awake()
    {
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
}

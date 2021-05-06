using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotDestroy1 : MonoBehaviour
{
    public static DoNotDestroy1 instance;

    private void Awake()
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

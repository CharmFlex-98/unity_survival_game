using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInvEnv : MonoBehaviour
{
    public static SingletonInvEnv instance;

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

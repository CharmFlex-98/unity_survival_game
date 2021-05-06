﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonInv : MonoBehaviour
{
    public static SingletonInv instance;

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
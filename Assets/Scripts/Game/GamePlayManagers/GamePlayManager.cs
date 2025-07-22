using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayManager 
{
    public static GamePlayManager Instance { get; private set; }

    public void Init()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new Exception("An instance of GamePlayManager already exists.");
        }
    }

    public void Destroy()
    {
        if (Instance != null)
        {
            Instance = null;
        }
        else
        {
            throw new Exception("No instance of GamePlayManager to destroy.");
        }
    }
}

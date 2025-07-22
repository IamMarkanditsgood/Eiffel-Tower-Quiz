using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PoolObjectManager
{/*
    [SerializeField] private BasicRoomManager basicRoomManagerPrefab;

    [SerializeField] private Transform _basicRoomManagerContainer;

    public ObjectPool<BasicRoomManager> basicRoomManagerPool = new();
*/
    public static PoolObjectManager instant;

    public void Init()
    {
        if (instant == null)
        {
            instant = this;
        }
        InitPoolObjects();
    }

    public void DeInit()
    {
        if (instant != null)
        {
            instant = null;
        }
    }

    private void InitPoolObjects()
    {
        //basicRoomManagerPool.InitializePool(basicRoomManagerPrefab, _basicRoomManagerContainer);
    }
}

using System;

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

    public void Subscribe()
    {
        // Add subscription logic here if needed
    }

    public void UnSubscribe()
    {
        // Add unsubscription logic here if needed
    }
}

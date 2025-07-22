using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GamePlayManager 
{
    public static GamePlayManager Instance { get; private set; }

    [SerializeField] private List<CategoryConfig> _categoryConfigs;

    public List<CategoryConfig> CategoryConfigs => _categoryConfigs;

    public CategoryConfig CurrentCategory { get; private set; }

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

    public void SetCurrentCategory(CategoryTypes category)
    {
        CurrentCategory = GetCategoryConfig(category);
    }

    private CategoryConfig GetCategoryConfig(CategoryTypes categoryType)
    {
        foreach (CategoryConfig categoryConfig in _categoryConfigs)
        {
            if(categoryConfig.CategoryType == categoryType) { return categoryConfig; }
        }
        return null;
    }
}
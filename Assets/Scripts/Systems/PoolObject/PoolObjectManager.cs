using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PoolObjectManager
{
    [SerializeField] private CategoryButton _categoryButtonPrefab;
    [SerializeField] private AnswerButton _answerButtonPrefab;
    [SerializeField] private Image _confettiPrefab;

    [SerializeField] private Transform _categoryButtonContainer;
    [SerializeField] private Transform _answerContainer;
    [SerializeField] private Transform _confettiContainer;

    public ObjectPool<CategoryButton> CategoryButtonPool = new();
    public ObjectPool<AnswerButton> AnserButtonPool = new();
    public ObjectPool<Image> ConfettiPool = new();

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
        CategoryButtonPool.InitializePool(_categoryButtonPrefab, _categoryButtonContainer);
        AnserButtonPool.InitializePool(_answerButtonPrefab, _answerContainer);
        ConfettiPool.InitializePool(_confettiPrefab, _confettiContainer, 50);
    }
}

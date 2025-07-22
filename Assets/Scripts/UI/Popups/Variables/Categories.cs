using System.Collections.Generic;
using UnityEngine;

public class Categories : BasicPopup
{
    private List<CategoryButton> _categories = new List<CategoryButton>();
    private List<CategoryConfig> _categoryConfig = new List<CategoryConfig>();

    public override void ResetPopup()
    {
        ResetCategoryButtons();
    }

    public override void SetPopup()
    {
        _categoryConfig = GamePlayManager.Instance.CategoryConfigs;
        SetCategoryButtons();
    }

    private void SetCategoryButtons()
    {
        for(int i = 0; i < _categoryConfig.Count; i++)
        {
            CategoryButton categoryButton = PoolObjectManager.instant.CategoryButtonPool.GetFreeComponent();
            _categories.Add(categoryButton);
            categoryButton.SetLabelText(_categoryConfig[i].CategoryName);
        }

        SubscribeCategoryButtons(_categories);
    }

    private void ResetCategoryButtons()
    {
        foreach (CategoryButton categoryButton in _categories)
        {
            PoolObjectManager.instant.CategoryButtonPool.DisableComponent(categoryButton);
        }

        UnSubscibeCategoryButtons(_categories);
        _categories.Clear();
    }

    private void SubscribeCategoryButtons(List<CategoryButton> categoryButtons)
    {
        for(int i = 0; i < categoryButtons.Count; i++)
        {
            int index = i;
            categoryButtons[index].ButtonComponent.onClick.AddListener(() => CategoryButtonPressed(index));
        }
    }

    private void UnSubscibeCategoryButtons(List<CategoryButton> categoryButtons)
    {
        for (int i = 0; i < categoryButtons.Count; i++)
        {
            int index = i;
            categoryButtons[index].ButtonComponent.onClick.RemoveListener(() => CategoryButtonPressed(index));
        }
    }

    private void CategoryButtonPressed(int index)
    {
        CategoryTypes type = _categoryConfig[index].CategoryType;
        GamePlayManager.Instance.SetCurrentCategory(type);
        UIManager.Instance.ShowScreen(ScreenTypes.GamePlay);
        Hide();
    }
}
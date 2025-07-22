using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : BasicInteractiveTextButton
{
    public void SetCategoryName(string name) => SetLabelText(name);
}

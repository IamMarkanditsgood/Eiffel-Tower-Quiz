using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuizCategory", menuName = "ScriptableObjects/GamePlay/QuizCategory", order = 1)]

public class CategoryConfig : ScriptableObject
{
    [SerializeField] private CategoryTypes _categoryType;
    [SerializeField] private string _categoryName;
    [SerializeField] private List<QuizQuestionData> quizQuestions;

    public CategoryTypes CategoryType => _categoryType;
    public string CategoryName => _categoryName;
    public List<QuizQuestionData> QuizQuestions => quizQuestions;
}
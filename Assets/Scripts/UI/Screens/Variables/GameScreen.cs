using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BasicScreen
{
    [SerializeField] private ConfettiEffectUI _confettiEffectUI;
    [SerializeField] private Image _questionImage;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private float _delayBeforeNextQuestion;

    private List<AnswerButton> _answerButtons = new();

    private List<QuizQuestionData> _quizQuestions = new();

    private int _currentQuestion = 0;
    private bool _canSelectAnswer;

    public override void ResetScreen()
    {
        _currentQuestion = 0;
        _quizQuestions = null;  
        ResetCategoryButtons();
    }

    public override void SetScreen()
    {
        _quizQuestions = GamePlayManager.Instance.CurrentCategory.QuizQuestions;
        NextQuestion();
        _canSelectAnswer = true;
    }

    private void NextQuestion()
    {
        SetQuestion();
        SetAnswers();
    }

    private void SetQuestion()
    {
        _questionImage.sprite = _quizQuestions[_currentQuestion].questionImage;
        _questionText.text = _quizQuestions[_currentQuestion].questionText;
    }

    private void SetAnswers()
    {
        ResetCategoryButtons();
        SetCategoryButtons(_quizQuestions[_currentQuestion].answers);
    }

    private void SetCategoryButtons(List<string> answers)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            AnswerButton categoryButton = PoolObjectManager.instant.AnserButtonPool.GetFreeComponent();
            _answerButtons.Add(categoryButton);
            categoryButton.SetReplyText(answers[i]);
        }

        SubscribeCategoryButtons(_answerButtons);
    }

    private void ResetCategoryButtons()
    {
        foreach (AnswerButton answerButton in _answerButtons)
        {
            PoolObjectManager.instant.AnserButtonPool.DisableComponent(answerButton);
        }

        UnSubscibeCategoryButtons(_answerButtons);
        _answerButtons.Clear();
    }

    private void SubscribeCategoryButtons(List<AnswerButton> answerButtons)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[index].ButtonComponent.onClick.AddListener(() => AnswerPressed(index));
        }
    }

    private void UnSubscibeCategoryButtons(List<AnswerButton> answerButtons)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[index].ButtonComponent.onClick.RemoveListener(() => AnswerPressed(index));
        }
    }

    private void AnswerPressed(int answerIndex)
    {
        if (!_canSelectAnswer) return;

        _canSelectAnswer = false;

        if (_quizQuestions[_currentQuestion].correntAnswer == answerIndex)
        {
            CorrectReply();
        }
        else
        {
            WrongReply();
        }      
    }

    private void CorrectReply()
    {
        StartCoroutine(ShowResult(true));

        int nextQuestion = _currentQuestion + 1;
        if (nextQuestion < _quizQuestions.Count - 1)
        {
            _currentQuestion++;
            NextQuestion();
        }
    }

    private void WrongReply()
    {
        StartCoroutine(ShowResult(false));
    }

    private IEnumerator ShowResult(bool isCorect)
    {
        _messageText.gameObject.SetActive(true);
        if (isCorect)
        {
            _confettiEffectUI?.PlayConfetti(_delayBeforeNextQuestion);
            _messageText.text = "Congratulations! You've won!";
        }
        else
        {
            _messageText.text = "Unfortunately, this is an incorrect answer. You lose.";
        }

        yield return new WaitForSeconds(_delayBeforeNextQuestion);

        _messageText.gameObject.SetActive(false);
        _canSelectAnswer = true;
    }
}

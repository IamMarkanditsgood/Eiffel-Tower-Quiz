using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScreen : BasicScreen
{
    [SerializeField] private ConfettiEffectUI _confettiEffectUI;
    [SerializeField] private RectTransform _answerButtonsContent;
    [SerializeField] private Image _questionImage;
    [SerializeField] private TMP_Text _questionText;
    [SerializeField] private TMP_Text _messageText;
    [SerializeField] private float _delayBeforeNextQuestion;
    [Header("Message")]
    [SerializeField] private string _correctMessage = "Congratulations! You've won!";
    [SerializeField] private string _wrongMessage = "Unfortunately, this is an incorrect answer. You lose.";

    private List<InteractiveLabledButton> _answerButtons = new();
    private GamePlayManager _gamePlayManager;

    private bool _canSelectAnswer;

    public override void ResetScreen()
    {
        _gamePlayManager = null;
        ResetCategoryButtons();
    }

    public override void SetScreen()
    {
        _gamePlayManager = GamePlayManager.Instance;
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
        _questionImage.sprite = _gamePlayManager.GetCurrentQuestionImage();
        _questionText.text = _gamePlayManager.GetCurrentQuestionText();
    }

    private void SetAnswers()
    {
        ResetCategoryButtons();
        SetCategoryButtons(_gamePlayManager.GetCurrentAnswers());
    }

    private void SetCategoryButtons(List<string> answers)
    {
        for (int i = 0; i < answers.Count; i++)
        {
            InteractiveLabledButton categoryButton = PoolObjectManager.instant.AnserButtonPool.GetFreeComponent();
            _answerButtons.Add(categoryButton);
            categoryButton.transform.SetParent(_answerButtonsContent, false);
            categoryButton.SetLabelText(answers[i]);
        }

        SubscribeCategoryButtons(_answerButtons);
    }

    private void ResetCategoryButtons()
    {
        foreach (InteractiveLabledButton answerButton in _answerButtons)
        {
            PoolObjectManager.instant.AnserButtonPool.DisableComponent(answerButton);
        }

        UnSubscibeCategoryButtons(_answerButtons);
        _answerButtons.Clear();
    }

    private void SubscribeCategoryButtons(List<InteractiveLabledButton> answerButtons)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[index].ButtonComponent.onClick.AddListener(() => AnswerPressed(index));
        }
    }

    private void UnSubscibeCategoryButtons(List<InteractiveLabledButton> answerButtons)
    {
        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[index].ButtonComponent.onClick.RemoveAllListeners();
        }
    }

    private void AnswerPressed(int answerIndex)
    {
        if (!_canSelectAnswer) return;

        _canSelectAnswer = false;

        if (_gamePlayManager.IsCorrectAnswer(answerIndex))
        {
            StartCoroutine(CorrectReply()); 
        }
        else
        {
            StartCoroutine(WrongReply()); 
        }

    }

    private IEnumerator CorrectReply()
    {
        // here you can add logic to handle correct answer
        yield return StartCoroutine(ShowResult(true));
        HandleNextStep();
    }

    private IEnumerator WrongReply()
    {
        // here you can add logic to handle wrong answer
        yield return StartCoroutine(ShowResult(false));
        HandleNextStep();
    }

    private void HandleNextStep()
    {
        if (_gamePlayManager.IsLastQuestion())
        {
            //here you can add logic to finish the game or show final results
        }
        else
        {
            //Prepare for the next question
            _gamePlayManager.NextQuestion();
            NextQuestion();
        }
    }

    private IEnumerator ShowResult(bool isCorrect)
    {
        _messageText.gameObject.SetActive(true);
        if (isCorrect)
        {
            _confettiEffectUI?.PlayConfetti(_delayBeforeNextQuestion);
            _messageText.text = _correctMessage;
        }
        else
        {
            _messageText.text = _wrongMessage;
        }

        yield return new WaitForSeconds(_delayBeforeNextQuestion);

        _messageText.gameObject.SetActive(false);
        _canSelectAnswer = true;
    }
}
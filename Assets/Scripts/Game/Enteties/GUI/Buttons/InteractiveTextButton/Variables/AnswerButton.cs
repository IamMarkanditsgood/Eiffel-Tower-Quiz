using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : BasicInteractiveTextButton
{
    public void SetReplyText(string reply) => SetLabelText(reply);
}

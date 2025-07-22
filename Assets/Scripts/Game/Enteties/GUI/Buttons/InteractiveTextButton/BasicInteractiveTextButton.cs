using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicInteractiveTextButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Button _button;
    [SerializeField] protected TMP_Text _label;
    [Header("Interaction")]
    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _hoverColor = Color.cyan;
    private Image _image;
    [Header("Cursor")]
    [SerializeField] private Texture2D _cursorTexture;
    [SerializeField] private Vector2 _hotspot = Vector2.zero;

    public Button ButtonComponent => _button;

    protected virtual void Awake()
    {
        if (_button == null)
        {
            _button = GetComponent<Button>();
            if (_button == null)
            {
                Debug.LogWarning($"{GetType().Name} on {gameObject.name} had no Button — one was added.");
                _button = gameObject.AddComponent<Button>();
            }
        }

        _image = _button.GetComponent<Image>();
        if (_image == null)
        {
            Debug.LogWarning($"{GetType().Name} on {gameObject.name} had no Image — one was added.");
            _image = _button.gameObject.AddComponent<Image>();
        }
    }
    private void OnDisable()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    private void OnDestroy()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_image != null)
            _image.color = _hoverColor;

        Cursor.SetCursor(_cursorTexture != null ? _cursorTexture : null, _hotspot, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_image != null)
            _image.color = _normalColor;

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public virtual void SetLabelText(string text)
    {
        if (_label != null)
            _label.text = text;
    }
}

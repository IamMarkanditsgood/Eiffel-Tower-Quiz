using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BasicUIInteractable : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource buttonsSource;
    [SerializeField] private AudioClip selectSound;
    [SerializeField] private AudioClip pressSound;
    [Header("UI Selection elements")]
    [Header("Switching objects")]
    [SerializeField] private bool _switchObject;
    [SerializeField] private GameObject[] _selectionObject;
    [Header("Switching objects")]
    [SerializeField] private bool _switchElements;
    [SerializeField] private Image[] _selectionElements;
    [Header("Switching gradients")]
    [SerializeField] private bool _switchGradient;
    [SerializeField] private TMP_Text[] _selectionText;
    [Header("Switching alfa")]
    [SerializeField] private bool _switchAlfa;
    [SerializeField] private float _defaulAlfa;
    [SerializeField] private float _newAlfa;
    [SerializeField] private Image[] _alfaImage;
    [SerializeField] private TMP_Text[] _alfaText;
    [SerializeField] private CanvasGroup[] _alfaCanvasGroup;
    [Header("Switching colors")]
    [SerializeField] private bool _switchColor;
    [SerializeField] private Color _defaultColor;
    [SerializeField] private Color _changedColor;
    [SerializeField] private TMP_Text[] _colorText;


    public int index = -1;
    public event Action<int> OnSelected;
    public event Action<int> OnDeselected;

    private Button _button;

    private void Start()
    {
        _button = gameObject.GetComponent<Button>();
        if (_button != null)
        {
            _button.onClick.AddListener(Press);
        }
    }

    private void OnDestroy()
    {
        if (_button != null)
        {
            _button.onClick.RemoveListener(Press);
        }
    }

    private void OnDisable()
    {
        HideSelection();
    }

    private void Press()
    {
        if (buttonsSource && pressSound)
        {
            buttonsSource.PlayOneShot(pressSound);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(index+  " " + this.gameObject);
        OnSelected?.Invoke(index);
        if (audioSource && selectSound)
        {
            audioSource.PlayOneShot(selectSound);
        }
        ShowSelection();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(index + " deselected " + this.gameObject);
        OnDeselected?.Invoke(index);
        HideSelection();
    }

    private void ShowSelection()
    {
        ToggleComponent(true);
        ToggleElements(true);
        ToggleGradient(true);
        SwitchAlfa(true);
        SwitchColor(true);
    }

    private void HideSelection()
    {
        ToggleComponent(false);
        ToggleElements(false);
        ToggleGradient(false);
        SwitchAlfa(false);
        SwitchColor(false);
    }

    private void ToggleComponent(bool state)
    {
        if(!_switchObject) return;

        foreach (var obj in _selectionObject)
        {
            obj.SetActive(state);
        }
    }

    private void ToggleElements(bool state)
    {
        if(!_switchElements) return;

        foreach(var element in _selectionElements)
        {
            element.enabled = state;
        }
    }

    private void ToggleGradient(bool state)
    {
        if (!_switchGradient) return;

        foreach (var text in _selectionText)
        {
            text.enableVertexGradient = state;
        }
    }

    private void SwitchAlfa(bool state)
    {
        if (!_switchAlfa) return;

        foreach (var image in _alfaImage)
        {
            if (image != null)
            {
                Color color = image.color;
                color.a = state ? _newAlfa : _defaulAlfa;
                image.color = color;
            }
        }

        foreach (var text in _alfaText)
        {
            if (text != null)
            {
                Color color = text.color;
                color.a = state ? _newAlfa : _defaulAlfa;
                text.color = color;
            }
        }
        foreach (var canvas in _alfaCanvasGroup)
        {
            if (canvas != null)
            {
                canvas.alpha = state ? _newAlfa : _defaulAlfa;
            }
        }
    }

    private void SwitchColor(bool state)
    {
        if (!_switchColor) return;

        Color newColor;
        if(state)
        {
            newColor = _changedColor;
        }
        else
        {
            newColor = _defaultColor;
        }

        foreach (var text in _colorText)
        {
           
            if (text != null)
            {
                Debug.Log(text + " " + newColor);
                text.color = newColor;
            }
        }
    }
}

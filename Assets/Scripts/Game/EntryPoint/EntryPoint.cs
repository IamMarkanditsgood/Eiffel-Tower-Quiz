using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;

    private void Awake()
    {
        if (_uiManager == null)
        {
            Debug.LogError("UIManager is not assigned in EntryPoint.");
            return;
        }

        _uiManager.Init();
    }

    private void OnDestroy()
    {
        if (_uiManager != null)
        {
            _uiManager.Destroy();
        }
    }
}

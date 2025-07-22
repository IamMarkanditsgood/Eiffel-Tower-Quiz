using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GamePlayManager _gamePlayManager;

    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        Subscribe();
    }
    
    private void OnDisable()
    {
        UnSubscribe();
    }

    private void OnDestroy()
    {
        UnSubscribe();
        Destroy();
    }

    private void Init()
    {
        _uiManager?.Init();
        _gamePlayManager?.Init();
    }

    private void Destroy()
    {
        _uiManager?.Destroy();
        _gamePlayManager?.Destroy();
    }

    private void Subscribe()
    {
        _uiManager?.Subscribe();
        _gamePlayManager?.Subscribe();
    }

    private void UnSubscribe()
    {
        _uiManager?.UnSubscribe();
        _gamePlayManager?.UnSubscribe();
    }
}

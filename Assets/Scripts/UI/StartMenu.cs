using UnityEngine;
using UnityEngine.UI;

public class StartMenu : Menu
{
    [SerializeField] private GameObject _startMenu;
    [SerializeField] private Button _closeButton;

    private void OnEnable()
    {
        _closeButton.onClick.AddListener(() => ClosePanel(_startMenu));
        _closeButton.onClick.AddListener(Spawner.SetGameStart);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveListener(() => ClosePanel(_startMenu));
        _closeButton.onClick.RemoveListener(Spawner.SetGameStart);
    }

    protected override void Start()
    {
        base.Start();
        SetDefaultState();
    }

    protected override void SetDefaultState()
    {
        OpenPanel(_startMenu);
    }
}

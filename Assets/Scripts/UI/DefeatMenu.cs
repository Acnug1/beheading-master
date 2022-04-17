using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatMenu : Menu
{
    [SerializeField] private GameObject _defeatMenu;
    [SerializeField] private Button _restartButton;
    [Min(0f)]
    [SerializeField] private float _openingTime = 3;

    private void OnEnable()
    {
        Spawner.HostageDied += OnHostageDied;
        _restartButton.onClick.AddListener(Restart);
    }

    private void OnDisable()
    {
        Spawner.HostageDied -= OnHostageDied;
        _restartButton.onClick.RemoveListener(Restart);
    }

    protected override void Start()
    {
        base.Start();
        SetDefaultState();
    }

    protected override void SetDefaultState()
    {
        _defeatMenu.SetActive(false);
    }

    private void OnHostageDied()
    {
        Invoke(nameof(OpenDefeatMenu), _openingTime);
    }

    private void OpenDefeatMenu()
    {
        OpenPanel(_defeatMenu);
    }

    private void Restart()
    {
        SceneManager.LoadScene(CurrentScene);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextLevelMenu : Menu
{
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private Button _nextLevelButton;
    [Min(0f)]
    [SerializeField] private float _openingTime = 3;

    private void OnEnable()
    {
        Spawner.AllEnemyDied += OnAllEnemyDied;
        _nextLevelButton.onClick.AddListener(TryLoadNextLevel);
    }

    private void OnDisable()
    {
        Spawner.AllEnemyDied -= OnAllEnemyDied;
        _nextLevelButton.onClick.RemoveListener(TryLoadNextLevel);
    }

    protected override void Start()
    {
        base.Start();
        SetDefaultState();
    }

    protected override void SetDefaultState()
    {
        _nextLevelMenu.SetActive(false);
    }

    private void OnAllEnemyDied()
    {
        Invoke(nameof(OpenNextLevelMenu), _openingTime);
    }

    private void OpenNextLevelMenu()
    {
        OpenPanel(_nextLevelMenu);
    }

    private void TryLoadNextLevel()
    {
        if (CurrentScene + 1 < SceneManager.sceneCountInBuildSettings)
            LoadNextLevel();
        else
            LoadFirstLevel();
    }

    private void LoadNextLevel()
    {
        CurrentScene++;
        int newScene = CurrentScene;
        SceneManager.LoadScene(newScene);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
}

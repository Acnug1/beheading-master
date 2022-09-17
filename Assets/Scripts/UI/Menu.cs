using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class Menu : MonoBehaviour
{
    [SerializeField] protected Spawner Spawner;
    [SerializeField] private LevelViewer _levelViewer;

    protected int CurrentScene;

    protected virtual void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void OpenPanel(GameObject panel)
    {
        panel.SetActive(true);
        DisableLevelViewer();
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
        EnableLevelViewer();
    }

    protected abstract void SetDefaultState();

    private void EnableLevelViewer()
    {
        _levelViewer.gameObject.SetActive(true);
        _levelViewer.Render(CurrentScene);
    }

    private void DisableLevelViewer()
    {
        _levelViewer.gameObject.SetActive(false);
    }
}

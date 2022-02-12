using TMPro;
using UnityEngine;

public class LevelViewer : MonoBehaviour
{
    [SerializeField] private TMP_Text _label;

    private const string Level = "Level {0}";

    public void Render(int levelNumber)
    {
        _label.text = string.Format(Level, levelNumber + 1);
    }
}

using TMPro;
using UnityEngine;

public class GameScoreTextHandler : MonoBehaviour
{
    private GameplayManager _gameplayManager;

    public TextMeshProUGUI Text;
    public string Format;

    // Start is called before the first frame update
    void Start()
    {
        _gameplayManager = GameplayManager.Instance;
        Text = GetComponent<TextMeshProUGUI>();
        SetScoreText(_gameplayManager.CurrentScore);
        _gameplayManager.ScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        _gameplayManager.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int value)
    {
        SetScoreText(value);
    }

    private void SetScoreText(int value)
    {
        Text.text = string.Format(Format, value.ToString());
    }
}

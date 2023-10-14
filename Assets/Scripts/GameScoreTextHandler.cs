using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameScoreTextHandler : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string format;
    public GameplayManager gameplayManager;

    // Start is called before the first frame update
    void Start()
    {
        gameplayManager = GameplayManager.Instance;
        text = GetComponent<TextMeshProUGUI>();
        SetScoreText(gameplayManager.CurrentScore);
        gameplayManager.ScoreChanged += OnScoreChanged;
    }

    private void OnDestroy()
    {
        gameplayManager.ScoreChanged -= OnScoreChanged;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnScoreChanged(int value)
    {
        SetScoreText(value);
    }

    private void SetScoreText(int value)
    {
        text.text = string.Format(format, value.ToString());
    }
}

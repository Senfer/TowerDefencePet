using Assets.Scripts.Gameplay.States;
using TMPro;
using UnityEngine;

public class GameplayStateTextHandler : MonoBehaviour
{
    public TextMeshProUGUI LevelStateText;
    public string Format;

    private void Start()
    {
        GameplayManager.Instance.GameplayStateChanged += OnGameplayStateChanged;
        SetGameplayState(GameplayManager.Instance.CurrentState);
    }

    private void OnGameplayStateChanged(GameplayState previousState, GameplayState currentState)
    {
        SetGameplayState(currentState);
    }

    private void SetGameplayState(GameplayState currentState)
    {
        switch (currentState)
        {
            case GameplayState.Building:
                SetGameplayStateText("Building");
                break;
            case GameplayState.WavesIncoming:
                SetGameplayStateText("Enemies incoming");
                break;
            case GameplayState.WavesDepleted:
                SetGameplayStateText("All enemies spawned");
                break;
            default:
                SetGameplayStateText(string.Empty);
                break;
        }
    }

    private void SetGameplayStateText(string stateName)
    {
        LevelStateText.SetText(string.Format(Format, stateName));
    }
}

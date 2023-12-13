using Assets.Scripts.Gameplay.States;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonHandler : MonoBehaviour
{
    private GameplayManager _gameplayManagerInstance;

    public Button StartButton;

    void Start()
    {
        _gameplayManagerInstance = GameplayManager.Instance;
        _gameplayManagerInstance.GameplayStateChanged += OnGameplayStateChanged;
    }

    private void OnGameplayStateChanged(GameplayState previous, GameplayState current)
    {
        switch (current)
        {
            case GameplayState.Building:
                StartButton.interactable = true;
                break;
            default:
                StartButton.interactable = false;
                break;
        }
    }
}

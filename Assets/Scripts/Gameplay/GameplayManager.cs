using Assets.Scripts;
using Assets.Scripts.Gameplay.States;
using System;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private GameplayState _currentState;

    public WaveManager WaveManager;
    public int CurrentScore;
    public GameObject SelectedTower;

    public GameplayState CurrentState => _currentState;
    public event Action<int> ScoreChanged = delegate { };
    public event Action<GameplayState, GameplayState> GameplayStateChanged = delegate { };
    public event Action TowerSelected = delegate { };
    public event Action TowerSelectionEnded = delegate { };

    protected override void Awake()
    {
        base.Awake();
        ChangeGameplayState(GameplayState.Building);   
    }

    public void StartWaves()
    {
        ChangeGameplayState(GameplayState.WavesIncoming);
    }    

    public void IncreaseScore(int value)
    {
        if (_currentState == GameplayState.WavesIncoming || _currentState == GameplayState.WavesDepleted)
        {
            if (CurrentScore > 0)
            {
                CurrentScore += value;
                ScoreChanged(CurrentScore);
            }

            if (CurrentScore <= 0)
            {
                ChangeGameplayState(GameplayState.GameOver);
            }
        }
    }

    public void SelectTowerToBuild(GameObject selectedTower)
    {
        SelectedTower = selectedTower;
        TowerSelected();
    }

    public void DeselectTower()
    {
        SelectedTower = null;
        TowerSelectionEnded();
    }

    private void ChangeGameplayState(GameplayState state)
    {
        if (_currentState == state)
        {
            return;
        }

        var previousState = _currentState;
        _currentState = state;
        GameplayStateChanged(previousState, _currentState);

        switch (_currentState)
        {
            case GameplayState.GameOver:
                SetGameOverState();
                break;
            case GameplayState.WavesIncoming:
                StartWavesInternal(); 
                break;
            default:
                break;
        }
    }

    private void SetGameOverState()
    {
        if (GameUIController.InstanceExists)
        {
            GameUIController.Instance.SetGameOver();
        }
    }

    private void StartWavesInternal()
    {
        WaveManager.WavesCompleted += OnWavesCompleted;
        WaveManager.StartNextWave();
    }

    private void OnWavesCompleted()
    {
        WaveManager.WavesCompleted -= OnWavesCompleted;
        ChangeGameplayState(GameplayState.WavesDepleted);
    }
}

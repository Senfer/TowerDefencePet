using Assets.Scripts;
using Assets.Scripts.Gameplay.Enemy;
using Assets.Scripts.Gameplay.Level;
using Assets.Scripts.Gameplay.States;
using System;
using UnityEngine;

public class GameplayManager : Singleton<GameplayManager>
{
    private GameplayState _currentState;
    private int _enemiesCount;

    public WaveManager WaveManager;
    public PlayerWalletManager WalletManager;
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

    public void EnemyDestroyedCallback(EnemyController enemy)
    {
        if (enemy.State == EnemyStates.Dead)
        {
            WalletManager.IncreaseCurrency(enemy.Reward);
        }
        if (enemy.State == EnemyStates.ReachedObjective)
        {
            DecreaseScore(enemy.Damage);
        }

        _enemiesCount--;

        if (_enemiesCount == 0 && CurrentState == GameplayState.WavesIncoming)
        {
            if (WaveManager.State == WaveManagerState.WaveIsOver)
            {
                ChangeGameplayState(GameplayState.Building);
            }

            if (WaveManager.State == WaveManagerState.WavesDepleted)
            {
                ChangeGameplayState(GameplayState.GameOver);
            }
        }
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
        WaveManager.CurrentWaveCompleted += OnCurrentWaveCompleted;
        WaveManager.EnemySpawned += OnEnemySpawned;
        WaveManager.StartNextWave();
    }

    private void OnCurrentWaveCompleted()
    {
        WaveManager.CurrentWaveCompleted -= OnCurrentWaveCompleted;
        WaveManager.EnemySpawned -= OnEnemySpawned;
    }

    private void OnWavesCompleted()
    {
        WaveManager.WavesCompleted -= OnWavesCompleted;
    }

    private void OnEnemySpawned()
    {
        _enemiesCount++;
    }

    private void IncreaseScore(int value)
    {
        if (_currentState == GameplayState.WavesIncoming || WaveManager.State != WaveManagerState.Undefined)
        {
            if (CurrentScore > 0)
            {
                CurrentScore += value;
                ScoreChanged(CurrentScore);
            }
        }
    }

    private void DecreaseScore(int value)
    {
        if (_currentState == GameplayState.WavesIncoming || WaveManager.State != WaveManagerState.Undefined)
        {
            if (CurrentScore > 0)
            {
                CurrentScore -= value;
                ScoreChanged(CurrentScore);
            }

            if (CurrentScore <= 0)
            {
                ChangeGameplayState(GameplayState.GameOver);
            }
        }
    }
}

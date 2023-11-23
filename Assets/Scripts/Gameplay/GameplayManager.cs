using Assets.Scripts;
using Assets.Scripts.Gameplay.States;
using System;

public class GameplayManager : Singleton<GameplayManager>
{
    private GameplayState _currentState;

    public WaveManager WaveManager;
    public int CurrentScore;
    public event Action<int> ScoreChanged;

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

    private void ChangeGameplayState(GameplayState state)
    {
        if (_currentState == state)
        {
            return;
        }

        _currentState = state;
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

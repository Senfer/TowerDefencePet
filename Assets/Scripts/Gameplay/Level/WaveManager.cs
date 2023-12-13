using Assets.Scripts.Gameplay.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private IEnumerator<Wave> _waveIterator;
    private Wave _currentWave => _waveIterator.Current;

    public List<Wave> ConfiguredEnemyWaves;
    public int CurrentWaveNumber;
    public WaveManagerState State;

    public event Action WavesCompleted;
    public event Action CurrentWaveCompleted;
    public event Action EnemySpawned;

    void Awake()
    {
        _waveIterator = ConfiguredEnemyWaves.GetEnumerator();
    }

    void Start()
    {
        if (ConfiguredEnemyWaves.Count > 0)
        {
            _waveIterator.MoveNext();
        }
    }

    public void StartNextWave()
    {
        if (ConfiguredEnemyWaves.Count == 0)
        {
            WavesCompleted();
            return;
        }

        State = WaveManagerState.WaveIncoming;

        _currentWave.StartWave();
        _currentWave.WaveFinished += OnWaveFinished;
        _currentWave.EnemySpawned += OnEnemySpawned;
        CurrentWaveNumber++;
    }

    private void OnWaveFinished()
    {
        State = WaveManagerState.WaveIsOver;
        _currentWave.WaveFinished -= OnWaveFinished;
        _currentWave.EnemySpawned -= OnEnemySpawned;
        CurrentWaveCompleted();
        if (!_waveIterator.MoveNext())
        {
            State = WaveManagerState.WavesDepleted;
            WavesCompleted();
        }
    }

    private void OnEnemySpawned()
    {
        EnemySpawned();
    }
}

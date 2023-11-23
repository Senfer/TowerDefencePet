using Assets.Scripts.Gameplay.Level;
using System;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    private IEnumerator<Wave> _waveIterator;
    private Wave _currentWave => _waveIterator.Current;

    public List<Wave> ConfiguredEnemyWaves;
    public event Action WavesCompleted;

    public void Awake()
    {
        _waveIterator = ConfiguredEnemyWaves.GetEnumerator();
    }

    public void StartNextWave()
    {
        if (_waveIterator.MoveNext())
        {
            _currentWave.StartWave();
            _currentWave.WaveFinished += OnWaveFinished;
            return;
        }

        WavesCompleted();
    }

    private void OnWaveFinished()
    {
        _currentWave.WaveFinished -= OnWaveFinished;
        StartNextWave();
    }
}

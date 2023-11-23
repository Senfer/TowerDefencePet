using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Level
{
    public class Wave : MonoBehaviour
    {
        private IEnumerator<SpawningConfig> _spawnConfigsIterator;
        private float _currentSpawnTimer;
        private bool _waveStarted;
        private bool _waveFinished;

        public List<SpawningConfig> SpawningConfigs;
        public SpawnerController WaveSpawner;

        public event Action WaveFinished;

        public void StartWave()
        {
            _spawnConfigsIterator = SpawningConfigs.GetEnumerator();
            if (_spawnConfigsIterator.MoveNext())
            {
                _waveStarted = true;
                _currentSpawnTimer = _spawnConfigsIterator.Current.SpawnDelay;
            }
        }

        public void Update()
        {
            if (_waveStarted && !_waveFinished)
            {
                _currentSpawnTimer -= Time.deltaTime;

                if (_currentSpawnTimer <= 0)
                {
                    SpawnCurrent();
                    if (_spawnConfigsIterator.MoveNext())
                    {
                        _currentSpawnTimer = _spawnConfigsIterator.Current.SpawnDelay;
                    }
                    else
                    {
                        _waveFinished = true;
                        WaveFinished();
                    }
                }
            }
        }

        private void SpawnCurrent()
        {
            WaveSpawner.SpawnEntity(_spawnConfigsIterator.Current.SpawningEntity);
        }
    }
}

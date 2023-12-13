using System;

namespace Assets.Scripts.Gameplay.Level
{
    [Serializable]
    public class SpawningConfig
    {
        public EnemyController SpawningEntity;
        public float SpawnDelay;
    }
}

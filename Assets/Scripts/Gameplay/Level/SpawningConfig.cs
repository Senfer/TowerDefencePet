using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Level
{
    [Serializable]
    public class SpawningConfig
    {
        public EnemyController SpawningEntity;
        public float SpawnDelay;
    }
}

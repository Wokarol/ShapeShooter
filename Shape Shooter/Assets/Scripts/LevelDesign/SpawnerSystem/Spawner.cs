using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

namespace Wokarol.SpawnSystem
{

    public class Spawner : MonoBehaviour
    {
        int currentEnemyCount = 0;
        public int CurrentEnemyCount => currentEnemyCount;

        Dictionary<SpawnableDefinition, Pool> enemyPools = new Dictionary<SpawnableDefinition, Pool>();

        public void SpawnWave(WavePattern pattern) {
            foreach (var spawnPoint in pattern.SpawnPoints) {
                Spawn(spawnPoint.Spawnable, spawnPoint.Point);
            }
        }

        public void Spawn(SpawnableDefinition spawnable, Vector2 point) {
            var ob = GetPool(spawnable).Get(transform.TransformPoint(point), Quaternion.identity);
            currentEnemyCount++;
        }

        private Pool GetPool(SpawnableDefinition spawnable) {
            if (!enemyPools.ContainsKey(spawnable)) {
                var pool = gameObject.AddComponent<Pool>();
                pool.Setup(spawnable.SpawnableObject);
                enemyPools.Add(spawnable, pool);
                pool.OnObjectDestroyed += () => currentEnemyCount--;
                return pool;
            }
            return enemyPools[spawnable];
        }
    }
}

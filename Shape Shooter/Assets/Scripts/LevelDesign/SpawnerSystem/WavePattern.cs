using System;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.SpawnSystem;

namespace Wokarol
{
    [CreateAssetMenu]
    public class WavePattern : ScriptableObject
    {
        [SerializeField] List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
        public List<SpawnPoint> SpawnPoints => spawnPoints;

        [System.Serializable]
        public class SpawnPoint
        {
            public SpawnPoint(Vector2 point, SpawnableDefinition spawnable) {
                this.point = point;
                this.spawnable = spawnable;
            }

            [SerializeField] Vector2 point = Vector2.zero;
            [SerializeField] SpawnableDefinition spawnable = null;

            public Vector2 Point => point;
            public SpawnableDefinition Spawnable => spawnable;
        }

        public void Validate() {
            RemoveNulls();
        }

        private void RemoveNulls() {
            for (int i = spawnPoints.Count - 1; i >= 0; i--) {
                if (spawnPoints[i].Spawnable == null)
                    spawnPoints.RemoveAt(i);
            }
        }
    }

}

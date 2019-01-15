using UnityEngine;

namespace Wokarol.SpawnSystem
{
    [CreateAssetMenu]
    public class WavePattern : ScriptableObject
    {
        [SerializeField] SpawnPoint[] spawnPoints;
        public SpawnPoint[] SpawnPoints => spawnPoints;

        [System.Serializable]
        public class SpawnPoint
        {
            [SerializeField] Vector2 point;
            [SerializeField] SpawnableDefinition spawnable;

            public Vector2 Point => point;
            public SpawnableDefinition Spawnable => spawnable;
        }
    }

}

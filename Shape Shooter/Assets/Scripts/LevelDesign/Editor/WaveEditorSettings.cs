using UnityEngine;

namespace Wokarol.SpawnSystem.Editors
{
    internal class WaveEditorSettings : ScriptableObject
    {
        [SerializeField] SpawnableDefinition[] spawnables;

        [field: SerializeField, HideInInspector]
        public WavePattern CurrentWave { get; set; }
        public SpawnableDefinition[] Spawnables => spawnables;

        public static WaveEditorSettings Init() {
            var settings = (WaveEditorSettings)CreateInstance(typeof(WaveEditorSettings));
            settings.spawnables = new SpawnableDefinition[9];
            return settings;
        }

    }
}
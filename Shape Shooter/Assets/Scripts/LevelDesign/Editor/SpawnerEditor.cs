using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Wokarol.SpawnSystem.Editors
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        Color innerColorCache = new Color(0.98f, 0.7f, 0.43f, 0.5f);
        Color outerColorCache = new Color(0.98f, 0.7f, 0.43f, 1);

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

        }

        private void OnSceneGUI() {
            Spawner spawner = target as Spawner;

            if (spawner.WavePattern != null)
                foreach (var spawn in spawner.WavePattern.SpawnPoints) {
                    Handles.color = innerColorCache;
                    Handles.DrawSolidDisc(spawner.transform.TransformPoint(spawn.Point), Vector3.back, 0.65f);
                    Handles.color = outerColorCache;
                    Handles.DrawWireDisc(spawner.transform.TransformPoint(spawn.Point), Vector3.back, 0.65f);
                }
        }
    } 
}

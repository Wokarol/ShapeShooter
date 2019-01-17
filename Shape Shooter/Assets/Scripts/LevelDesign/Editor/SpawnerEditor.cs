using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Wokarol.SpawnSystem.Editors
{
    [CustomEditor(typeof(Spawner))]
    public class SpawnerEditor : Editor
    {
        [SerializeField] Transform transform;

        [SerializeField] WaveEditorHandler waveEditorHandler;

        private void OnEnable() {
            transform = (target as Spawner).transform;
            waveEditorHandler = new WaveEditorHandler(transform, this);
        }

        private void OnSceneGUI() {
            waveEditorHandler.OnSceneGUI();
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            waveEditorHandler.OnInspectorGUI();
        }

        private void DrawShape(Transform target, WavePattern.SpawnPoint spawn) {
            Color color = spawn.Spawnable.Color;

            switch (spawn.Spawnable.Shape) {
                case SpawnableDefinition.Shapes.Circle:
                    color.a = 0.5f;
                    Handles.color = color;
                    Handles.DrawSolidDisc(target.TransformPoint(spawn.Point), Vector3.back, 0.65f);

                    color.a = 1f;
                    Handles.color = color;
                    Handles.DrawWireDisc(target.TransformPoint(spawn.Point), Vector3.back, 0.65f);
                    break;

                case SpawnableDefinition.Shapes.Square:
                    Vector2 size = new Vector2(1f, 1f);
                    Color face = color;
                    face.a = 0.5f;
                    Color outline = color;
                    outline.a = 1f;
                    Handles.DrawSolidRectangleWithOutline(new Rect((Vector2)target.TransformPoint(spawn.Point) - (size * 0.5f), size), face, outline);
                    break;

                default:
                    break;
            }
        }
    } 
}

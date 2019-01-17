using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Wokarol.SpawnSystem.Editors
{
    [System.Serializable]
    internal class WaveEditorHandler
    {
        Transform transform;
        int enemyIndex = -1;
        WaveEditorSettings settings;

        Editor editor;

        GUIContent labelContent = new GUIContent("<b>Editor</b>", "Tools used to edit waves");
        GUIStyle style = new GUIStyle();

        public WaveEditorHandler(Transform transform, Editor editor) {
            this.transform = transform;
            this.editor = editor;
            var allSettings = AssetDatabase.FindAssets($"t:{nameof(WaveEditorSettings)}");
            if (allSettings.Length > 0 && allSettings[0] != null) {
                settings = AssetDatabase.LoadAssetAtPath<WaveEditorSettings>(AssetDatabase.GUIDToAssetPath(allSettings[0]));
            } else {
                settings = WaveEditorSettings.Init();
                AssetDatabase.CreateAsset(settings, "Assets/Settings/WaveEditorSettings.asset");
                Debug.Log(AssetDatabase.GetAssetPath(settings));
            }

            style.richText = true;
            settings.CurrentWave?.Validate();
        }

        public void OnSceneGUI() {
            if (settings == null) return;
            if (settings.CurrentWave != null && transform != null) {
                Event guiEvent = Event.current;

                foreach (var spawn in settings.CurrentWave.SpawnPoints) {
                    DrawShape(transform, spawn);
                }

                if (guiEvent.type == EventType.KeyDown) {
                    if ((int)guiEvent.keyCode >= 257 && (int)guiEvent.keyCode <= 265) {
                        int index = (int)guiEvent.keyCode - 257;
                        enemyIndex = index;
                        editor.Repaint();
                    }
                }

                if (guiEvent.type == EventType.MouseDown && guiEvent.modifiers == EventModifiers.None && guiEvent.button == 0 && enemyIndex != -1 && settings.Spawnables[enemyIndex] != null) {
                    // Add new enemy
                    Vector2 pos = transform.InverseTransformPoint(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);
                    settings.CurrentWave.SpawnPoints.Add(new WavePattern.SpawnPoint(pos, settings.Spawnables[enemyIndex]));
                    SceneView.RepaintAll();
                }

                if (guiEvent.type == EventType.MouseDown && guiEvent.modifiers == EventModifiers.Control && guiEvent.button == 0 && settings.CurrentWave.SpawnPoints.Count > 0) {
                    // Add new enemy
                    Vector2 pos = transform.InverseTransformPoint(HandleUtility.GUIPointToWorldRay(Event.current.mousePosition).origin);
                    var target = settings.CurrentWave.SpawnPoints.OrderBy(o => Mathf.Abs((o.Point - pos).sqrMagnitude)).ToArray()[0];
                    settings.CurrentWave.SpawnPoints.Remove(target);
                    SceneView.RepaintAll();
                }

                if (guiEvent.type == EventType.Layout) {
                    HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
                }
            }
        }

        public void OnInspectorGUI() {
            if (settings == null) return;
            EditorGUILayout.Space();
            int indent = EditorGUI.indentLevel;

            EditorGUI.indentLevel = 0;

            EditorGUILayout.LabelField(labelContent, style);

            EditorGUI.indentLevel = 1;

            EditorGUI.BeginChangeCheck();
            settings.CurrentWave = (WavePattern)EditorGUILayout.ObjectField("Wave", settings.CurrentWave, typeof(WavePattern), false);
            if (enemyIndex != -1)
                EditorGUILayout.HelpBox($"Selected: {enemyIndex + 1}: {(settings.Spawnables[enemyIndex] != null ? settings.Spawnables[enemyIndex].DebugName : "Null")}", MessageType.Info);
            else
                EditorGUILayout.HelpBox($"No spawnable selected", MessageType.Info);

            EditorGUI.indentLevel = indent;

            if (EditorGUI.EndChangeCheck()) {
                SceneView.RepaintAll();
                settings.CurrentWave?.Validate();
            }

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
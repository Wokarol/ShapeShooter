using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Wokarol
{
	public class PersistentSceneEditorSettingsWindow : EditorWindow
	{
		[MenuItem("Window/Persistent Scene Settings")]
		static void Init ()
		{
			var window = (PersistentSceneEditorSettingsWindow)GetWindow(typeof(PersistentSceneEditorSettingsWindow));
			window.titleContent = new GUIContent("PS Settings");
			window.minSize = new Vector2(100, 25);
			window.Show();
		}

		private void OnGUI ()
		{
			var key = $"Scene_{AssetDatabase.FindAssets(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name)}_NoPersistentScene";
			bool enabled = !EditorPrefs.HasKey(key);

			EditorGUILayout.BeginHorizontal();

			//EditorGUILayout.HelpBox($"{(enabled ? "[X]" : "[  ]")}", MessageType.None);
			EditorGUI.BeginDisabledGroup(true);
			EditorGUILayout.Toggle(enabled);
			EditorGUI.EndDisabledGroup();

			EditorGUI.BeginDisabledGroup(!enabled);
			if (GUILayout.Button("Disable Persistent Scene")) {
				EditorPrefs.SetBool(key, true);
			}
			EditorGUI.EndDisabledGroup();
			EditorGUI.BeginDisabledGroup(enabled);
			if (GUILayout.Button("Enable Persistent Scene")) {
				EditorPrefs.DeleteKey(key);
			}
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.EndHorizontal();
		}
	}
}


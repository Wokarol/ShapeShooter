using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Wokarol.SerializationSystem.Editors
{
    [CustomEditor(typeof(SaveData))]
    public class SaveDataEditor : Editor
    {
        SaveData saveData;
        string search;

        private void OnEnable() {
            saveData = target as SaveData;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Load")) {
                saveData.Load();
            }
            if (GUILayout.Button("Save")) {
                saveData.Save();
            }
            GUILayout.EndHorizontal();
            if (GUILayout.Button("Remove")) {
                saveData.Delete();
            }
            GUILayout.Space(5);
            search = EditorGUILayout.TextField("Search: ", search);
            if (search == null) search = "";
            List<string> entries = new List<string>();
            foreach (KeyValuePair<string, object> entry in saveData.AllEntries) {
                if (entry.Key.Contains(search))
                    entries.Add($"{entry.Key} => {entry.Value.ToString()}");
            }
            string[] sortedEntries = entries.OrderBy(x => x).ToArray();
            EditorGUILayout.HelpBox($"Entries currently loaded:\n•{string.Join("\n•", sortedEntries)}", MessageType.Info);
        }
    }
}

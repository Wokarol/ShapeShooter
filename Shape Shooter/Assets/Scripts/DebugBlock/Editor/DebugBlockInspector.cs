using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Reflection;

public class DebugBlockInspector : EditorWindow
{
    [SerializeField] MonoBehaviour target;

    [MenuItem("Tools/Debug Block Inspector")]
    static void Init() {
        DebugBlockInspector inspector = GetWindow<DebugBlockInspector>();
        inspector.Show();
        
    }

    private void Update() {
        Repaint();
    }

    private void OnGUI() {
        string testText = "====";
        target = (MonoBehaviour)EditorGUILayout.ObjectField("Target", target, typeof(MonoBehaviour), true);
        testText += target.GetType().FullName;
        
        var properties = target.GetType().GetProperties();
        foreach (var property in properties) {
            if(property.PropertyType == typeof(DebugBlock)) {
                DrawDebugBlock((DebugBlock)property.GetValue(target));
            }
        }

        EditorGUILayout.HelpBox(testText, MessageType.None);
    }

                //testText += ((DebugBlock)property.GetValue(target)).OverrideName;
    private void DrawDebugBlock(DebugBlock block) {
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DebugBlock))]
public class DebugBlockDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        string info = $"{label.text}\n-------------------------";
        var data = property.FindPropertyRelative("data");
        for (int i = 0; i < data.arraySize; i++) {
            var dataI = data.GetArrayElementAtIndex(i);
            info += $"\n{dataI.FindPropertyRelative("Name").stringValue}:\t{dataI.FindPropertyRelative("Value").stringValue}";
        }
        EditorGUI.HelpBox(position, info, MessageType.None);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        int rowsCount = 2;
        rowsCount += property.FindPropertyRelative("data").arraySize;
        return 11 * rowsCount + 6;
    }
}

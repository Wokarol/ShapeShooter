using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomPropertyDrawer(typeof(DebugBlock))]
//[CustomEditor(typeof(DebugBlock))]
public class DebugBlockDrawer : PropertyDrawer
{
    //public override void OnInspectorGUI() {
    //    DebugBlock block = fieldInfo.GetValue(property.serializedObject.targetObject) as DebugBlock;
    //    string info = $"{(block.OverrideName.Length == 0 ? label.text : block.OverrideName)}\n-------------------------";

    //    var dataValues = block.Data.Values;
    //    foreach (DataObject data in dataValues) {
    //        info += $"\n{data.Name}:\t{data.Value}";
    //    }

    //    EditorGUI.HelpBox(position, info, MessageType.Info);
    //}

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        DebugBlock block = fieldInfo.GetValue(property.serializedObject.targetObject) as DebugBlock;
        string info = $"{(block.OverrideName.Length == 0 ? label.text : block.OverrideName)}\n-------------------------";

        var dataValues = block.Data.Values;
        foreach (DataObject data in dataValues) {
            info += $"\n{data.Name}:\t{data.Value}";
        }

        EditorGUI.HelpBox(position, info, MessageType.Info);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        int rowsCount = 2;
        DebugBlock block = fieldInfo.GetValue(property.serializedObject.targetObject) as DebugBlock;
        rowsCount += block.Data.Count;
        return 11 * rowsCount + 6;
    }
}

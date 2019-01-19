using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Wokarol;

[CustomPropertyDrawer(typeof(MinMaxFloat))]
public class MinMaxFloatDrawer : PropertyDrawer
{

    readonly GUIStyle style = new GUIStyle {
        alignment = TextAnchor.MiddleCenter
    };

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        //base.OnGUI(position, property, label);
        EditorGUI.BeginProperty(position, label, property);
        EditorGUI.BeginChangeCheck();
        position = EditorGUI.PrefixLabel(position, label);

        var minProperty = property.FindPropertyRelative("min");
        var maxProperty = property.FindPropertyRelative("max");

        float centreTextSize = 40;

        var minRect = new Rect(new Vector2(position.position.x, position.position.y), new Vector2((position.size.x - centreTextSize) * 0.5f, position.size.y));
        var maxRect = new Rect(new Vector2(position.position.x + (position.size.x - centreTextSize) * 0.5f + centreTextSize, position.position.y), new Vector2((position.size.x - centreTextSize) * 0.5f, position.size.y));
        var centreTextRect = new Rect(new Vector2(position.position.x + (position.size.x - centreTextSize) * 0.5f, position.position.y), new Vector2(centreTextSize, position.size.y));

        EditorGUI.LabelField(centreTextRect, "\u2264 X \u2264", style);
        minProperty.floatValue = EditorGUI.FloatField(minRect, minProperty.floatValue);
        maxProperty.floatValue = EditorGUI.FloatField(maxRect, maxProperty.floatValue);

        if (EditorGUI.EndChangeCheck()) {
            if(minProperty.floatValue > maxProperty.floatValue) {
                float temp = minProperty.floatValue;
                minProperty.floatValue = maxProperty.floatValue;
                maxProperty.floatValue = temp;
            }
        }

        EditorGUI.EndProperty();
    }
}

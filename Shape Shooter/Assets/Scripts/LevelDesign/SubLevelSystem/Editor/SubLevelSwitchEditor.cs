using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Wokarol.SubLevelSystem.Editors
{
    [CustomEditor(typeof(SubLevelSwitch))]
    public class SubLevelSwitchEditor : Editor
    {
        SubLevelSwitch levelSwitch;
        int menuPosition = -1;

        private void OnEnable() {
            levelSwitch = target as SubLevelSwitch;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUI.indentLevel = 0;

            List<SubLevelID> accesibleLevel = new List<SubLevelID>();
            List<string> accesibleLevelNames = new List<string>();
            foreach (var level in levelSwitch.Levels) {
                if(level.ID != null) {
                    accesibleLevel.Add(level.ID);
                    accesibleLevelNames.Add(level.ID.name);
                }
            }

            EditorGUI.BeginChangeCheck();
            menuPosition = EditorGUILayout.Popup(menuPosition, accesibleLevelNames.ToArray());
            if (EditorGUI.EndChangeCheck()) {
                levelSwitch.SetAllLevelsState(false);
                levelSwitch.ChangeLevel(accesibleLevel[menuPosition]);
            }
        }
    } 
}

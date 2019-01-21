using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Wokarol.MenuSystem.Editors
{
    [CustomEditor(typeof(MenuController))]
    public class MenuControllerInspector : Editor
    {
        MenuController menuController;
        int menuPosition = -1;

        private void OnEnable() {
            menuController = target as MenuController;
        }

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            EditorGUI.indentLevel = 0;

            List<string> accesibleScenes = new List<string>();
            foreach (var scene in menuController.Scenes) {
                accesibleScenes.Add(scene.Name);
            }

            EditorGUI.BeginChangeCheck();
            menuPosition = EditorGUILayout.Popup(menuPosition, accesibleScenes.ToArray());
            if (EditorGUI.EndChangeCheck()) {
                menuController.ClearChangeMenuScene(accesibleScenes[menuPosition]);
            }
        }
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Wokarol.MenuSystem
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] MenuScene[] scenes = new MenuScene[0];
        Dictionary<string, MenuScene> scenesDictionary;

        MenuScene currentScene;
        Stack<MenuScene> previousScenes = new Stack<MenuScene>();

        public MenuScene[] Scenes => scenes;
        public bool HasPreviousScenes => previousScenes.Count > 0;

        void RemapScenesDictionary() {
            scenesDictionary = new Dictionary<string, MenuScene>();
            foreach (var scene in scenes) {
                if (!scenesDictionary.ContainsKey(scene.Name)) {
                    scenesDictionary.Add(scene.Name, scene);
                } else {
                    Debug.LogWarning($"Multiple menu scenes named {scene.Name}");
                }
            }
        }
        private void OnValidate() {
            RemapScenesDictionary();
        }

        private void Start() {
            if (scenes.Length == 0) return;
            RemapScenesDictionary();
            foreach (var scene in scenes) {
                foreach (var ob in scene.SceneElements) {
                    ob.SetActive(false);
                }
            }
            ChangeMenuScene(scenes[0], false);
        }

        internal void ChangeToPreviousMenuScene() {
            if (HasPreviousScenes) {
                ChangeMenuScene(previousScenes.Pop(), false);
            }
        }

        public void ChangeMenuScene(string sceneName) {
            if (scenesDictionary == null) RemapScenesDictionary();
            if (!scenesDictionary.ContainsKey(sceneName)) {
                Debug.LogWarning($"There's no menu scene named {sceneName}");
                return;
            }
            ChangeMenuScene(scenesDictionary[sceneName], true);
        }

        public void ClearChangeMenuScene(string sceneName) {
            foreach (var scene in scenes) {
                foreach (var ob in scene.SceneElements) {
                    ob.SetActive(false);
                }
            }
            ChangeMenuScene(scenesDictionary[sceneName], true);
        }

        void ChangeMenuScene(MenuScene scene, bool pushToHistory) {
            if(currentScene != null) {
                foreach (var ob in currentScene.SceneElements) {
                    ob.SetActive(false);
                }
            }
            foreach (var ob in scene.SceneElements) {
                ob.SetActive(true);
            }
            if(pushToHistory && Application.isPlaying)
                previousScenes.Push(currentScene);
            currentScene = scene;

            EventSystem.current.SetSelectedGameObject(currentScene.DefaultSelected);
        }

    }

    [System.Serializable]
    public class MenuScene
    {
        [SerializeField] string name = "";
        [SerializeField] GameObject defaultSelected;
        [SerializeField] GameObject[] sceneElements = new GameObject[0];

        public string Name => name;
        public GameObject DefaultSelected => defaultSelected;
        public GameObject[] SceneElements => sceneElements;
    }
}

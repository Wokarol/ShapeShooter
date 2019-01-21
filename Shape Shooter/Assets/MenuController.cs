using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField] MenuScene[] scenes;
        Dictionary<string, MenuScene> scenesDictionary;
        void RemapScenesDictionary() {
            Debug.Log("Remapping");
            scenesDictionary = new Dictionary<string, MenuScene>();
            foreach (var scene in scenes) {
                if (!scenesDictionary.ContainsKey(scene.Name)) {
                    scenesDictionary.Add(scene.Name, scene);
                } else {
                    Debug.LogWarning($"Multiple menu scenes named {scene.Name}");
                }
            }
        }

        MenuScene currentScene;


        private void Start() {
            if (scenes.Length == 0) return;
            RemapScenesDictionary();
            ChangeMenuScene(scenes[0]);
        }

        public void ChangeMenuScene(string sceneName) {
            if (!scenesDictionary.ContainsKey(sceneName)) {
                Debug.LogWarning($"There's no menu scene named {sceneName}");
                return;
            }
            ChangeMenuScene(scenesDictionary[sceneName]);
        }

        void ChangeMenuScene(MenuScene scene) {
            if(currentScene != null) {
                foreach (var ob in currentScene.SceneElements) {
                    ob.SetActive(false);
                }
            }
            foreach (var ob in scene.SceneElements) {
                ob.SetActive(true);
            }
            currentScene = scene;
        }
    }

    [System.Serializable]
    public class MenuScene
    {
        [SerializeField] string name;
        [SerializeField] GameObject[] sceneElements;

        public string Name => name;
        public GameObject[] SceneElements => sceneElements;
    }
}

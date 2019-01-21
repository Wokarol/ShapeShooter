using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.SubLevelSystem
{
    public class SubLevelSwitch : MonoBehaviour
    {
        [System.Serializable] public class SubLevel
        {
            public SubLevelID ID;
            public GameObject[] SubLevelElements = new GameObject[0];
        }

        [SerializeField] SubLevel[] _levels = new SubLevel[0];

        Dictionary<SubLevelID, SubLevel> _levelsDictionary;
        SubLevel currentLevel;

        public SubLevel[] Levels => _levels;
        
        void RemapLevelsDictionary() {
            _levelsDictionary = new Dictionary<SubLevelID, SubLevel>();
            foreach (var level in _levels) {
                if(level.ID == null) {
                    if (Application.isPlaying) Debug.LogError("Null ID", gameObject);
                    continue;
                }
                if (!_levelsDictionary.ContainsKey(level.ID)) {
                    _levelsDictionary.Add(level.ID, level);
                } else {
                    Debug.LogWarning($"Multiple sub-levels with {level.ID.name} ID");
                }
            }
        }
        private void OnValidate() {
            RemapLevelsDictionary();
        }

        private void Start() {
            if (Levels.Length == 0) return;
            RemapLevelsDictionary();
            DisableAllSubLevels();
            ChangeLevel(_levels[0]);
        }

        public void ChangeLevel(SubLevelID id) {
            if (_levelsDictionary == null) RemapLevelsDictionary();
            if (!_levelsDictionary.ContainsKey(id)) {
                Debug.LogWarning($"There's no {id.name} sublevel", gameObject);
            }
            ChangeLevel(_levelsDictionary[id]);
        }

        void ChangeLevel(SubLevel level) {
            if (currentLevel != null)
                SetLevelState(currentLevel, false);
            SetLevelState(level, true);
            currentLevel = level;
        }

        private void DisableAllSubLevels() {
            foreach (var level in _levels) {
                SetLevelState(level, false);
            }
        }
        private static void SetLevelState(SubLevel level, bool state) {
            foreach (var ob in level.SubLevelElements) {
                ob.SetActive(state);
            }
        }
    } 
}

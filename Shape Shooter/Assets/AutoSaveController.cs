using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.SerializationSystem
{
    public class AutoSaveController : MonoBehaviour
    {
        [SerializeField] SaveData save = null;
        [SerializeField] bool AutoLoadOnStart = true;
        [SerializeField] bool AutoSaveOnExit = true;

        private void Awake() {
            if (!AutoLoadOnStart) return;
            save.Load();
        }

        private void OnDestroy() {
            if (!AutoSaveOnExit) return;
            save.Save();
        }
    } 
}

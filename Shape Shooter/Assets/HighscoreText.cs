using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.SerializationSystem;

namespace Wokarol
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class HighscoreText : MonoBehaviour
    {
        [SerializeField] SaveData saveData = null;
        [SerializeField] string key = "Highscore";
        [TextArea] [SerializeField] string noKeyText = "";
        [TextArea] [SerializeField] string format = "{0}";
        [SerializeField] string numberFormat = "F0";
        TMPro.TMP_Text text;

        private void Start() {
            text = GetComponent<TMPro.TMP_Text>();
        }

        private void Update() {
            if (saveData.ContainsEntry(key)) {
                text.text = string.Format(format, saveData.GetEntry<int>(key, 0).ToString(numberFormat));
            } else {
                text.text = noKeyText;
            }
        }
    } 
}

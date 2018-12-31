using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Wokarol
{
    public class VersionDataTextController : MonoBehaviour
    {
        private const string versionTag = "{V}";
        [SerializeField] TMP_Text text;
        [SerializeField] string format = $"v. {versionTag}";

        private void Start()
        {
            UpdateText();
        }

        private void OnValidate()
        {
            if (text == null) {
                text = GetComponent<TMP_Text>();
            }

            if(text != null) {
                UpdateText();
            }
        }

        private void UpdateText()
        {
            text.text = format.Replace(versionTag, Application.version);
        }
    }
}

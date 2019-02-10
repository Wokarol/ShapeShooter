using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    [RequireComponent(typeof(TMPro.TMP_Text))]
    public class FloatToText : MonoBehaviour
    {
        [SerializeField] FloatVariableReference value;
        [SerializeField] string format = "{0}";
        [SerializeField] string numberFormat = "F0";
        TMPro.TMP_Text text;

        private void Start() {
            text = GetComponent<TMPro.TMP_Text>();
        }

        private void Update() {
            text.text = string.Format(format, value.Value.ToString(numberFormat));
        }
    } 
}

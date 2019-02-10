using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Wokarol.SerializationSystem;

public class ButtonLockedUntilSaveData : MonoBehaviour
{
    [SerializeField] string key = "";
    [SerializeField] SaveData saveData;
    [SerializeField] Selectable selectable;
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] Color disabledTextColor;

    Color enabledTextColor;

    private void Start() {
        enabledTextColor = text.color;
    }

    private void Update() {
        if (!saveData.ContainsEntry(key)) {
            text.color = disabledTextColor;
            selectable.interactable = false;
        } else {
            text.color = enabledTextColor;
            selectable.interactable = true;
        }
    }
}

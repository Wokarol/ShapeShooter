using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    public class BackToMainMenu : MonoBehaviour
    {
        [SerializeField] StringVariableReference menuSceneName = new StringVariableReference();
        [SerializeField] KeyCode backKey = KeyCode.Escape;
        [SerializeField] string button = "Menu";

        private void Update() {
            if (Input.GetKeyDown(backKey) || Input.GetButtonDown(button)) {
                ScenesController.Instance.ChangeScene(menuSceneName.Value);
            }
        }
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.MenuSystem
{
    public class MenuCommands : MonoBehaviour
    {
        [SerializeField] MenuController controller = null;
        [SerializeField] MenuTransitionController transitionController = null;
        [SerializeField] KeyCode backKey = KeyCode.Escape;

        private void Update() {
            if (Input.GetKeyDown(backKey)) {
                GoToPreviousMenuScene();
            }
        }
        public void Quit() {
            Application.Quit();
        }
        public void ChangeMenuScene(string name) {
            if (!transitionController.InMotion)
                transitionController.CallTransition(() => controller.ChangeMenuScene(name));
        }
        public void GoToPreviousMenuScene() {
            if (controller.HasPreviousScenes && !transitionController.InMotion)
                transitionController.CallTransition(() => controller.ChangeToPreviousMenuScene());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.MenuSystem
{
    public class MenuCommands : MonoBehaviour
    {
        [SerializeField] MenuController controller = null;
        [SerializeField] MenuTransitionController transitionController = null;

        public void ChangeMenuScene(string name) {
            transitionController.CallTransition(() => controller.ChangeMenuScene(name));
        }
        public void GoToPreviousMenuScene(string name) {

        }
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    public class SceneChanger : MonoBehaviour
    {
        [SerializeField] StringVariableReference sceneName = new StringVariableReference();
        public void ChanageScene() {
            ScenesController.Instance.ChangeScene(sceneName.Value);
        }
    } 
}

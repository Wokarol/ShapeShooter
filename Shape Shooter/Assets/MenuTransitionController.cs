using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.MenuSystem
{
    [RequireComponent(typeof(Animator))]
    public class MenuTransitionController : MonoBehaviour
    {
        Animator _animator;
        Action _callback;
        private void Start() {
            _animator = GetComponent<Animator>();
        }

        public void CallTransition(Action callback) {
            _animator.SetTrigger("Transition");
            _callback = callback;
        }

        public void TriggerMenuSceneChange() {
            Debug.Log("Called callback");
            _callback?.Invoke();
        }
    }
}

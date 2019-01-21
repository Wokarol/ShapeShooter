using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.MenuSystem
{
    [RequireComponent(typeof(Animator))]
    public class CoverUncoverMenuTransitionController : MenuTransitionController
    {
        private readonly int TriggerHash = Animator.StringToHash("Transition");

        Animator _animator;
        Action _callback;

        private void Start() {
            _animator = GetComponent<Animator>();
        }

        public override void CallTransition(Action callback) {
            InMotion = true;
            _animator.SetTrigger(TriggerHash);
            _callback = callback;
        }

        public void TriggerMenuSceneChange() {
            _callback?.Invoke();
        }

        public void TranitionEnded() {
            InMotion = false;
        }
    }
}

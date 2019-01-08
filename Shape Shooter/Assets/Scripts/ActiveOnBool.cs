using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    [ExecuteInEditMode]
    public class ActiveOnBool : MonoBehaviour
    {
        [SerializeField] BoolVariableReference state = null;
        [SerializeField] bool inversed = false;
        [SerializeField] GameObject target = null;

        void Update() {
            if (state == null || target == null) return;
            bool targetState = inversed != state.Value;
            if (target.activeSelf != targetState)
                target.SetActive(targetState);
        }
    } 
}

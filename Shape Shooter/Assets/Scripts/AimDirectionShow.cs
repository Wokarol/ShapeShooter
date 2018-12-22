using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    public class AimDirectionShow : MonoBehaviour
    {
        [SerializeField] InputSystem.InputData input = null;

        private void Update() {
            transform.up = input.AimDirection;
        }
    } 
}

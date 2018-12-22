using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.InputSystem
{
    public class PlayerInput : InputData
    {
        private void Update() {
            Vector2 v = Movement;
            v.x = Input.GetAxis("Horizontal");
            v.y = Input.GetAxis("Vertical");
            Movement = v;
            if (Movement.sqrMagnitude > 1)
                Movement.Normalize();
        }
    } 
}

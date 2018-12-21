using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.InputSystem
{
    public class PlayerInput : InputData
    {
        private void Update() {
            Vector2 v = Movement;
            v.x = Input.GetAxisRaw("Horizontal");
            v.y = Input.GetAxisRaw("Vertical");
            Movement = v.normalized;
        }
    } 
}

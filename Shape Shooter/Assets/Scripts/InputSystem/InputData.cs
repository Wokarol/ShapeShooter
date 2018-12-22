﻿using UnityEngine;

namespace Wokarol.InputSystem
{
    public abstract class InputData : MonoBehaviour
    {
        public Vector2 Movement { get; protected set; }
        public Vector2 AimDirection { get; protected set; }
    }
}
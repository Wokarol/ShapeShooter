﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.InputSystem
{
    public class PlayerInput : InputData
    {
        [SerializeField] Camera mainCamera;

        bool usingPad = false;
        Vector3 oldMousePos = Vector3.zero;
        float sqrMinMouseDelta = 100;

        private void Update() {
            Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Movement.sqrMagnitude > 1)
                Movement.Normalize();

            CheckForInputChange();

            if (usingPad) {
                if (JoystickMoved()) {
                    AimDirection = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
                }
            } else {
                AimDirection = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
            }
        }
        private void CheckForInputChange() {
            if (usingPad && (oldMousePos - Input.mousePosition).sqrMagnitude > sqrMinMouseDelta) {
                usingPad = false;
                Debug.Log("Using mouse");
            }
            if (!usingPad && JoystickMoved()) {
                usingPad = true;
                oldMousePos = Input.mousePosition;
                Debug.Log("Using pad");
            }
        }

        private static bool JoystickMoved() {
            return (Mathf.Abs(Input.GetAxisRaw("Vertical2")) > 0.3f || Mathf.Abs(Input.GetAxisRaw("Horizontal2")) > 0.3f);
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.InputSystem
{
    public class PlayerInput : InputData
    {
        [SerializeField] Camera mainCamera = null;
        [Header("Auto Aim")]
        [SerializeField] bool useAutoAimForPad = true;
        [SerializeField] bool useAutoAimForMouse = false;
        [SerializeField] LayerMask autoAimLayer = 0;
        [Range(0.7f, 1f)][SerializeField] float minDotProduct = 0.93f;
        [Header("Output Data")]
        [SerializeField] BoolVariableReference UsingPad = new BoolVariableReference();

        bool usingPad = false;
        Vector3 oldMousePos = Vector3.zero;
        float sqrMinMouseDelta = 100;

        private void Update() {
            Movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if (Movement.sqrMagnitude > 1)
                Movement = Movement.normalized;

            CheckForInputChange();

            if (usingPad) {
                if (JoystickMoved()) {
                    RealAimDirection = new Vector2(Input.GetAxisRaw("Horizontal2"), Input.GetAxisRaw("Vertical2"));
                } else if(Movement.sqrMagnitude > 0.04) {
                    RealAimDirection = Movement;
                }
            } else {
                RealAimDirection = ((Vector2)(mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;                
            }

            AimDirection = RealAimDirection;
            if (usingPad && useAutoAimForPad) CalculateAimAssist();
            if (!usingPad && useAutoAimForMouse) CalculateAimAssist();

            Debug.DrawRay(transform.position, RealAimDirection * 10f, Color.white);
            Debug.DrawRay(transform.position, AimDirection * 5f, Color.green);

            Shoot = Input.GetMouseButton(0) || Input.GetButton("Shoot");

            UsingPad.Value = usingPad;
        }

        private void CalculateAimAssist() {

            var hits = Physics2D.CircleCastAll(transform.position, 3f, RealAimDirection, 50, autoAimLayer);
            Transform aimTarget = GetClosestToAim(hits);
            if (aimTarget) {
                AimDirection = ((Vector2)(aimTarget.position - transform.position)).normalized;
            }

        }

        private Transform GetClosestToAim(RaycastHit2D[] hits) {
            float biggerDotProduct = -1;
            Transform closest = null;
            foreach (var hit in hits) {
                Transform hitTransform = hit.transform;
                Debug.DrawLine(transform.position, hitTransform.position, Color.blue);
                float dotProduct = Vector2.Dot(((Vector2)(hitTransform.position - transform.position)).normalized, AimDirection);
                if(dotProduct > biggerDotProduct) {
                    closest = hitTransform;
                    biggerDotProduct = dotProduct;
                }
            }

            if (biggerDotProduct < minDotProduct)
                return null;
            else
                return closest;
        }

        private void CheckForInputChange() {
            if (usingPad && (oldMousePos - Input.mousePosition).sqrMagnitude > sqrMinMouseDelta) {
                usingPad = false;
                Cursor.visible = true;
            }
            if (!usingPad && JoystickMoved()) {
                usingPad = true;
                oldMousePos = Input.mousePosition;
                Cursor.visible = false;
            }
        }

        private void OnDisable() {
            Movement = Vector2.zero;
            RealAimDirection = Vector2.right;
            AimDirection = RealAimDirection;
            Shoot = false;
        }

        private static bool JoystickMoved() {
            return (Mathf.Abs(Input.GetAxisRaw("Vertical2")) > 0.3f || Mathf.Abs(Input.GetAxisRaw("Horizontal2")) > 0.3f);
        }
    }
}

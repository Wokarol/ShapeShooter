using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectsGroup : MonoBehaviour
{
    [System.Serializable]
    struct MovingObject
    {
        public Transform Target { get => target; set => target = value; }
        public Vector3 MovementOffset { get => movementOffset; set => movementOffset = value; }
        public AnimationCurve LerpCurve { get => lerpCurve; set => lerpCurve = value; }

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 movementOffset;
        [SerializeField] private AnimationCurve lerpCurve;

        Vector3 startPos;
        Vector3 endPos;

        public void Recenter() {
            startPos = Target.localPosition;
            endPos = startPos += MovementOffset;
        }

        public void Evalute(float time) {
            var curvedTime = LerpCurve.Evaluate(time);
            Target.localPosition = Vector3.Lerp(startPos, endPos, curvedTime);
        }
    }

    [SerializeField] MovingObject[] movingObjects = new MovingObject[0];
    [SerializeField] [Range(0, 1)] float LerpValue = 0;

    private void Start() {
        foreach (var movingObject in movingObjects) {
            movingObject.Recenter();
        }
    }

    private void Update() {
        foreach (var movingObject in movingObjects) {
            movingObject.Evalute(LerpValue);
        }
    }

    private void OnDrawGizmosSelected() {
        foreach (var movingObject in movingObjects) {
            if (movingObject.Target == null) return;
            Gizmos.color = Color.red;
            Gizmos.DrawRay(movingObject.Target.position, transform.TransformDirection(movingObject.MovementOffset));
            Gizmos.DrawSphere(movingObject.Target.position + transform.TransformDirection(movingObject.MovementOffset), 0.2f);
        }
    }
}

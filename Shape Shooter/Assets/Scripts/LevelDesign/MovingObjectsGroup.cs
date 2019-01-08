using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectsGroup : MonoBehaviour
{
    [System.Serializable]
    class MovingObject
    {
        public Transform Target { get => target; set => target = value; }
        public Vector3 MovementOffset { get => movementOffset; set => movementOffset = value; }
        public AnimationCurve LerpCurve { get => lerpCurve; set => lerpCurve = value; }

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 movementOffset;
        [SerializeField] private AnimationCurve lerpCurve;

        Vector3 startPos;
        Vector3 EndPos => startPos + movementOffset;
        public bool Recentered { get; private set; } = false;

        public void Recenter() {
            startPos = Target.localPosition;
            Recentered = true;
        }

        public void Evalute(float time) {
            var curvedTime = LerpCurve.Evaluate(time);
            Target.localPosition = Vector3.Lerp(startPos, EndPos, curvedTime);
        }

        public Vector3 Preview(float time) {
            var curvedTime = LerpCurve.Evaluate(time);
            if (!Recentered)
                return Vector3.Lerp(target.localPosition, target.localPosition + movementOffset, curvedTime);
            else
                return Vector3.Lerp(startPos, EndPos, curvedTime);
        }
    }

    [SerializeField] MovingObject[] movingObjects = new MovingObject[0];
    [SerializeField] [Range(0, 1)] float lerpValue = 0;
    public float LerpValue { get => lerpValue; set => lerpValue = Mathf.Clamp01(value); }

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

            PreviewObject(movingObject);
        }
    }
    private void OnDrawGizmos() {
        if (lerpValue > float.Epsilon && !Application.isPlaying)
            foreach (var movingObject in movingObjects) {
                PreviewObject(movingObject);
            }
    }
    private void PreviewObject(MovingObject movingObject) {
        Gizmos.color = Color.blue;
        Vector3 previewPos = movingObject.Preview(LerpValue);
        Gizmos.DrawWireCube(previewPos + transform.position, movingObject.Target.lossyScale);
    }
}

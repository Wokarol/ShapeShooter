using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class DynamicSpriteController : MonoBehaviour
    {
        new SpriteRenderer renderer;
        MaterialPropertyBlock spritePropertyBlock;
        [SerializeField] float speedMultiplier = 0.1f;
        [SerializeField] float smoothing = 0f;

        Vector3 lastPos;

        Vector3 currentVelocity;
        Vector3 currentSmoothVelocity;

        public MaterialPropertyBlock SpritePropertyBlock {
            get {
                if (spritePropertyBlock == null) {
                    spritePropertyBlock = new MaterialPropertyBlock();
                }
                return spritePropertyBlock;
            }
        }
        public SpriteRenderer Renderer {
            get {
                if (renderer == null) {
                    renderer = GetComponent<SpriteRenderer>();
                }
                return renderer;
            }
        }

        private void Start() {
            lastPos = transform.position;
            currentVelocity = Vector3.zero;
        }

        private void Update() {
            var velocity = (lastPos - transform.position)/Time.deltaTime;

            currentVelocity = Vector3.SmoothDamp(currentVelocity, velocity, ref currentSmoothVelocity, smoothing);

            UpdateValues(currentVelocity.normalized, currentVelocity.magnitude * (speedMultiplier));
            lastPos = transform.position;

            Debug.DrawRay(transform.position, -velocity * 0.25f, Color.red);
            Debug.DrawRay(transform.position, -currentVelocity * 0.25f, Color.blue);
        }

        private void OnEnable() {
            lastPos = transform.position;
        }

        private void OnDisable() {
            UpdateValues(Vector2.zero, 0);
        }

        private void UpdateValues(Vector2 dir, float speed) {
            Renderer.GetPropertyBlock(SpritePropertyBlock);
            spritePropertyBlock.SetVector("_Direction", dir);
            spritePropertyBlock.SetFloat("_Speed", speed);
            Renderer.SetPropertyBlock(SpritePropertyBlock);
        }
    } 
}

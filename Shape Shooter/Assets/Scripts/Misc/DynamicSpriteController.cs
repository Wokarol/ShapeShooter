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

        Vector3 lastPos;

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

        private void Update() {
            var velocity = lastPos - transform.position;
            UpdateValues(velocity.normalized, velocity.magnitude * (speedMultiplier / Time.deltaTime));
            lastPos = transform.position;
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

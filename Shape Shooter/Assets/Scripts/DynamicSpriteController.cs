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
        [SerializeField] new Rigidbody2D rigidbody = null;
        [SerializeField] float speedMultiplier = 0.1f;

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
        private void OnValidate() {
            if (rigidbody == null) {
                rigidbody = GetComponentInParent<Rigidbody2D>();
            }
        }

        private void Update() {
            UpdateValues(rigidbody.velocity.normalized, rigidbody.velocity.magnitude * speedMultiplier);
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

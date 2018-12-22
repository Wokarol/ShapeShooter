using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteInEditMode()]
    public class CustomShaderGraphSprite : MonoBehaviour
    {
        new SpriteRenderer renderer;
        MaterialPropertyBlock spritePropertyBlock;

        public MaterialPropertyBlock SpritePropertyBlock {
            get {
                if (spritePropertyBlock == null) {
                    spritePropertyBlock = new MaterialPropertyBlock();
                }
                return spritePropertyBlock;
            }
            set => spritePropertyBlock = value;
        }
        public SpriteRenderer Renderer {
            get {
                if (renderer == null) {
                    renderer = GetComponent<SpriteRenderer>();
                }
                return renderer;
            }
            set => renderer = value;
        }

        private void Update() {
            Renderer.GetPropertyBlock(SpritePropertyBlock);
            SpritePropertyBlock.SetTexture("_MainTexCustom", Renderer.sprite.texture);
            SpritePropertyBlock.SetColor("_ColorCustom", Renderer.color);
            Renderer.SetPropertyBlock(SpritePropertyBlock);
        }
    } 
}

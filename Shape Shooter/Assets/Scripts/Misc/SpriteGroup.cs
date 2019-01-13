using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Wokarol
{
    [ExecuteInEditMode]
    public class SpriteGroup : MonoBehaviour
    {
        [SerializeField] SpriteRenderer[] sprites = new SpriteRenderer[0];
        [SerializeField][Range(0, 1)] float alpha = 1;

        private void Update() {
            foreach (var sprite in sprites) {
                var color = sprite.color;
                color.a = alpha;
                sprite.color = color;
            }
        }
    } 
}

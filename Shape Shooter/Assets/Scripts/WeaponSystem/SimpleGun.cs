using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

namespace Wokarol.GunSystem
{
    public class SimpleGun : SingleShotGun
    {
        [SerializeField] Pool bulletPool = null;
        [SerializeField] Animator animator = null;
        [SerializeField] string animationTrigger = "Shot";

        int animatorShotHash;
        private void Awake() {
            animatorShotHash = Animator.StringToHash(animationTrigger);
        }

        protected override void Shot() {
            bulletPool.Get(transform.position, Quaternion.FromToRotation(Vector3.up, input.AimDirection));
            if (animator) animator.SetTrigger(animatorShotHash);
        }
    }
}

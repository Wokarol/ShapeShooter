using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

namespace Wokarol.GunSystem
{
    public class MachineGun : RapidShotGun
    {
        [SerializeField] Pool bulletPool = null;
        [Space]
        [SerializeField] Animator animator = null;
        [SerializeField] string animationTrigger = "Shot";
        [Space]
        [SerializeField] float spread = 15;
        [Space]
        [SerializeField] Cinemachine.CinemachineImpulseSource impulseSource = null;

        private void OnValidate() {
            if (spread < 0) spread *= -1;
        }

        int animatorShotHash;
        private void Awake() {
            animatorShotHash = Animator.StringToHash(animationTrigger);
        }

        protected override void Shot() {
            impulseSource.GenerateImpulse();
            bulletPool.Get(transform.position, Quaternion.FromToRotation(Vector3.up, input.AimDirection) * Quaternion.Euler(0, 0, Random.Range(-spread, spread)));
            if (animator) animator.SetTrigger(animatorShotHash);
        }
    }
}

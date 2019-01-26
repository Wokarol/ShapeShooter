using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

namespace Wokarol.GunSystem
{
    public class MachineGun : RapidShotGun
    {
        [Space]
        [SerializeField] Animator animator = null;
        [SerializeField] string animationTrigger = "Shot";
        [Space]
        [SerializeField] AudioSource audioSource = null;
        [Space]
        [SerializeField] Pool bulletPool = null;
        [Space]
        [SerializeField] float spread = 15;
        [Space]
        [SerializeField] Cinemachine.CinemachineImpulseSource impulseSource = null;

        int animatorShotHash;
        private void Awake() {
            animatorShotHash = Animator.StringToHash(animationTrigger);
        }

        private void OnValidate() {
            if (spread < 0) spread *= -1;
            animatorShotHash = Animator.StringToHash(animationTrigger);
        }

        protected override void Shoot() {
            impulseSource?.GenerateImpulse();
            bulletPool.Get(transform.position, Quaternion.FromToRotation(Vector3.up, input.AimDirection) * Quaternion.Euler(0, 0, Random.Range(-spread, spread)));
            if (animator) animator.SetTrigger(animatorShotHash);
            if (audioSource) audioSource.Play();
        }
    }
}

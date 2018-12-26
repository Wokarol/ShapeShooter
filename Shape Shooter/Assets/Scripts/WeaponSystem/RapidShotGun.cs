using UnityEngine;
using Wokarol.InputSystem;

namespace Wokarol.GunSystem
{
    public abstract class RapidShotGun : MonoBehaviour
    {
        [SerializeField] protected InputData input;
        [SerializeField] float shotInterval = 0.5f;

        float shotCountdown = -1;

        private void OnValidate() {
            if (!input) input = GetComponent<InputData>();
        }

        private void Update() {
            if(input.Shoot && shotCountdown < 0) {
                shotCountdown = shotInterval;
                Shot();
            }
            shotCountdown -= Time.deltaTime;
        }

        protected abstract void Shot();
    } 
}
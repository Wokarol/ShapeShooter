using UnityEngine;
using Wokarol.InputSystem;

namespace Wokarol.GunSystem
{
    public abstract class SingleShotGun : MonoBehaviour
    {
        [SerializeField] protected InputData input;
        bool shooted;

        private void OnValidate() {
            if (!input) input = GetComponent<InputData>();
        }

        private void Update() {
            if(input.Shoot && !shooted) {
                shooted = true;
                Shot();
            }
            else if(!input.Shoot && shooted) {
                shooted = false;
            }
        }

        protected abstract void Shot();
    } 
}
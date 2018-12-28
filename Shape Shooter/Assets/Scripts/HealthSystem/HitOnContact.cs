using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.HealthSystem
{
    public class HitOnContact : MonoBehaviour
    {
        [SerializeField] int damagePerContact = 1;
        [SerializeField] bool destroyOnCollision = false;
        [SerializeField] bool rotateToFaceNormal = false;

        private void OnTriggerEnter2D(Collider2D collision) {
            DoDamage(collision);
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            if (rotateToFaceNormal) {
                var newUp = -collision.GetContact(0).normal;
                transform.rotation = Quaternion.FromToRotation(Vector3.up, newUp);
            }

            DoDamage(collision.collider);
        }

        void DoDamage(Collider2D collision) {
            var damagable = collision.GetComponent<IDamagable>();
            if (damagable != null)
                damagable.Hit(damagePerContact);

            if (destroyOnCollision) {
                var destroyable = GetComponent<IDestroyable>();
                if (destroyable != null)
                    destroyable.Destroy(); 
            }
        }
    }
}

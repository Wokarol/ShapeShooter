using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.HealthSystem
{
    public class HitOnContact : MonoBehaviour
    {
        [SerializeField] int damagePerContact = 1;
        [SerializeField] bool destroyOnCollision;

        private void OnTriggerEnter2D(Collider2D collision) {
            DoDamage(collision);
        }
        private void OnCollisionEnter2D(Collision2D collision) {
            DoDamage(collision.collider);
        }

        void DoDamage(Collider2D collision) {
            var actorHealth = collision.GetComponent<ActorHealth>();
            if (actorHealth)
                actorHealth.Hit(damagePerContact);

            var destroyable = GetComponent<IDestroyable>();
            if (destroyable != null)
                destroyable.Destroy();
        }
    }
}

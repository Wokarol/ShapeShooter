using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.HealthSystem
{
    public class HealthOutput : MonoBehaviour
    {
        [SerializeField] ActorHealth actorHealth = null;
        [SerializeField] IntVariableReference currentHealth = null;

        private void Awake()
        {
            actorHealth.OnHealthChanged += UpdateHealth;
        }

        void UpdateHealth()
        {
            currentHealth.Value = actorHealth.CurrentHealth;
        }
    } 
}

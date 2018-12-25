using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Wokarol.HealthSystem
{
    public class ActorHealth : MonoBehaviour
    {
        [Header("Starting variables")]
        [SerializeField] int maxHealth = 10;
        [SerializeField] bool setHealthOnStart = true;
        public int MaxHealth { get { return maxHealth; } }

        [Header("Settings")]
        [SerializeField] float invincibilityFrameTime = 0.5f;

        [Header("Events")]
        [SerializeField] UnityEvent onInvincibleFrameStart = new UnityEvent();
        [SerializeField] UnityEvent onInvincibleFrameStop = new UnityEvent();
        [Space]
        [SerializeField] UnityEvent onHit = new UnityEvent();

        public event System.Action OnHealthChanged = null;

        public int CurrentHealth { get; private set; }

        float invincibilityCountdown = -1;
        bool invincible = false;

        private void Start()
        {
            if (setHealthOnStart) {
                CurrentHealth = maxHealth;
                OnHealthChanged?.Invoke(); 
            }
        }

        private void Update()
        {
            if(invincibilityCountdown > 0) {
                invincibilityCountdown -= Time.deltaTime;
            } else if (invincible && invincibilityCountdown <= 0) {
                invincible = false;
                onInvincibleFrameStop.Invoke();
            }
        }

        public void Hit(int hitPoints)
        {
            if(!invincible) {
                if(hitPoints < 0) {
                    Debug.LogWarning("Hitting value should be positive");
                    return;
                }
                CurrentHealth -= hitPoints;
                OnHealthChanged?.Invoke();
                invincible = true;
                invincibilityCountdown = invincibilityFrameTime;
                onInvincibleFrameStart.Invoke();
                onHit.Invoke();

                if(CurrentHealth <= 0) {
                    GetComponent<IDestroyable>()?.Destroy();
                }
            }
        }

        public void Heal(int healPoints)
        {
            if (healPoints < 0) {
                Debug.LogWarning("Healing value should be positive");
                return;
            }
            CurrentHealth = Mathf.Clamp(CurrentHealth + healPoints, 0, maxHealth);
            OnHealthChanged?.Invoke();
        }

        public void SetHealth(int value)
        {
            CurrentHealth = Mathf.Clamp(value, 0, maxHealth);
            if (CurrentHealth <= 0) {
                GetComponent<IDestroyable>()?.Destroy();
            }
            OnHealthChanged?.Invoke();
        }

    } 
}

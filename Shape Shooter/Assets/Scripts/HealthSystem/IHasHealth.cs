using System;

namespace Wokarol.HealthSystem
{
    public interface IHasHealth
    {
        int CurrentHealth { get; }

        event Action OnHealthChanged;

        void ResetHealth();
        void SetHealth(int value);
    }
}
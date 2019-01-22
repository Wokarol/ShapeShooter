using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.HealthSystem;

public class HealthToMaskAlphaCutoff : MonoBehaviour
{
    [SerializeField] SpriteMask mask = null;
    [SerializeField] ActorHealth actorHealth = null;

    private void Update() {
        mask.alphaCutoff = actorHealth.CurrentHealth / (float)actorHealth.MaxHealth;
    }
}

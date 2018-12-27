using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.HealthSystem;
using Wokarol.PoolSystem;

public class Enemy : PoolObject
{
    [SerializeField] GameObject gfx = null;
    [SerializeField] Behaviour[] otherComponents = new Behaviour[0];
    [SerializeField] ActorHealth actorHealth = null;

    public override void Destroy() {
        Deactivate();
        base.Destroy();
    }

    public override void Activate() {
        gfx.SetActive(true);
        foreach (var component in otherComponents) {
            component.enabled = true;
        }
        actorHealth.ResetHealth();
    }

    public override void Deactivate() {
        gfx.SetActive(false);
        foreach (var component in otherComponents) {
            component.enabled = false;
        }
    }

    public override void Recreate() {
        Activate();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.HealthSystem;
using Wokarol.MessageSystem;
using Wokarol.PoolSystem;

public class Enemy : PoolObject
{
    [SerializeField] int pointsPerDestroyed = 5;

    [SerializeField] GameObject gfx = null;
    [SerializeField] Behaviour[] otherComponents = new Behaviour[0];
    IResetable[] resetables;

    [SerializeField] ActorHealth actorHealth = null;
    public System.Action<Enemy> OnEnemyDestroyed;

    public override void Destroy() {
        Messenger.Default.SendMessage(new EnemyDestroyedEvent(pointsPerDestroyed, this));

        Deactivate();
        OnEnemyDestroyed?.Invoke(this);
        base.Destroy();
    }

    public override void Activate() {
        if(resetables == null) resetables = GetComponents<IResetable>();
        gfx.SetActive(true);
        foreach (var component in otherComponents) {
            component.enabled = true;
        }
        foreach (var resetable in resetables) {
            resetable.ResetObject();
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

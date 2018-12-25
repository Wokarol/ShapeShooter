using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : PoolObject
{
    [SerializeField] GameObject gfx = null;
    [SerializeField] float speed = 20;
    new Rigidbody2D rigidbody;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    public override void Activate() {
        gfx.SetActive(true);
        rigidbody.simulated = true;
        rigidbody.velocity = transform.up * speed;
    }

    public override void Deactivate() {
        gfx.SetActive(false);
        rigidbody.simulated = false;
    }

    public override void Recreate() {
        Activate();
    }

    public override void Destroy() {
        Deactivate();
        base.Destroy();
    }
}

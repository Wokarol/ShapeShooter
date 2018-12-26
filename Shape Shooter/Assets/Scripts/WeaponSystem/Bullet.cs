using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : PoolObject
{
    [SerializeField] GameObject gfx = null;
    [Space]
    [SerializeField] AnimationCurve startingSizeCurve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(1, 1));
    [SerializeField] float startingAnimationTime = 0.5f;
    [SerializeField] float speed = 20;
    [Space]
    [SerializeField] ParticleSystem hitParticles = null;
    [SerializeField] float afterDestroyWait = -1;

    new Rigidbody2D rigidbody;

    float lifetime;
    float particleStartTime;
    bool waitForDestroyParticles = false;

    Vector3 startingScale;

    private void Awake() {
        rigidbody = GetComponent<Rigidbody2D>();
        startingScale = transform.localScale;
        afterDestroyWait = Mathf.Max(hitParticles.main.startLifetime.constantMax, hitParticles.main.startLifetime.constant);
    }

    private void Update() {
        lifetime += Time.deltaTime;

        transform.localScale = startingScale * startingSizeCurve.Evaluate(lifetime / startingAnimationTime);

        if (waitForDestroyParticles && lifetime > particleStartTime + afterDestroyWait) {
            waitForDestroyParticles = false;
            base.Destroy();
        }
    }

    public override void Activate() {
        gfx.SetActive(true);
        rigidbody.simulated = true;
        rigidbody.velocity = transform.up * speed;
        lifetime = 0;
    }

    public override void Deactivate() {
        gfx.SetActive(false);
        rigidbody.simulated = false;
        waitForDestroyParticles = false;
    }

    public override void Recreate() {
        Activate();
    }

    public override void Destroy() {
        Deactivate();

        Debug.DrawLine(transform.position + Vector3.right, transform.position + Vector3.left, Color.red, 5f);
        Debug.DrawLine(transform.position + Vector3.up, transform.position + Vector3.down, Color.red, 5f);

        hitParticles.Play();
        particleStartTime = lifetime;
        waitForDestroyParticles = true;
    }
}

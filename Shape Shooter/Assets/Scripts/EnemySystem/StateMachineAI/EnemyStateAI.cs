using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Wokarol.AI;
using Wokarol.StateSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateAI : MonoBehaviour
{
    StateMachine aiMachine;
    [SerializeField] Target target = new Target();
    NavMeshAgent agent;

    [SerializeField] LayerMask targetLayer = 0;
    [SerializeField] float distance = 10;
    [SerializeField] float stoppingDistance = 0.2f;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();

        var wait = new WaitState();
        var attacking = new AttackingState(target, agent, wait);

        wait.AddTransition(() => target.Transform != null, attacking);
        attacking.AddTransition(() => target.Transform == null, wait);

        //wait.AddTransition(() => DEBUG_EnemyInSight, attacking);
        //attacking.AddTransition(() => !DEBUG_EnemyInSight, wait);

        aiMachine = new StateMachine(wait);
    }

    private void Update() {
        TargetCalculation();
        agent.stoppingDistance = stoppingDistance;
        aiMachine.Tick();
    }

    private void TargetCalculation() {
        if (!target.Transform) {
            var collider = Physics2D.OverlapCircle(transform.position, distance, targetLayer);
            if (collider) {
                target.Transform = collider.transform;
            }
        }
        if (target.Transform) {
            float sqrDist = Vector2.SqrMagnitude(transform.position - target.Transform.position);
            if (sqrDist > (distance * distance)) {
                target.Transform = null;
            }
        }
    }
}

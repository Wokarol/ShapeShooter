﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Wokarol.AI;
using Wokarol.StateSystem;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStateAI : MonoBehaviour, IResetable
{
    private const string TargetID = "AI_Target";

    public DebugBlock AIDebugBlock { get; } = new DebugBlock("AI");

    StateMachine aiMachine;
    [SerializeField] Target target = new Target();
    NavMeshAgent agent;

    [SerializeField] LayerMask targetLayer = 0;
    [SerializeField] float distance = 10;
    [SerializeField] float stoppingDistance = 0.2f;

    private void Start() {
        agent = GetComponent<NavMeshAgent>();
        AIDebugBlock.Define("Target", TargetID);

        var wait = new WaitState();
        var attacking = new AttackingState(target, agent, wait);

        wait.AddTransition(() => target.Transform != null, attacking);
        attacking.AddTransition(() => target.Transform == null, wait);

        aiMachine = new StateMachine(wait, AIDebugBlock);
    }

    private void Update() {
        TargetCalculation();
        agent.stoppingDistance = stoppingDistance;
        aiMachine?.Tick();
    }

    private void TargetCalculation() {
        if (!target.Transform) {
            var collider = Physics2D.OverlapCircle(transform.position, distance, targetLayer);
            if (collider) {
                target.Transform = collider.transform;
                AIDebugBlock.Change(TargetID, target.Transform.name);
            }
        }
        if (target.Transform) {
            float sqrDist = Vector2.SqrMagnitude(transform.position - target.Transform.position);
            if (sqrDist > (distance * distance)) {
                target.Transform = null;
                AIDebugBlock.Change(TargetID, "null");
            }
        }
    }

    public void ResetObject() {
        aiMachine?.Restart();
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, distance);
        if (target.Transform) {
            Gizmos.color = Color.red * Color.grey;
            Gizmos.DrawWireSphere(target.Transform.position, 0.2f);
        }
        if (agent) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.destination, 0.3f);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}

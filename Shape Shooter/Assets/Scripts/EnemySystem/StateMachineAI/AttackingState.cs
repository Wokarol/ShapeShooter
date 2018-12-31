using UnityEngine;
using UnityEngine.AI;
using Wokarol.AI;
using Wokarol.StateSystem;

public class AttackingState : State
{
    const float targetRefreshInterval = 0.2f;
    private Target target;
    private NavMeshAgent agent;
    private WaitState wait;
    private float countdown;

    public AttackingState(Target target, NavMeshAgent agent, WaitState wait) {
        this.target = target;
        this.agent = agent;
        this.wait = wait;
    }

    public override void Enter(StateMachine stateMachine) {
        countdown = 0;
    }

    public override void Exit() {
        agent.SetDestination(agent.transform.position + agent.velocity);
    }

    protected override State Process() {
        countdown -= Time.deltaTime;
        if (countdown < 0) {
            countdown += targetRefreshInterval;
            if(target.Transform) agent.SetDestination(target.Transform.position);
        }
        return null;
    }
}

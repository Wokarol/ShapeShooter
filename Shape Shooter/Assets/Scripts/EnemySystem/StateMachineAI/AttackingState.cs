using UnityEngine;
using UnityEngine.AI;
using Wokarol.AI;
using Wokarol.StateSystem;

public class AttackingState : State
{
    private Target target;
    private NavMeshAgent agent;
    private WaitState wait;

    public AttackingState(Target target, NavMeshAgent agent, WaitState wait) {
        this.target = target;
        this.agent = agent;
        this.wait = wait;
    }

    public override void Enter(StateMachine stateMachine) {
        Debug.Log($"<b> It's attacking time</b>");
    }

    public override void Exit() {
        Debug.Log($"<b> I see no enemy</b>");
    }

    protected override State Process() {
        return null;
    }
}
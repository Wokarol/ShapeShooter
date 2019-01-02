using UnityEngine;
using UnityEngine.AI;
using Wokarol.AI;
using Wokarol.StateSystem;

public class AttackingState : State
{
    const float targetRefreshInterval = 0.2f;
    private const string TargetID = "State_Target";
    private const string CountdownID = "State_Countdown";

    private Target _target;
    private NavMeshAgent _agent;
    private WaitState _wait;
    private float _countdown;
    private DebugBlock _debugBlock;

    public AttackingState(Target target, NavMeshAgent agent, WaitState wait) {
        _target = target;
        _agent = agent;
        _wait = wait;
    }

    public override void Enter(StateMachine stateMachine) {
        _debugBlock = stateMachine.DebugBlock;
        _debugBlock.Define("Target", TargetID);
        _debugBlock.Define("Countdown", CountdownID);
        _countdown = 0;
    }

    public override void Exit(StateMachine stateMachine) {
        _agent.SetDestination(_agent.transform.position + _agent.velocity);
        _debugBlock.Undefine(CountdownID);
        _debugBlock.Undefine(TargetID);
    }

    protected override State Process() {
        _countdown -= Time.deltaTime;
        _debugBlock.Change(CountdownID, _countdown.ToString("F5"));
        if (_countdown < 0) {
            _countdown += targetRefreshInterval;
            if (_target.Transform) {
                _agent.SetDestination(_target.Transform.position);
                _debugBlock.Change(TargetID, _target.Transform.name);
            }
        }
        return null;
    }
}

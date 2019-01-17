using UnityEngine;
using UnityEngine.AI;
using Wokarol.AI;
using Wokarol.StateSystem;

public class GoTowardsTargetState : State
{
    const float targetRefreshInterval = 0.2f;

    #region DebugBlock
#if UNITY_EDITOR
    private const string TargetID = "State_Target";
    private const string CountdownID = "State_Countdown";
    private DebugBlock _debugBlock;
#endif
    #endregion

    public override bool CanTransitionToSelf => true;

    private Target _target;
    private NavMeshAgent _agent;
    private WaitState _wait;
    private float _countdown;

    public GoTowardsTargetState(string name, Target target, NavMeshAgent agent, WaitState wait) {
        Name = name;
        _target = target;
        _agent = agent;
        _wait = wait;
    }
    protected override void EnterProcess(StateMachine stateMachine) {
        #region DebugBlock
#if UNITY_EDITOR
        _debugBlock = stateMachine.DebugBlock;
        _debugBlock.Define("Target", TargetID);
        _debugBlock.Define("Countdown", CountdownID);
#endif
        #endregion
        _countdown = 0;
    }

    protected override void ExitProcess(StateMachine stateMachine) {
        _agent.SetDestination(_agent.transform.position + _agent.velocity);
        #region DebugBlock
#if UNITY_EDITOR
        _debugBlock.Undefine(CountdownID);
        _debugBlock.Undefine(TargetID);
#endif
        #endregion
    }

    protected override State Process() {
        _countdown -= Time.deltaTime;
        #region DebugBlock
#if UNITY_EDITOR
        _debugBlock.Change(CountdownID, _countdown.ToString("F5"));
#endif
        #endregion
        if (_countdown < 0) {
            _countdown += targetRefreshInterval;
            if (_target.Transform) {
                _agent.SetDestination(_target.Transform.position);
                #region DebugBlock
#if UNITY_EDITOR
                _debugBlock.Change(TargetID, _target.Transform.name);
#endif
                #endregion
            }
        }
        return null;
    }
}

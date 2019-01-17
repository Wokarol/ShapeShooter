using UnityEngine;
using Wokarol.AI;
using Wokarol.InputSystem;
using Wokarol.StateSystem;

public class ShootAtTargetState : State
{
    Target _target;
    ICanModifyShootingInput _input;
    private Transform _me;
    private const string TargetID = "State_Target";
    private DebugBlock _debugBlock;

    public ShootAtTargetState(string name, Target target, Transform me, ICanModifyShootingInput inputdata) {
        Name = name;
        _target = target;
        _input = inputdata;
        _me = me;
    }

    public override bool CanTransitionToSelf => false;


    protected override void EnterProcess(StateMachine stateMachine) {
        _debugBlock = stateMachine.DebugBlock;
        _debugBlock.Define("Target", TargetID);
    }

    protected override void ExitProcess(StateMachine stateMachine) {
        _debugBlock.Undefine(TargetID);
        _input.Shoot = false;
    }

    protected override State Process() {
        if (_target.Transform) {
            _debugBlock.Change(TargetID, _target.Transform.name);

            _input.AimDirection = ((Vector2)(_target.Transform.position - _me.position)).normalized;
            _input.Shoot = true;
        }
        return null;
    }
}
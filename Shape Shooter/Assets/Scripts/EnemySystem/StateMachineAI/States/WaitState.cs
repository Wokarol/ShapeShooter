using UnityEngine;
using Wokarol.StateSystem;

public class WaitState : State
{
    public WaitState(string name) {
        Name = name;
    }

    public override bool CanTransitionToSelf => false;

    protected override void EnterProcess(StateMachine stateMachine) {
    }

    protected override void ExitProcess(StateMachine stateMachine) {
    }

    protected override State Process() {
        return null;      
    }
}
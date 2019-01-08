﻿using UnityEngine;
using Wokarol.StateSystem;

public class WaitState : State
{
    public WaitState(string name) {
        Name = name;
    }

    public override bool CanTransitionToSelf => false;

    public override void Enter(StateMachine stateMachine) {
    }

    public override void Exit(StateMachine stateMachine) {
    }

    protected override State Process() {
        return null;      
    }
}
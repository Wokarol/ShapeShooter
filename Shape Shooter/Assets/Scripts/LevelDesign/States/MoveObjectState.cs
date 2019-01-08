using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.StateSystem;

public class MoveObjectState : State
{
    public override bool CanTransitionToSelf => false;

    float target;
    MovingObjectsGroup group;
    private float speed;

    public MoveObjectState(string name, float target, MovingObjectsGroup group, float speed) {
        Name = name;
        this.target = target;
        this.group = group;
        this.speed = speed;
    }

    public override void Enter(StateMachine stateMachine) {
    }

    public override void Exit(StateMachine stateMachine) {
    }

    protected override State Process() {
        group.LerpValue = Mathf.MoveTowards(group.LerpValue, target, speed * Time.deltaTime);
        return null;
    }
}

public class MoveObjectsState : State
{
    public override bool CanTransitionToSelf => false;

    float target;
    MovingObjectsGroup[] groups;
    private float speed;

    public MoveObjectsState(string name, float target, MovingObjectsGroup[] groups, float speed) {
        Name = name;
        this.target = target;
        this.groups = groups;
        this.speed = speed;
    }

    public override void Enter(StateMachine stateMachine) {
    }

    public override void Exit(StateMachine stateMachine) {
    }

    protected override State Process() {
        foreach (var group in groups) {
            group.LerpValue = Mathf.MoveTowards(group.LerpValue, target, speed * Time.deltaTime);
        }
        return null;
    }
}

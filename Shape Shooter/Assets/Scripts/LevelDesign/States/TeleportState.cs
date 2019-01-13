using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.StateSystem;

namespace Wokarol.LevelDesign
{
    public class TeleportState : State, IHasExitState
    {
        Teleporter _start;
        Vector3 _end;
        Action onTeleportAction;

        public TeleportState(Teleporter start, Vector3 end, Action onTeleport) : this(start, end) {
            onTeleportAction = onTeleport;
        }

        public TeleportState(Teleporter start, Vector3 end) {
            _start = start;
            _end = end;
        }

        public override bool CanTransitionToSelf => throw new System.NotImplementedException();
        public State ExitState { get; set; }
        public override void Enter(StateMachine stateMachine) {
            _start.Teleport(_end);
            onTeleportAction?.Invoke();
        }
        public override void Exit(StateMachine stateMachine) {

        }
        protected override State Process() {
            return ExitState;
        }
    } 
}

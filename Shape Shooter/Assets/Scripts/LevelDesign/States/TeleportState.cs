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

        public TeleportState(Teleporter start, Vector3 end) {
            _start = start;
            _end = end;
        }

        public override bool CanTransitionToSelf => throw new System.NotImplementedException();
        public State ExitState { get; set; }
        public override void Enter(StateMachine stateMachine) {
            _start.Teleport(_end);
        }
        public override void Exit(StateMachine stateMachine) {

        }
        protected override State Process() {
            return ExitState;
        }
    } 
}

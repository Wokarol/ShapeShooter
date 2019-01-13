using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.StateSystem;

namespace Wokarol.LevelDesign
{
    public class MoveObjectsState : State, ICanBeFinished , IHasExitState
    {
        public override bool CanTransitionToSelf => false;
        public bool Finished { get; private set; }
        public State ExitState { get; set; }

        float target;
        MovingObjectsGroup[] groups;
        private float speed;

        public MoveObjectsState(string name, float target, MovingObjectsGroup group, float speed) {
            Name = name;
            this.target = target;
            this.groups = new MovingObjectsGroup[] { group };
            this.speed = speed;
        }

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
            Finished = true;
            foreach (var group in groups) {
                group.LerpValue = Mathf.MoveTowards(group.LerpValue, target, speed * Time.deltaTime);
                Finished = Finished && Mathf.Abs(group.LerpValue - target) < float.Epsilon;
            }
            if (Finished) {
                Debug.Log($"{Name} Finished, returning {(ExitState != null?ExitState.Name:"null")}");
                return ExitState;
            } else {
                Debug.Log($"{Name} Not finished");
                return null;
            }
        }
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.StateSystem;

namespace Wokarol.LevelBrains
{
    public class Tutorial_1_Brain : MonoBehaviour
    {
        private const string TimeID = "Brain_Time";
        StateMachine levelMachine;
        public DebugBlock BrainDebugBlock { get; } = new DebugBlock("Level Brain");

        [Header("Moving Groups")]
        [SerializeField] MovingObjectsGroup horizontalGroup;
        [SerializeField] MovingObjectsGroup verticalGroup;

        [Header("Moving Distances")]
        [SerializeField] float horizontalFirstPhaseDistance = 0.14f;
        [SerializeField] float verticalFirstPhaseDistance = 0.14f;

        [Header("Timming")]
        [SerializeField] float timeToStart = 15f;

        private void Awake() {
            BrainDebugBlock.Define("Time", TimeID);

            var builder = new SequenceBuilder();

            builder.Add(new NullState(), () => Time.time > timeToStart);
            builder.Add(new MoveObjectState(horizontalFirstPhaseDistance, horizontalGroup, 1), () => Time.time > timeToStart * 2f);
            builder.Add(new MoveObjectState(verticalFirstPhaseDistance, verticalGroup, 1), () => Time.time > timeToStart * 3f);
            builder.Add(new MoveObjectsState(1, new MovingObjectsGroup[]{ verticalGroup, horizontalGroup }, 1));

            levelMachine = new StateMachine(builder.Compose(), BrainDebugBlock);
        }

        private void Update() {
            BrainDebugBlock.Change(TimeID, Time.time.ToString("F2"));
            levelMachine?.Tick();
        }
    }

    class NullState : State
    {
        public override bool CanTransitionToSelf => true;

        public override void Enter(StateMachine stateMachine) {
        }

        public override void Exit(StateMachine stateMachine) {
        }

        protected override State Process() {
            return null;
        }
    }

}

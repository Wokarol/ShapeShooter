using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.StateSystem;

namespace Wokarol.LevelBrains
{
    public class Tutorial_1_Brain : MonoBehaviour
    {
        StateMachine levelMachine;
        StateMachine levelMachine2;
        public DebugBlock BrainDebugBlock { get; } = new DebugBlock("Level Brain");
        public DebugBlock BrainDebugBlock2 { get; } = new DebugBlock("Level Brain with Sequencer");


        [Header("Moving Groups")]
        [SerializeField] MovingObjectsGroup horizontalGroup;
        [SerializeField] MovingObjectsGroup verticalGroup;

        [SerializeField] bool transition = false;

        private void Awake() {
            // Without Sequencer
            var sequence0 = new DebugState("N00");
            var sequence1 = new DebugState("N01");
            var sequence2 = new DebugState("N02");
            var sequence3 = new DebugState("N03");
            var sequence4 = new DebugState("N04");
            sequence0.AddTransition(() => transition, sequence1, () => transition = false);
            sequence1.AddTransition(() => transition, sequence2, () => transition = false);
            sequence2.AddTransition(() => transition, sequence3, () => transition = false);
            sequence3.AddTransition(() => transition, sequence4, () => transition = false);
            levelMachine = new StateMachine(sequence0, BrainDebugBlock);

            // With Sequencer
            var builder = new SequenceBuilder();
            builder.Add(new DebugState("N00"), () => transition, () => transition = false);
            builder.Add(new DebugState("N01"), () => transition, () => transition = false);
            builder.Add(new DebugState("N02"), () => transition, () => transition = false);
            builder.Add(new DebugState("N03"), () => transition, () => transition = false);
            builder.Add(new DebugState("N04"), () => transition, () => transition = false);
            levelMachine2 = new StateMachine(builder.Compose(), BrainDebugBlock2);
        }

        private void Update() {
            levelMachine?.Tick();
        }
    }

    class DebugState : State
    {
        string data = "";
        private const string DataID = "DebugState_Data";
        public override bool CanTransitionToSelf => false;
        public DebugState(string data) {
            this.data = data;
        }
        public override void Enter(StateMachine stateMachine) {
            stateMachine.DebugBlock.Define("Data", DataID, data);
        }
        public override void Exit(StateMachine stateMachine) {
            stateMachine.DebugBlock.Undefine(DataID);
        }
        protected override State Process() {
            return null;
        }
    }
}

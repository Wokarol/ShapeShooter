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
        [SerializeField] MovingObjectsGroup horizontalGroup = null;
        [SerializeField] MovingObjectsGroup verticalGroup = null;

        [Header("Moving Distances")]
        [SerializeField] float horizontalFirstPhaseDistance = 0.14f;
        [SerializeField] float verticalFirstPhaseDistance = 0.14f;

        [Header("Timming")]
        [SerializeField] float timeToStart = 15f;

        private void Awake() {
            BrainDebugBlock.Define("Time", TimeID);

            var builder = new SequenceBuilder();

            builder.Add(new WaitState("Wait for time"), () => Time.time > timeToStart);
            builder.Add(new MoveObjectState("Moving horizontal space",horizontalFirstPhaseDistance, horizontalGroup, 1), () => Time.time > timeToStart * 2f);
            builder.Add(new MoveObjectState("Moving vertical space", verticalFirstPhaseDistance, verticalGroup, 1), () => Time.time > timeToStart * 3f);
            builder.Add(new MoveObjectsState("Moving whole space", 1, new MovingObjectsGroup[]{ verticalGroup, horizontalGroup }, 1));

            levelMachine = new StateMachine(builder.Compose(), BrainDebugBlock);
        }

        private void Update() {
            BrainDebugBlock.Change(TimeID, Time.time.ToString("F2"));
            levelMachine?.Tick();
        }
    }
}

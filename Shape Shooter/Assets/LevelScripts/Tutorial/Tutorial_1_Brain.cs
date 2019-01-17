using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.LevelDesign;
using Wokarol.SpawnSystem;
using Wokarol.StateSystem;

namespace Wokarol.LevelBrains
{
    public class Tutorial_1_Brain : MonoBehaviour
    {
        private const string TimeID = "Brain_Time";
        StateMachine levelMachine;
        public DebugBlock BrainDebugBlock { get; } = new DebugBlock("Level Brain");

        [Header("Scenes")]
        [SerializeField] StringVariable nextScene;

        [Header("Teleports")]
        [SerializeField] Teleporter movingLevelTeleporter = null;
        [SerializeField] Teleporter shootingLevelTeleporter = null;

        [Header("Cameras")]
        [SerializeField] GameObject movingLevelCamera = null;
        [SerializeField] GameObject shootingLevelCamera = null;

        [Header("Moving Groups")]
        [SerializeField] MovingObjectsGroup horizontalGroup = null;
        [SerializeField] MovingObjectsGroup verticalGroup = null;

        [Header("Moving Distances")]
        [SerializeField] float horizontalFirstPhaseDistance = 0.14f;
        [SerializeField] float verticalFirstPhaseDistance = 0.14f;

        [Header("Objectives")]
        [SerializeField] Objective targetUp = null;
        [SerializeField] Objective targetDown = null;
        [SerializeField] Objective targetLeft = null;
        [SerializeField] Objective targetRight = null;

        [Header("Waves")]
        [SerializeField] WavePattern waveWithDummies = null;
        [SerializeField] WavePattern waveWithStandardEnemies = null;

        [Header("Helpers")]
        [SerializeField] GameObject movementHelper = null;

        [Header("Timming")]
        [SerializeField] float timeToStart = 15f;

        [Header("Other")]
        [SerializeField] Spawner shootingLevelSpawner = null;

        [Header("Debug")]
        [SerializeField] LevelState startState = LevelState.Moving;
        enum LevelState { Moving, Shooting }

        private void Awake() {
            BrainDebugBlock.Define("Time", TimeID);

            movementHelper.SetActive(false);
            shootingLevelCamera.SetActive(false);

            // States
            var waitForTime = new WaitState("Wait for time");
            var movingHorizontal = new MoveObjectsState("Moving horizontal space", horizontalFirstPhaseDistance, horizontalGroup, 1);
            var movingVertical = new MoveObjectsState("Moving vertical space", verticalFirstPhaseDistance, verticalGroup, 1);
            var movingBoth = new MoveObjectsState("Moving whole space", 1, new MovingObjectsGroup[] { verticalGroup, horizontalGroup }, 1);
            var teleport = new TeleportState(movingLevelTeleporter, shootingLevelTeleporter.transform.position, () => { movingLevelCamera.SetActive(false); shootingLevelCamera.SetActive(true); });
            var dummiesWave = new SpawnWaveState(shootingLevelSpawner, waveWithDummies, 0.1f);
            var normalWave = new SpawnWaveState(shootingLevelSpawner, waveWithStandardEnemies, 0.1f);

            // OnEnter or OnExit events
            movingHorizontal.OnEnter += () => movementHelper.SetActive(true);
            movingVertical.OnExit += () => movementHelper.SetActive(false);

            // Transitions
            waitForTime.AddTransition(
                () => Time.time > timeToStart,
                movingHorizontal);
            movingHorizontal.AddTransition(
                () => movingHorizontal.Finished && targetLeft.Achieved && targetRight.Achieved,
                movingVertical);
            movingVertical.AddTransition(
                () => movingVertical.Finished && targetUp.Achieved && targetDown.Achieved,
                movingBoth);
            movingBoth.AddTransition(
                () => movingBoth.Finished,
                teleport);
            teleport.ExitState = dummiesWave;
            dummiesWave.AddTransition(
                () => dummiesWave.Finished && shootingLevelSpawner.CurrentEnemyCount == 0,
                normalWave);
            

            switch (startState) {
                case LevelState.Moving:
                    levelMachine = new StateMachine(waitForTime, BrainDebugBlock);
                    break;
                case LevelState.Shooting:
                    levelMachine = new StateMachine(teleport, BrainDebugBlock);
                    break;
                default:
                    break;
            }

        }

        private void Update() {
            BrainDebugBlock.Change(TimeID, Time.time.ToString("F2"));
            levelMachine?.Tick();
        }
    }
}

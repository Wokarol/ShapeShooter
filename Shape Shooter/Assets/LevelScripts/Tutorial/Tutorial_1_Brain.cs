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

        private readonly int HelperAnimatorActiveBoolHash = Animator.StringToHash("Active");

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
        [SerializeField] Objective exitTarget = null;

        [Header("Waves")]
        [SerializeField] WavePattern waveWithDummies = null;
        [SerializeField] WavePattern waveWithStandardEnemies = null;

        [Header("Helpers")]
        [SerializeField] Animator movementHelper = null;
        [SerializeField] Animator aimingHelper = null;

        [Header("Timming")]
        [SerializeField] float timeToStart = 15f;
        [SerializeField] float timeToStartAfterTeleport = 3f;

        [Header("Other")]
        [SerializeField] Spawner shootingLevelSpawner = null;

        [Header("Debug")]
        [SerializeField] LevelState startState = LevelState.Moving;
        enum LevelState { Moving, Shooting }

        float teleportTimestamp;

        private void Awake() {
            BrainDebugBlock.Define("Time", TimeID);

            movementHelper.SetBool(HelperAnimatorActiveBoolHash, false);
            aimingHelper.SetBool(HelperAnimatorActiveBoolHash, false);
            shootingLevelCamera.SetActive(false);
            exitTarget.gameObject.SetActive(false);

            // States
            var waitForTime = new WaitState("Wait for time");
            var movingHorizontal = new MoveObjectsState("Moving horizontal space", horizontalFirstPhaseDistance, horizontalGroup, 1);
            var movingVertical = new MoveObjectsState("Moving vertical space", verticalFirstPhaseDistance, verticalGroup, 1);
            var movingBoth = new MoveObjectsState("Moving whole space", 1, new MovingObjectsGroup[] { verticalGroup, horizontalGroup }, 1);
            var teleport = new TeleportState(movingLevelTeleporter, shootingLevelTeleporter.transform.position, () => { movingLevelCamera.SetActive(false); shootingLevelCamera.SetActive(true); });
            var waitAfterTeleport = new WaitState("Waiting for first wave");
            var dummiesWave = new SpawnWaveState("Spawning dummies wave", shootingLevelSpawner, waveWithDummies, 0.1f);
            var normalWave = new SpawnWaveState("Spawning acctual wave", shootingLevelSpawner, waveWithStandardEnemies, 0.1f);
            var waitForExit = new WaitState("Wait for exit");

            // OnEnter or OnExit events
            movingHorizontal.OnEnter += () => movementHelper.SetBool(HelperAnimatorActiveBoolHash, true);
            movingVertical.OnExit += () => movementHelper.SetBool(HelperAnimatorActiveBoolHash, false);

            dummiesWave.OnEnter += () => aimingHelper.SetBool(HelperAnimatorActiveBoolHash, true);
            dummiesWave.OnExit += () => aimingHelper.SetBool(HelperAnimatorActiveBoolHash, false);

            teleport.OnExit += () => teleportTimestamp = Time.time;

            waitForExit.OnEnter += () => exitTarget.gameObject.SetActive(true);
            waitForExit.OnExit += () => ScenesController.Instance.ChangeScene(nextScene.Value);

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
            teleport.ExitState = waitAfterTeleport;
            waitAfterTeleport.AddTransition(
                () => Time.time > teleportTimestamp + timeToStartAfterTeleport,
                dummiesWave);
            dummiesWave.AddTransition(
                () => dummiesWave.Finished && shootingLevelSpawner.CurrentEnemyCount == 0,
                normalWave);
            normalWave.AddTransition(
                () => normalWave.Finished && shootingLevelSpawner.CurrentEnemyCount == 0,
                waitForExit);

            waitForExit.AddTransition(
                () => exitTarget.Achieved,
                new WaitState("EXIT"));

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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.LevelDesign;
using Wokarol.SerializationSystem;
using Wokarol.SpawnSystem;
using Wokarol.StateSystem;
using Wokarol.SubLevelSystem;

namespace Wokarol.LevelBrains
{
    public class Tutorial_1_Brain : MonoBehaviour
    {
        private const string SaveDataKey = "LastPositionInLevel";
        private const string LvlBegginignID = "Beggining";
        private const string LvlShootingID = "Shooting";
        private const string TutorialFinishedKey = "Tutorial_Finished";

        private const string TimeID = "Brain_Time";
        private readonly int HelperAnimatorActiveBoolHash = Animator.StringToHash("Active");

        StateMachine levelMachine;
        public DebugBlock BrainDebugBlock { get; } = new DebugBlock("Level Brain");

        [Header("Saving")]
        [SerializeField] SaveData saveData = null;

        [Header("Scenes")]
        [SerializeField] StringVariable nextScene = null;

        [Header("Sub-levels")]
        [SerializeField] SubLevelID movingLevelID = null;
        [SerializeField] SubLevelID shootingLevelID = null;

        [Header("Moving Groups")]
        [SerializeField] MovingObjectsGroup horizontalGroup = null;
        [SerializeField] MovingObjectsGroup verticalGroup = null;
        [SerializeField] MovingObjectsGroup leftDoorGroup = null;

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
        [SerializeField] WavePattern[] shootingWaves = null;

        [Header("Helpers")]
        [SerializeField] Animator movementHelper = null;
        [SerializeField] Animator aimingHelper = null;
        [SerializeField] Animator exitArrow = null;

        [Header("Timming")]
        [SerializeField] float timeToStart = 15f;
        [SerializeField] float timeToStartAfterTeleport = 3f;

        [Header("Other")]
        [SerializeField] Spawner shootingLevelSpawner = null;
        [SerializeField] SubLevelSwitch subLevelSwitch = null;

#if UNITY_EDITOR
        [Header("Debug")]
        [SerializeField] bool ovverideSave = false;
        [SerializeField] LevelState startState = LevelState.Moving;
        enum LevelState { Moving, Shooting }
#endif

        float startTimestamp;
        float waveWaitTimeTimestamp;

        private void Start() {
            startTimestamp = Time.time;
        }

        private void Awake() {
            BrainDebugBlock.Define("Time", TimeID);

            subLevelSwitch.SetAllLevelsState(true);

            movementHelper.SetBool(HelperAnimatorActiveBoolHash, false);
            aimingHelper.SetBool(HelperAnimatorActiveBoolHash, false);
            exitArrow.SetBool(HelperAnimatorActiveBoolHash, false);
            exitTarget.gameObject.SetActive(false);

            subLevelSwitch.SetAllLevelsState(false);

            // States
            var waitForTime = new WaitState("Wait for time");
            var movingHorizontal = new MoveObjectsState("Moving horizontal space", horizontalFirstPhaseDistance, horizontalGroup, 1);
            var movingVertical = new MoveObjectsState("Moving vertical space", verticalFirstPhaseDistance, verticalGroup, 1);
            var movingBoth = new MoveObjectsState("Moving whole space", 1, new MovingObjectsGroup[] { verticalGroup, horizontalGroup }, 1);
            var waitBeforeFirstWave = new WaitState("Waiting for first wave");
            var dummiesWave = new SpawnWaveState("Spawning dummies wave", shootingLevelSpawner, waveWithDummies, 0.1f);

            SpawnWaveState[] spawnWaveStates = new SpawnWaveState[shootingWaves.Length];
            for (int i = 0; i < shootingWaves.Length; i++) {
                var state = spawnWaveStates[i] = new SpawnWaveState($"Spawning wave {i}", shootingLevelSpawner, shootingWaves[i], 0.1f);
                if (i != 0) {
                    var prevState = spawnWaveStates[i - 1];
                    prevState.AddTransition(
                        () => prevState.Finished && shootingLevelSpawner.CurrentEnemyCount == 0,
                        state);
                }
            }

            var openingWalls = new MoveObjectsState("Opening Wall", 1, leftDoorGroup, 1);
            var waitForExit = new WaitState("Wait for exit");

            // OnEnter or OnExit events
            movingHorizontal.OnEnter += () => movementHelper.SetBool(HelperAnimatorActiveBoolHash, true);
            movingVertical.OnExit += () => movementHelper.SetBool(HelperAnimatorActiveBoolHash, false);

            dummiesWave.OnEnter += () => aimingHelper.SetBool(HelperAnimatorActiveBoolHash, true);
            dummiesWave.OnExit += () => aimingHelper.SetBool(HelperAnimatorActiveBoolHash, false);
            dummiesWave.OnExit += () => saveData.SendEntry(SaveDataKey, LvlShootingID);

            waitBeforeFirstWave.OnEnter += () => subLevelSwitch.ChangeLevel(shootingLevelID);
            waitBeforeFirstWave.OnEnter += () => waveWaitTimeTimestamp = Time.time;

            waitForExit.OnEnter += () => exitTarget.gameObject.SetActive(true);
            waitForExit.OnEnter += () => exitArrow.SetBool(HelperAnimatorActiveBoolHash, true);
            waitForExit.OnExit += () => saveData.RemoveEntry(SaveDataKey);
            waitForExit.OnExit += () => saveData.SendEntry(TutorialFinishedKey, true);
            waitForExit.OnExit += () => ScenesController.Instance.ChangeScene(nextScene.Value);

            // Transitions
            waitForTime.AddTransition(
                () => Time.time > startTimestamp + timeToStart,
                movingHorizontal);
            movingHorizontal.AddTransition(
                () => movingHorizontal.Finished && targetLeft.Achieved && targetRight.Achieved,
                movingVertical);
            movingVertical.AddTransition(
                () => movingVertical.Finished && targetUp.Achieved && targetDown.Achieved,
                movingBoth);
            movingBoth.AddTransition(
                () => movingBoth.Finished,
                waitBeforeFirstWave);
            waitBeforeFirstWave.AddTransition(
                () => Time.time > waveWaitTimeTimestamp + timeToStartAfterTeleport,
                dummiesWave);
            dummiesWave.AddTransition(
                () => dummiesWave.Finished && shootingLevelSpawner.CurrentEnemyCount == 0,
                spawnWaveStates[0]);
            spawnWaveStates[spawnWaveStates.Length - 1].AddTransition(
                () => spawnWaveStates[spawnWaveStates.Length - 1].Finished && shootingLevelSpawner.CurrentEnemyCount == 0,
                openingWalls);
            openingWalls.AddTransition(
                () => openingWalls.Finished,
                waitForExit);
            waitForExit.AddTransition(
                () => exitTarget.Achieved,
                new WaitState("EXIT"));

#if UNITY_EDITOR
            if (ovverideSave) {
                Debug.Log("Overwritten save");
                switch (startState) {
                    default:
                    case LevelState.Moving:
                        saveData.SendEntry(SaveDataKey, LvlBegginignID);
                        break;
                    case LevelState.Shooting:
                        saveData.SendEntry(SaveDataKey, LvlShootingID);
                        break;
                }
            }
#endif
            switch (saveData.GetEntry(SaveDataKey, LvlBegginignID)) {
                default:
                case LvlBegginignID:
                    subLevelSwitch.ChangeLevel(movingLevelID);
                    levelMachine = new StateMachine(waitForTime, BrainDebugBlock);
                    break;
                case LvlShootingID:
                    subLevelSwitch.ChangeLevel(shootingLevelID);
                    levelMachine = new StateMachine(dummiesWave, BrainDebugBlock);
                    break;
            }

        }

        private void Update() {
            BrainDebugBlock.Change(TimeID, Time.time.ToString("F2"));
            levelMachine?.Tick();
        }
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.LevelDesign;
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
        [SerializeField] Teleporter MovingLevelTeleporter = null;
        [SerializeField] Teleporter ShootingLevelTeleporter = null;

        [Header("Cameras")]
        [SerializeField] GameObject MovingLevelCamera;
        [SerializeField] GameObject ShootingLevelCamera;

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

        [Header("Helper")]
        [SerializeField] GameObject MovementHelper = null;

        [Header("Timming")]
        [SerializeField] float timeToStart = 15f;

        private void Awake() {
            BrainDebugBlock.Define("Time", TimeID);

            MovementHelper.SetActive(false);
            ShootingLevelCamera.SetActive(false);

            // States
            var waitForTime = new WaitState("Wait for time");
            var movingHorizontal = new MoveObjectsState("Moving horizontal space", horizontalFirstPhaseDistance, horizontalGroup, 1);
            var movingVertical = new MoveObjectsState("Moving vertical space", verticalFirstPhaseDistance, verticalGroup, 1);
            var movingBoth = new MoveObjectsState("Moving whole space", 1, new MovingObjectsGroup[] { verticalGroup, horizontalGroup }, 1);
            var teleport = new TeleportState(MovingLevelTeleporter, ShootingLevelTeleporter.transform.position);

            // Transitions
            waitForTime.AddTransition(
                (s) => Time.time > timeToStart, 
                movingHorizontal, 
                () => MovementHelper.SetActive(true));
            movingHorizontal.AddTransition(
                (s) => (s as MoveObjectsState).Finished && targetLeft.Achieved && targetRight.Achieved,
                movingVertical);
            movingVertical.AddTransition(
                (s) => (s as MoveObjectsState).Finished && targetUp.Achieved && targetDown.Achieved,
                movingBoth,
                () => MovementHelper.SetActive(false));
            movingBoth.AddTransition(
                (s) => (s as MoveObjectsState).Finished,
                teleport,
                 () => { MovingLevelCamera.SetActive(false); ShootingLevelCamera.SetActive(true); });

            levelMachine = new StateMachine(waitForTime, BrainDebugBlock);
        }

        private void Update() {
            BrainDebugBlock.Change(TimeID, Time.time.ToString("F2"));
            levelMachine?.Tick();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.SerializationSystem;
using Wokarol.SpawnSystem;
using Wokarol.StateSystem;

namespace Wokarol.LevelBrains
{
    public class InfiniteArena_Brain : MonoBehaviour
    {
        StateMachine levelMachine;
        public DebugBlock BrainDebugBlock { get; } = new DebugBlock("Level Brain");

        [SerializeField] WavePattern[] possiblePatterns = new WavePattern[0];
 
        [Header("Other")]
        [SerializeField] Spawner spawner = null;

        private void Awake() {

            // States
            var randomWaves = new SpawnWaveState[possiblePatterns.Length];
            for (int i = 0; i < randomWaves.Length; i++) {
                randomWaves[i] = new SpawnWaveState($"Wave {i} : {possiblePatterns[i].name}", spawner, possiblePatterns[i], 0.1f);
            }

            var randomWaveSelector = new RandomSelectorState("Getting new wave", randomWaves);

            foreach (var waveSate in randomWaves) {
                waveSate.AddTransition(() => waveSate.Finished && spawner.CurrentEnemyCount <= 0, randomWaveSelector);
            }

            levelMachine = new StateMachine(randomWaveSelector, BrainDebugBlock);
        }

        private void Start() {
            
        }

        private void Update() {
            levelMachine?.Tick();
        }
    } 
}

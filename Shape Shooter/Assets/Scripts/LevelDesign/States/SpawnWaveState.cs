using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.SpawnSystem;
using Wokarol.StateSystem;
namespace Wokarol.StateSystem
{
    public class SpawnWaveState : State, ICanBeFinished
    {
        readonly Spawner _spawner;
        readonly WavePattern _wavePattern;
        readonly float _spawnInterval;

        float _spawnCountdown = 0;
        int _currentWaveSpawnIndex = 0;
        bool _finished = false;

        public SpawnWaveState(Spawner spawner, WavePattern wavePattern, float spawnInterval) {
            _spawner = spawner;
            _wavePattern = wavePattern;
            _spawnInterval = spawnInterval;
        }

        public bool Finished => _finished;
        public override bool CanTransitionToSelf => false;


        protected override void EnterProcess(StateMachine stateMachine) {
            _currentWaveSpawnIndex = 0;
            _spawnCountdown = 0;
            _finished = false;

        }
        protected override void ExitProcess(StateMachine stateMachine) {
            
        }
        protected override State Process() {
            if (!_finished) {
                _spawnCountdown -= Time.deltaTime;

                while (_spawnCountdown < 0 && _currentWaveSpawnIndex < _wavePattern.SpawnPoints.Count) {
                    _spawner.Spawn(_wavePattern.SpawnPoints[_currentWaveSpawnIndex].Spawnable, _wavePattern.SpawnPoints[_currentWaveSpawnIndex].Point);
                    _currentWaveSpawnIndex++;
                    _spawnCountdown += _spawnInterval;
                }

                if (_currentWaveSpawnIndex == _wavePattern.SpawnPoints.Count) {
                    _finished = true;
                } 
            }
            return null;
        }
    }
}

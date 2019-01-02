using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public class StateMachine
    {
        private const string StateID = "Machine_State";
        private const string DividerID = "Machine_Divider";
        State _initialState;
        State _currentState;
        public DebugBlock DebugBlock { get; }

        private StateMachine() { }
        public StateMachine(State initialState, DebugBlock debugBlock) {
            _initialState = initialState;
            DebugBlock = debugBlock;
            DebugBlock.Define("State", StateID);
            DebugBlock.Define("", DividerID);
            ChangeState(_initialState);
        }

        public void ChangeState(State nextState) {
            _currentState?.Exit(this);
            _currentState = nextState;
            if (_currentState != null) {
                _currentState.Enter(this);
                DebugBlock.Change(StateID, _currentState.ToString());
            }
        }

        public void Tick() {
            var nextState = _currentState?.Tick();
            if (nextState != null) {
                ChangeState(nextState);
            }
        }

        internal void Restart() {
            ChangeState(_initialState);
        }
    }
}

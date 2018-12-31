using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public class StateMachine
    {
        State _initialState;
        State _currentState;
        public StateMachine(State initialState) {
            _initialState = _currentState = initialState;
            _currentState?.Enter(this);
        }

        public void ChangeState(State nextState) {
            _currentState?.Exit();
            _currentState = nextState;
            _currentState?.Enter(this);
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

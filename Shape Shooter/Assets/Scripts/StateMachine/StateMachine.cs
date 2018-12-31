using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public class StateMachine
    {
        State currentState;
        public StateMachine(State initialState) {
            currentState = initialState;
            currentState?.Enter(this);
        }

        public void ChangeState(State nextState) {
            currentState?.Exit();
            currentState = nextState;
            currentState?.Enter(this);
        }

        public void Tick() {
            var nextState = currentState?.Tick();
            if (nextState != null) {
                ChangeState(nextState);
            }
        }
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public class StateMachine
    {
        #region DebugBlock
#if UNITY_EDITOR
        private const string NoTickWarningID = "StateMachine_Warning_NoTick";
        private const string StateID = "Machine_State";
        private const string DividerID = "Machine_Divider";

        public DebugBlock DebugBlock { get; }
#endif
        #endregion

        State _initialState;
        State _currentState;

        private StateMachine() { }
        public StateMachine(State initialState, DebugBlock debugBlock) {
            _initialState = initialState;
            #region DebugBlock
#if UNITY_EDITOR
            DebugBlock = debugBlock;
            DebugBlock.Define("Warning", NoTickWarningID, "StateMachine hasn't been ticked through!!!");
            DebugBlock.Define("State", StateID);
            DebugBlock.Define("", DividerID);
#endif
            #endregion
            ChangeState(_initialState);
        }

        public void ChangeState(State nextState) {
            if (nextState == _currentState && !_currentState.CanTransitionToSelf) // Makes sure that transition will not occur if current state can't transition to them selves
                return;
            _currentState?.Exit(this);
            _currentState = nextState;
            if (nextState != null) {
                nextState.Enter(this);
                #region DebugBlock
#if UNITY_EDITOR
                DebugBlock.Change(StateID, nextState.ToString());
#endif
                #endregion
            }
        }

        public void Tick() {
            var nextState = _currentState?.Tick();
            if (nextState != null) {
                ChangeState(nextState);
            }
            #region DebugBlock
#if UNITY_EDITOR
            DebugBlock.Undefine(NoTickWarningID);
#endif
            #endregion
        }

        /// <summary>
        /// Changes state of StateMachine to intial state
        /// </summary>
        internal void Restart() {
            ChangeState(_initialState);
        }
    }
}

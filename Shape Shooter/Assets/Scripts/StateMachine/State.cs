﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public abstract class State
    {
        public struct Transition
        {
            public State NextState { get; }
            public Func<bool> Evaluator { get; }

            public Transition(Func<bool> evaluator, State nextState) {
                Evaluator = evaluator;
                NextState = nextState;
            }
        }
        public class CantSetToNullException : Exception
        {
            public override string Message => "Transitions can't be set to null";
            public override string StackTrace {
                get {
                    string baseTrace = base.StackTrace;
                    int firstLineEnd = baseTrace.IndexOf('\n');
                    return baseTrace.Substring(firstLineEnd);
                }
            }
        }

        private List<Transition> transitions = new List<Transition>();
        public List<Transition> Transitions {
            get => transitions;
            set => transitions = value ?? throw new CantSetToNullException();
        }
        public abstract bool CanTransitionToSelf { get; }

        public abstract void Enter(StateMachine stateMachine);
        public abstract void Exit(StateMachine stateMachine);

        public State Tick() {
            State processedState = Process();
            if (processedState != null) return processedState;
            State state = CheckTransitions();
            return state;
        }
        protected abstract State Process();

        private State CheckTransitions() {
            foreach (var transition in Transitions) {
                if (transition.Evaluator()) {
                    return transition.NextState;
                }
            }
            return null;
        }

        /// <summary>
        /// Adds transition to Transitions list (works exactly like adding transition manually)
        /// </summary>
        /// <param name="evaluator">Transition is active if this function returns true</param>
        /// <param name="nextState">State to transition to</param>
        public void AddTransition(Func<bool> evaluator, State nextState) {
            Transitions.Add(new Transition(evaluator, nextState));
        }
    }
}
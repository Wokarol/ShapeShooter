using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public abstract class State
    {
        List<Transition> transitions = new List<Transition>();

        public abstract void Enter(StateMachine stateMachine);
        public abstract void Exit();

        public State Tick() {
            State processedState = Process();
            if (processedState != null) return processedState;
            State state = CheckTransitions();
            return state;
        }
        protected abstract State Process();

        private State CheckTransitions() {
            foreach (var transition in transitions) {
                if (transition.Evaluator()) {
                    return transition.NextState;
                }
            }
            return null;
        }

        public void AddTransition(Func<bool> evaluator, State nextState) {
            transitions.Add(new Transition(evaluator, nextState));
        }
    }

    struct Transition
    {
        public Func<bool> Evaluator;
        public State NextState;

        public Transition(Func<bool> evaluator, State nextState) {
            Evaluator = evaluator;
            NextState = nextState;
        }
    }
}

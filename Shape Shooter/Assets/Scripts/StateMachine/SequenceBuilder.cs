using System;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.StateSystem
{
    public class SequenceBuilder
    {
        public class SequneceState
        {
            public SequneceState(State state, Func<bool> evaluator, Action onTransition) {
                State = state;
                Evaluator = evaluator;
                OnTransition = onTransition;
            }

            public State State { get; }
            public Func<bool> Evaluator { get; }
            public Action OnTransition { get; }
        }
        public List<SequneceState> States { get; } = new List<SequneceState>();

        public State Compose() {
            for (int i = 0; i < States.Count - 1; i++) {
                if (States[i].Evaluator != null)
                    States[i].State.AddTransition(States[i].Evaluator, States[i + 1].State, States[i].OnTransition);
            }
            return States[0].State;
        }

        public void Add(State state, Func<bool> evaluator = null, Action onTransition = null) {
            States.Add(new SequneceState(state, evaluator, onTransition));
        }
    }
}
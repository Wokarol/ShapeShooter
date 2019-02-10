using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.MessageSystem
{
    public class Messenger
    {
        public static Messenger Default { get; } = new Messenger();

        private Dictionary<Type, List<Delegate>> _messageMappings = new Dictionary<Type, List<Delegate>>();

        public void RegisterSubscriberTo<T>(Action<T> action) {
            var type = typeof(T);
            if (!_messageMappings.ContainsKey(type)) {
                _messageMappings.Add(type, new List<Delegate>());
            }

            _messageMappings[type].Add(action);
        }

        public void SendMessage<T>(T message) {
            var type = typeof(T);
            if (!_messageMappings.ContainsKey(type)) return;

            foreach (var listener in _messageMappings[type]) {
                listener.DynamicInvoke(message);
            }
        }

        public void UnRegisterAllSubscribersForObjects(object target) {
            var listsOfActions = _messageMappings.Values;

            foreach (var listOfActions in listsOfActions) {
                listOfActions.RemoveAll(x => x.Target == target);
            }
        }
    } 
}

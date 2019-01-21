using System;
using UnityEngine;

namespace Wokarol.MenuSystem
{
    public abstract class MenuTransitionController : MonoBehaviour
    {
        public bool InMotion { get; protected set; } = false;
        public abstract void CallTransition(Action callback);
    }
}
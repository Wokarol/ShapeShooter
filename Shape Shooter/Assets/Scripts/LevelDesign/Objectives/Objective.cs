using UnityEngine;

namespace Wokarol.LevelDesign
{
    public abstract class Objective : MonoBehaviour
    {
        public UnityEngine.Events.UnityEvent OnAchieved;
        public abstract bool Achieved { get; }
    }
}
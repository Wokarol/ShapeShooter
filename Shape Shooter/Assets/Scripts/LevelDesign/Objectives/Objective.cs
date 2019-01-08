using UnityEngine;

namespace Wokarol.LevelDesign
{
    public abstract class Objective : MonoBehaviour
    {
        public abstract bool Achieved { get; }
    }
}
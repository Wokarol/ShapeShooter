using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

namespace Wokarol.SpawnSystem
{
    [CreateAssetMenu]
    public class SpawnableDefinition : ScriptableObject
    {
        [SerializeField] PoolObject spawnableObject = null;
        [Header("Debug")]
        [SerializeField] string debugName = "";
        [SerializeField] Color color = Color.white;
        [SerializeField] Shapes shape = Shapes.Circle;

        public PoolObject SpawnableObject => spawnableObject;
        public string DebugName => debugName;
        public Color Color => color;
        public Shapes Shape => shape;

        public enum Shapes { Circle, Square}
    } 
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

namespace Wokarol.SpawnSystem
{
    [CreateAssetMenu]
    public class SpawnableDefinition : ScriptableObject
    {
        [SerializeField] string debugName;
        [SerializeField] PoolObject spawnableObject;

        // TODO: Add giuzmos color and shape for debug purposes
    } 
}

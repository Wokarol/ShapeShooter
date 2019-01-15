using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.SpawnSystem
{
    public class Spawner : MonoBehaviour
    {
        [Header("Preview")]
        [SerializeField] WavePattern wavePattern = null;
        public WavePattern WavePattern => wavePattern;
    } 
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol
{
    [Serializable]
    public struct MinMaxFloat
    {
        [SerializeField] float min;
        [SerializeField] float max;

        public float Value { get {
                return UnityEngine.Random.Range(min, max);
            } }

        public MinMaxFloat(float min, float max) {
            this.min = min;
            this.max = max;

            if(min > max) {
                this.min = max;
                this.max = min;
            }
        }
    } 
}

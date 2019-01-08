using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.LevelDesign
{
    public class MovePoint : Objective
    {
        private bool achieved = false;
        public override bool Achieved => achieved;

        private void OnTriggerEnter2D(Collider2D collision) {
            achieved = true;
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.LevelDesign
{
    public class MovePoint : Objective
    {
        [SerializeField] bool deactivateOnTouch = false;

        public override bool Achieved => achieved;
        private bool achieved = false;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (!achieved) {
                achieved = true;
                OnAchieved.Invoke();
                if (deactivateOnTouch) gameObject.SetActive(false); 
            }
        }
    }
}

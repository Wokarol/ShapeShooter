using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.PoolSystem
{
    public abstract class PoolObject : MonoBehaviour, IDestroyable
    {
        public event Action<PoolObject> OnDestroyed = null;
        public event Action OnDestroy = null;

        public void Recreate(Vector3 pos, Quaternion rot)
        {
            transform.SetPositionAndRotation(pos, rot);
            Recreate();
        }

        public virtual void Destroy()
        {
            OnDestroy?.Invoke();
            OnDestroyed?.Invoke(this);
        }

        public abstract void Recreate();
        public abstract void Deactivate();
        public abstract void Activate();
    }
}

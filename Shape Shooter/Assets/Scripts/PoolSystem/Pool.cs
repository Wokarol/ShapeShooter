using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.PoolSystem
{
    public class Pool : MonoBehaviour
    {

        [SerializeField] PoolObject prefab = null;
        [SerializeField] int initialSize = 10;

        Stack<PoolObject> poolObjects = new Stack<PoolObject>();
        int genNumber = 0;

        void Awake()
        {
            PopulatePool(initialSize);
        }

        private void PopulatePool(int size)
        {
            for (int i = 0; i < size; i++) {
                var pObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                pObj.gameObject.name = $"{prefab.name}_{genNumber}";
                pObj.Deactivate();
                poolObjects.Push(pObj);
                pObj.OnDestroyed += ReturnToPool;
                genNumber++;
            }
        }

        public GameObject Get()
        {
            return Get(Vector3.zero, Quaternion.identity);
        }

        public GameObject Get(Vector3 pos, Quaternion rot)
        {
            if(poolObjects.Count <= 0) {
                PopulatePool(initialSize);
            }
            var pObj = poolObjects.Pop();
            pObj.Recreate(pos, rot);
            return pObj.gameObject;
        }



        void ReturnToPool (PoolObject poolObject)
        {
            poolObject.Deactivate();
            poolObjects.Push(poolObject);
        }
    }
}

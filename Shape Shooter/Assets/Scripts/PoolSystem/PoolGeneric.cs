using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Wokarol.PoolSystem
{
    public class PoolGeneric<PoolableObject> : MonoBehaviour where PoolableObject : PoolObject
    {
        [SerializeField] PoolableObject prefab = null;
        [SerializeField] int initialSize = 10;
        [SerializeField] bool populateOnAwake = true;

        Queue<PoolableObject> poolObjects = new Queue<PoolableObject>();
        int genNumber = 0;

        void Awake() {
            if(populateOnAwake)
            PopulatePool(initialSize);
        }

        public void PopulatePool(int size) {
            for (int i = 0; i < size; i++) {
                var pObj = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                pObj.gameObject.name = $"{prefab.name}_{genNumber}";
                pObj.Deactivate();
                poolObjects.Enqueue(pObj);
                pObj.OnDestroyed += ReturnToPool;
                ConfigureCreatedObject(pObj);
                genNumber++;
            }
        }

        public virtual void ConfigureCreatedObject(PoolableObject poolObject) {

        }

        public PoolableObject Get() {
            return Get(Vector3.zero, Quaternion.identity);
        }

        public PoolableObject Get(Vector3 pos, Quaternion rot) {
            if (poolObjects.Count <= 0) {
                PopulatePool(initialSize);
            }
            var pObj = poolObjects.Dequeue();
            pObj.Recreate(pos, rot);
            return pObj;
        }



        void ReturnToPool(PoolObject poolObject) {
            poolObject.Deactivate();
            if (poolObject as PoolableObject == null) Debug.LogError("...");
            poolObjects.Enqueue(poolObject as PoolableObject);
        }
    } 
}

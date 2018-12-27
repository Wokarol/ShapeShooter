using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Wokarol.NavMesh
{
    [RequireComponent(typeof(NavMeshSurface))]
    public class NavMesh2DBaker : MonoBehaviour
    {
        const int collidersDepth = 50;
        Vector3 Box = new Vector3(1, 1, 1);

        [SerializeField] Vector2 groundSize = new Vector2(40, 40);

        public void Bake()
        {
            var createdObjects = new List<GameObject>();

            // Ground creation
            var ground = new GameObject("NavMeshGround");
            ground.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
            var groundCollider = ground.AddComponent<BoxCollider>();
            groundCollider.size = groundSize;
            createdObjects.Add(ground);

            ConvertColliders(createdObjects);
            // Baking NavMesh
            var surfaces = GetComponents<NavMeshSurface>();
            foreach (var surface in surfaces) {
                if (surface.useGeometry != NavMeshCollectGeometry.PhysicsColliders) {
                    surface.useGeometry = NavMeshCollectGeometry.PhysicsColliders;
                    Debug.Log("NavMesh should be set to use Physics Colliders, I've corrected it for you");
                }
                surface.BuildNavMesh();
            }
            // Clearing after process
            foreach (var createdObject in createdObjects) {
                DestroyImmediate(createdObject);
            }

            Debug.Log("NavMesh 2D Baking complete :D");
        }

        private void ConvertColliders(List<GameObject> createdObjects)
        {
            // Converting Box colliders
            Vector3 colliderZSize = Vector3.forward * collidersDepth;
            var boxColliders2D = FindObjectsOfType<BoxCollider2D>();
            foreach (var collider in boxColliders2D) {
                if (collider.GetComponent<NavMeshObstacle>() != null) continue;
                var col = GetObjectCopy(collider.gameObject).AddComponent<BoxCollider>();
                col.size = (Vector3)collider.size + colliderZSize;
                col.center = (Vector3)collider.offset;
            }

            // Converting CircleColliders
            var circleCollider2D = FindObjectsOfType<CircleCollider2D>();
            foreach (var collider in circleCollider2D) {
                if (Isexcluded(collider.gameObject)) continue;
                var col = GetObjectCopy(collider.gameObject).AddComponent<CapsuleCollider>();
                col.radius = collider.radius;
                col.center = (Vector3)collider.offset;
                col.height = collidersDepth;
                col.direction = 2;
            }

            // Converting CapsuleColliders
            var capsuleColiders2D = FindObjectsOfType<CapsuleCollider2D>();
            foreach (var collider in capsuleColiders2D) {
                if (Isexcluded(collider.gameObject)) continue;
                var obj = GetObjectCopy(collider.gameObject);
                if (collider.direction == CapsuleDirection2D.Vertical) {
                    var correctedSize = collider.size;
                    correctedSize.y = Mathf.Clamp(correctedSize.y, correctedSize.x, float.MaxValue);

                    // Box collider
                    var boxCol = obj.AddComponent<BoxCollider>();
                    boxCol.center = (Vector3)collider.offset;
                    boxCol.size = new Vector3(correctedSize.x, correctedSize.y - correctedSize.x, collidersDepth);

                    // Top capsule collider
                    var topCapsCol = obj.AddComponent<CapsuleCollider>();
                    topCapsCol.center = new Vector3(collider.offset.x, collider.offset.y - boxCol.size.y / 2);
                    topCapsCol.radius = boxCol.size.x / 2;
                    topCapsCol.height = collidersDepth;
                    topCapsCol.direction = 2;

                    // Top capsule collider
                    var bottomCapsCol = obj.AddComponent<CapsuleCollider>();
                    bottomCapsCol.center = new Vector3(collider.offset.x, collider.offset.y + boxCol.size.y / 2);
                    bottomCapsCol.radius = boxCol.size.x / 2;
                    bottomCapsCol.height = collidersDepth;
                    bottomCapsCol.direction = 2;
                }

                if (collider.direction == CapsuleDirection2D.Horizontal) {
                    var correctedSize = collider.size;
                    correctedSize.x = Mathf.Clamp(correctedSize.x, correctedSize.y, float.MaxValue);

                    // Box collider
                    var boxCol = obj.AddComponent<BoxCollider>();
                    boxCol.center = (Vector3)collider.offset;
                    boxCol.size = new Vector3(correctedSize.x - correctedSize.y, correctedSize.y, collidersDepth);

                    // Top capsule collider
                    var topCapsCol = obj.AddComponent<CapsuleCollider>();
                    topCapsCol.center = new Vector3(collider.offset.x - boxCol.size.x / 2, collider.offset.y);
                    topCapsCol.radius = boxCol.size.y / 2;
                    topCapsCol.height = collidersDepth;
                    topCapsCol.direction = 2;

                    // Bottom capsule collider
                    var bottomCapsCol = obj.AddComponent<CapsuleCollider>();
                    bottomCapsCol.center = new Vector3(collider.offset.x + boxCol.size.x / 2, collider.offset.y);
                    bottomCapsCol.radius = boxCol.size.y / 2;
                    bottomCapsCol.height = collidersDepth;
                    bottomCapsCol.direction = 2;
                }
            }


            GameObject GetObjectCopy(GameObject original)
            {
                var obj = new GameObject($"{original.name}_NMClone");
                obj.transform.parent = original.transform;
                var newPos = original.transform.position;
                newPos.z = 0;
                obj.transform.SetPositionAndRotation(newPos, original.transform.rotation);
                obj.transform.localScale = Box;
                obj.layer = original.layer;
                createdObjects.Add(obj);
                return obj;
            }
            bool Isexcluded(GameObject obj)
            {
                return obj.GetComponent<NavMeshObstacle>() != null;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.DrawWireCube(transform.position, (Vector3)groundSize + Vector3.forward * 5);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    const float targetRefreshInterval = 0.2f;

    [SerializeField] LayerMask targetLayer = 0;
    [SerializeField] float distance = 10;
    [SerializeField] float stoppingDistance = 0.2f;

    private void OnValidate() {
        if (distance < 0) distance *= -1;
    }

    NavMeshAgent agent = null;
    Transform target;

    float countdown;

    private void Update() {
        countdown -= Time.deltaTime;
        if (countdown < 0) {
            countdown += targetRefreshInterval;

            if (target == null) {
                var collider = Physics2D.OverlapCircle(transform.position, distance, targetLayer);
                target = collider.transform;
            }

            agent.stoppingDistance = stoppingDistance;
            if (target)
                agent.SetDestination(target.position);
        }
    }


    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable() {
        countdown = 0;
    }

    private void OnDisable() {
        target = null;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.DrawWireSphere(transform.position, distance);
        if (target) {
            Gizmos.color = Color.red * Color.grey;
            Gizmos.DrawWireSphere(target.position, 0.2f);
        }
        if (agent) {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(agent.destination, 0.3f);
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}

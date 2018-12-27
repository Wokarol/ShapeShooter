using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.PoolSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Pool enemyPool = null;

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4)) {
            enemyPool.Get(new Vector3(Random.Range(-15f, 15), Random.Range(-8, 8), 0), Quaternion.identity);
        }
    }
}

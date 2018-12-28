using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Wokarol.HealthSystem;
using Wokarol.PoolSystem;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPool enemyPool = null;

    List<Enemy> enemiesAlive = new List<Enemy>();

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton4)) {
            Enemy enemy = enemyPool.Get(new Vector3(Random.Range(-15f, 15), Random.Range(-8, 8), 0), Quaternion.identity);
            OnEnemyCreated(enemy);
            enemy.OnEnemyDestroyed += OnEnemyDestroyed;
        }
    }

    void OnEnemyCreated(Enemy enemy) {
        enemiesAlive.Add(enemy);
    }

    void OnEnemyDestroyed(Enemy enemy) {
        enemiesAlive.Remove(enemy);
        enemy.OnEnemyDestroyed -= OnEnemyDestroyed;
    }

    public void KillAllEnemies() {
        while(enemiesAlive.Count > 0) {
            var enemyHealth = enemiesAlive[0].GetComponent<IHasHealth>();
            if (enemyHealth != null) enemyHealth.SetHealth(-1);
            else enemiesAlive[0].Destroy();
        }
    }
}

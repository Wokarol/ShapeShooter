using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct EnemyDestroyedEvent
{
    public readonly int PointsCount;
    public readonly Enemy DestroyedEnemy;

    public EnemyDestroyedEvent(int pointsCount, Enemy destroyedEnemy) {
        PointsCount = pointsCount;
        DestroyedEnemy = destroyedEnemy;
    }
}

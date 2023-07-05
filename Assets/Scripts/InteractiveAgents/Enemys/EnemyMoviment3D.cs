using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMoviment3D : ObstacleMoviment
{
    private bool alreadyCol;
    protected override void MoveEnemy()
    {
        base.MoveEnemy();
        if (enemyMovment != Vector3.zero)
        {
            enemyMaster.CallEventMovimentEnemy(enemyMovment);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyCol && (!enemy.ignoreWall && other.CompareTag(GameSettings.Instance.wallTag) || (other.CompareTag(GameSettings.Instance.enemyTag) && !enemy.ignoreEnemys)))
        {
            alreadyCol = true;
            if (faceDirection.x != 0)
                faceDirection.x *= -1;
            if (faceDirection.y != 0)
                faceDirection.y *= -1;
            if (faceDirection.z != 0)
                faceDirection.z *= -1;
            if(enemy.canFlip)
                enemyMaster.CallEventFlipEnemy(faceDirection);
        }
    }

    private void LateUpdate()
    {
        alreadyCol = false;
    }
}

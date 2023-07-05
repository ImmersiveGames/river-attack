using UnityEngine;
using Utils.Variables;

[RequireComponent(typeof(EnemyMoviment2D))]
public class EnemyMoveByApproach2D : ObstacleMoveByApproach
{

    protected override void ApproachPlayer()
    {
        playerDistance = GetPlayerDistance();
        if (randomPlayerDistanceNear.maxValue > 0)
            playerDistance = RangePatrol;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, playerDistance, LayerMask.GetMask("Player"));
        if (collider != null)
            obstacleMoviment.canMove = true;
    }
}
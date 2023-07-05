using UnityEngine;
using Utils.Variables;

public class EnemyMoveByApproach3D : ObstacleMoveByApproach
{
    protected override void ApproachPlayer()
    {       
        playerDistance = GetPlayerDistance();
        if (randomPlayerDistanceNear.maxValue > 0)
            playerDistance = RangePatrol;
        Collider[] collider = Physics.OverlapSphere(transform.position, playerDistance, LayerMask.GetMask("Player"));
        if (collider.Length > 0)
            obstacleMoviment.canMove = true;
    }
}

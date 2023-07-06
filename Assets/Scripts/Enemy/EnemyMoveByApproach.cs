using UnityEngine;

public class EnemyMoveByApproach : ObstacleMoveByApproach
{
    protected override void ApproachPlayer()
    {       
        playerDistance = GetPlayerDistance();
        if (randomPlayerDistanceNear.maxValue > 0)
            playerDistance = RangePatrol;
        Collider[] collider = Physics.OverlapSphere(transform.position, playerDistance, LayerMask.GetMask(GameSettings.Instance.playerLayer));
        if (collider.Length > 0 && radiusPlayerProximity > 0)
            obstacleMoviment.canMove = true;
    }
}

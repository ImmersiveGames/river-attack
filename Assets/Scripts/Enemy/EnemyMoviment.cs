using UnityEngine;

public class EnemyMoviment : ObstacleMoviment
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
        if (!alreadyCol && !enemyMaster.ignoreWall && other.CompareTag(GameSettings.Instance.wallTag))
        {
            FlipMe();
        } 
        //if(!alreadyCol && other.CompareTag(GameSettings.Instance.enemyTag) && !enemyMaster.ignoreEnemys){
        //    Debug.Log("ENEMY FLIP ME");
        //    FlipMe();
        //}
    }

    private void FlipMe()
    {
        alreadyCol = true;
        if (faceDirection.x != 0)
            faceDirection.x *= -1;
        if (faceDirection.y != 0)
            faceDirection.y *= -1;
        if (faceDirection.z != 0)
            faceDirection.z *= -1;
        if (enemy.canFlip)
            enemyMaster.CallEventFlipEnemy(faceDirection);
    }

    private void LateUpdate()
    {
        alreadyCol = false;
    }
}

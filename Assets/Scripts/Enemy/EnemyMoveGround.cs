using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveGround : ObstacleMove
{
    private EnemyMaster enemyMaster;
    private EnemyShoot enemyshoot;
    private GamePlayMaster gamePlay;
    private bool alreadyCol;

    private void OnEnable()
    {
        SetInitialReferences();
        direction = (direction == Vector3.zero) ? Vector3.forward : direction;
        canMove = true;
        gamePlay.EventResetEnemys -= ResetMovement;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        enemyMaster = GetComponent<EnemyMaster>();
        gamePlay = GamePlayMaster.Instance;
        enemyshoot = GetComponent<EnemyShoot>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyCol && !enemyMaster.ignoreWall && other.CompareTag(GameSettings.Instance.wallTag))
        {
            alreadyCol = true;
            MoveStop();
            
            if (enemyshoot != null && enemyshoot.holdShoot == true) enemyshoot.holdShoot = false;
        }
    }

    private void Update()
    {
        Move(direction);
    }

    public override bool ShouldMove()
    {
        bool should = base.ShouldMove();
        if (!should || !GamePlayMaster.Instance.ShouldBePlayingGame || enemyMaster.IsDestroyed)
            should = false;
        return should;
    }

    private void ResetMovement()
    {
        alreadyCol = false;
        if(enemyshoot != null)
        {
            enemyshoot.holdShoot = true;
        }
    }

    private void LateUpdate()
    {
        alreadyCol = false;
    }

    private void OnDisable()
    {
        gamePlay.EventResetEnemys -= ResetMovement;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : ObstacleColliders
{
    protected Collider[] mycol;

    protected override void OnEnable()
    {
        base.OnEnable();
        if (enemyMaster.enemy.canRespawn)
            gamePlay.EventResetEnemys += ColliderOn;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        mycol = GetComponentsInChildren<Collider>();
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        if ((collision.transform.root.CompareTag(gameSettings.playerTag) || collision.transform.root.CompareTag(gameSettings.shootTag)) && enemyMaster.enemy.canDestruct)
        {
            HitThis(collision);
        }
    }

    public override void HitThis(Collider collision)
    {
        enemyMaster.IsDestroyed = true;
        PlayerMaster playerMaster = WhoHit(collision);
        ColliderOff();
        // Quem desativa o rander é o animation de explosão
        enemyMaster.CallEventDestroyEnemy(playerMaster);
        ShouldSavePoint();
        playerMaster.CallEventPlayerHit();
        //ShouldCompleteMission();
    }


    protected void ColliderOff()
    {
        mycol = GetComponentsInChildren<Collider>();
        int length = mycol.Length;
        for (int i = 0; i < length; i++)
        {
            mycol[i].enabled = false;
        }
    }
    protected void ColliderOn()
    {
        mycol = GetComponentsInChildren<Collider>();
        if (mycol != null)
        {
            int length = mycol.Length;
            for (int i = 0; i < length; i++)
            {
                mycol[i].enabled = true;
            }
        }

    }
    private void OnDisable()
    {
        if (enemyMaster.enemy.canRespawn)
            gamePlay.EventResetEnemys -= ColliderOn;
    }
    private void OnDestroy()
    {
        if (enemyMaster.enemy.canRespawn)
            gamePlay.EventResetEnemys -= ColliderOn;
    }
}

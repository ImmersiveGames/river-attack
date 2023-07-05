using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyCollider3D : ObstacleColliders
{
    protected Collider mycol;

    protected override void OnEnable()
    {
        base.OnEnable();
        if(enemyMaster.enemy.canRespawn)
        gamePlay.EventResetEnemys += ColliderOn;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        mycol = GetComponent<Collider>();
    }

    protected virtual void OnTriggerEnter(Collider collision)
    {
        if ((collision.gameObject.CompareTag(gameSettings.playerTag) || collision.gameObject.CompareTag(gameSettings.shootTag)) && enemyMaster.enemy.canDestruct)
        {
            enemyMaster.IsDestroyed = true;
            PlayerMaster playerMaster = WhoHit(collision);
            ColliderOff();
            // Quem desativa o rander é o animation de explosão
            enemyMaster.CallEventDestroyEnemy(playerMaster);
            ShouldSavePoint();
            ShouldCompleteMission();
        }
    }
    protected PlayerMaster WhoHit(Collider collision)
    {
        if (collision.CompareTag(gameSettings.playerTag))
        {
            collision.GetComponent<PlayerMaster>().AddHitList(enemyMaster.enemy);
            return collision.GetComponent<PlayerMaster>();
        }

        if (collision.CompareTag(gameSettings.shootTag))
        {
            collision.GetComponent<PlayerBullet>().OwnerShoot.AddHitList(enemyMaster.enemy);
            return collision.GetComponent<PlayerBullet>().OwnerShoot;
        }
        return null;
    }

    protected void ColliderOff()
    {
        mycol.enabled = false;
    }
    protected void ColliderOn()
    {
        mycol.enabled = true;
    }
    private void OnDisable()
    {
        gamePlay.EventResetEnemys -= ColliderOn;
    }
    private void OnDestroy()
    {
        gamePlay.EventResetEnemys -= ColliderOn;
    }
}

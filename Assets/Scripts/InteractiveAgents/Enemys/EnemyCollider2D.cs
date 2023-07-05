using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyCollider2D : ObstacleColliders
{
    protected Collider2D mycol;

    protected override void OnEnable()
    {
        base.OnEnable();
        gamePlay.EventResetEnemys += ColliderOn;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        mycol = GetComponent<Collider2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
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
    protected PlayerMaster WhoHit(Collider2D collision)
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
}

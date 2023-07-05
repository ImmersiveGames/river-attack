using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaCollider2D : EnemyCollider2D
{
    protected EffectAreaMaster effectAreaMaster;
    private EffectArea effectArea;

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        effectAreaMaster = GetComponent<EffectAreaMaster>();
        effectArea = (EffectArea)effectAreaMaster.enemy;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(gameSettings.shootTag) && effectArea.canDestruct)
        {
            effectAreaMaster.IsDestroyed = true;
            PlayerMaster playerMaster = WhoHit(collision);
            ColliderOff();
            // Quem desativa o rander é o animation de explosão
            enemyMaster.CallEventDestroyEnemy(playerMaster);
            ShouldSavePoint();
            ShouldCompleteMission();
            enemyMaster.CallEventDestroyEnemy();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(gameSettings.playerTag) || collision.GetComponent<PlayerMaster>())
        {
            Player player = collision.GetComponent<PlayerMaster>().playerSettings;
            effectArea.EffectAreaStart(player);
            effectAreaMaster.CallAreaEffectEvent();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaCollider3D : EnemyCollider3D
{
    protected EffectAreaMaster effectAreaMaster;
    private EffectArea effectArea;
    [SerializeField]
    private float timetoAcess;

    private float timer;

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        effectAreaMaster = GetComponent<EffectAreaMaster>();
        effectArea = (EffectArea)effectAreaMaster.enemy;
    }

    protected override void OnTriggerEnter(Collider collision)
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

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag(gameSettings.playerTag) || collision.GetComponent<PlayerMaster>())
        {
            Player player = collision.GetComponent<PlayerMaster>().playerSettings;
            if (timer <= 0)
            {
                effectArea.EffectAreaStart(player);
                effectAreaMaster.CallAreaEffectEvent();
                timer = timetoAcess;
            }
            timer -= Time.deltaTime;  
        }
    }
}

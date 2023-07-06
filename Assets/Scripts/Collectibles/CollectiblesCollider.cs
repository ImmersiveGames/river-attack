using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectiblesMaster))]
public class CollectiblesCollider : EnemyCollider
{
    protected CollectiblesMaster collectiblesMaster;
    private Collectibles collectibles;

    protected override void OnEnable()
    {
        base.OnEnable();
        collectiblesMaster.CollectibleEvent += DisableGraphic;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        collectiblesMaster = GetComponent<CollectiblesMaster>();
        collectibles = (Collectibles)collectiblesMaster.enemy;
    }

    private void DisableGraphic(PlayerMaster playerMaster)
    {
        MyUtils.Tools.ToggleChildrens(this.transform, false);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.root.CompareTag(gameSettings.playerTag))
        {
            CollectThis(collision);
        }
        if (collision.transform.root.CompareTag(gameSettings.shootTag) && collectiblesMaster.enemy.canDestruct)
        {
            HitThis(collision);
        }
    }
    public override void CollectThis(Collider collision)
    {
        PlayerMaster playerMaster = collision.GetComponentInParent<PlayerMaster>();
        if (playerMaster.CouldCollectItem(collectibles.maxCollectible.Value, collectibles))
        {
            ColliderOff();
            playerMaster.AddCollectiblesList(collectibles);
            PlayerPowerUp playerPowerup = collision.GetComponentInParent<PlayerPowerUp>();
            if (playerPowerup != null && collectiblesMaster.collectibles.PowerUp != null)
                playerPowerup.ActivatePowerup(collectiblesMaster.collectibles.PowerUp);
            enemyMaster.IsDestroyed = true;
            collectiblesMaster.CallCollectibleEvent(playerMaster);
            gamePlay.CallEventCollectable(collectibles);
        }
    }

    public override void HitThis(Collider collision)
    {
        enemyMaster.IsDestroyed = true;
        PlayerMaster playerMaster = WhoHit(collision);
        //if(playerMaster == null)
        //    playerMaster = WhoHit(collision);
        ColliderOff();
        playerMaster.AddHitList(collectibles);
        // Quem desativa o rander é o animation de explosão
        enemyMaster.CallEventDestroyEnemy(playerMaster);
        ShouldSavePoint();
        //ShouldCompleteMission();
    }

    private void OnDisable()
    {
        collectiblesMaster.CollectibleEvent -= DisableGraphic;
    }
}

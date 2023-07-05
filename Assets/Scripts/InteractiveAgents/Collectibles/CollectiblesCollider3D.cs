using System.Collections;
using System.Collections.Generic;
using Utils.Variables;
using UnityEngine;

[RequireComponent(typeof(CollectiblesMaster))]
public class CollectiblesCollider3D : EnemyCollider3D
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
        Utils.Tools.ToggleChildrens(this.transform, false);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(gameSettings.playerTag))
        {
            PlayerMaster playerMaster = collision.GetComponent<PlayerMaster>();
            if (playerMaster.CouldCollectItem(collectibles.maxCollectible.Value, collectibles))
            {
                ColliderOff();
                playerMaster.AddCollectiblesList(collectibles);
                PlayerPowerUp playerPowerup = collision.GetComponent<PlayerPowerUp>();
                if (playerPowerup != null && collectiblesMaster.collectibles.PowerUp != null)
                    playerPowerup.ActivatePowerup(collectiblesMaster.collectibles.PowerUp);
                enemyMaster.IsDestroyed = true;
                collectiblesMaster.CallCollectibleEvent(playerMaster);
                gamePlay.CallEventUICollectable();
            }
        }
        if (collision.CompareTag(gameSettings.shootTag) && collectiblesMaster.enemy.canDestruct)
        {
            enemyMaster.IsDestroyed = true;
            PlayerMaster playerMaster = WhoHit(collision);
            ColliderOff();
            playerMaster.AddHitList(collectibles);
            // Quem desativa o rander é o animation de explosão
            enemyMaster.CallEventDestroyEnemy(playerMaster);
            ShouldSavePoint();
            ShouldCompleteMission();
        }
    }

    private void OnDisable()
    {
        collectiblesMaster.CollectibleEvent -= DisableGraphic;
    }
}

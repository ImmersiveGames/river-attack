using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectiblesCollider2D : EnemyCollider2D
{
    protected CollectiblesMaster collectiblesMaster;
    private Collectibles collectibles;

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        collectiblesMaster = GetComponent<CollectiblesMaster>();
        collectibles = (Collectibles)collectiblesMaster.enemy;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(gameSettings.playerTag))
        {
            PlayerMaster playerMaster = collision.GetComponent<PlayerMaster>();
            if (playerMaster.CouldCollectItem(collectibles.maxCollectible.Value, collectibles))
            {
                ColliderOff();
                playerMaster.AddCollectiblesList(collectibles);
                PlayerPowerUp playerPowerup = collision.GetComponent<PlayerPowerUp>();
                //CollectiblesPowerUp collectiblesPowerUp = GetComponent<CollectiblesPowerUp>();
                if (playerPowerup != null && collectiblesMaster.collectibles.PowerUp != null)
                    playerPowerup.ActivatePowerup(collectiblesMaster.collectibles.PowerUp);
                enemyMaster.IsDestroyed = true;
                collectiblesMaster.CallCollectibleEvent(playerMaster);
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
}

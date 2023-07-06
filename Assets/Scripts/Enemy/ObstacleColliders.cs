using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
public abstract class ObstacleColliders : MonoBehaviour
{
    protected EnemyMaster enemyMaster;
    protected GamePlayMaster gamePlay;
    protected GameSettings gameSettings;
    protected GameManager gameManager;

    protected virtual void OnEnable()
    {
        SetInitialReferences();
    }

    protected virtual void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        gameSettings = GameSettings.Instance;
        gamePlay = GamePlayMaster.Instance;
        gameManager = GameManager.Instance;
    }

    protected virtual void OnTriggerEnter(Collider collision) { }
    public virtual void HitThis(Collider collision) { }

    //public virtual void HitThis(Collider collision, PlayerMaster playerM = null) { }

    public virtual void CollectThis(Collider collision) { }
    protected PlayerMaster WhoHit(Collider collision)
    {
        if (collision.transform.root.CompareTag(gameSettings.playerTag))
        {
            collision.transform.root.GetComponent<PlayerMaster>().AddHitList(enemyMaster.enemy);
            return collision.transform.root.GetComponent<PlayerMaster>();
        }

        if (collision.transform.root.CompareTag(gameSettings.shootTag))
        {
            if (collision.transform.root.GetComponent<PlayerBullet>())
            {
                collision.transform.root.GetComponent<PlayerBullet>().OwnerShoot.AddHitList(enemyMaster.enemy);
                return collision.transform.root.GetComponent<PlayerBullet>().OwnerShoot;
            }

            if (collision.transform.root.GetComponent<PlayerBombSet>())
            {
                collision.transform.root.GetComponent<PlayerBombSet>().OwnerShoot.AddHitList(enemyMaster.enemy);
                return collision.transform.root.GetComponent<PlayerBombSet>().OwnerShoot;
            }
            
        }
        return null;
    }

    //protected void ShouldCompleteMission()
    //{
    //    if (enemyMaster.goalLevel)
    //    {
    //        //TODO: Rervisitar aqui pra faze a animação de final de fase por enquanto so termina a fase
    //        gamePlay.levelComplete = true;
    //        if (gameManager.ActualLevel.beatGame == true)
    //            gameManager.isGameBeat = true;
    //        gamePlay.PausePlayGame();
    //        gamePlay.CallEventCompleteMission();
    //    }
    //}

    protected void ShouldSavePoint()
    {
        if (enemyMaster.enemy.isCheckInPoint)
            gamePlay.CallEventCheckPoint(transform.position);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
public abstract class ObstacleColliders : MonoBehaviour {

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

    protected void ShouldCompleteMission()
    {
        if (enemyMaster.goalLevel)
        {
            //TODO: Rervisitar aqui pra faze a animação de final de fase por enquanto so termina a fase
            gamePlay.levelComplete = true;
            if (gameManager.ActualLevel.beatGame == true)
                gameManager.isGameBeat = true;
            gamePlay.PausePlayGame();
            gamePlay.CallEventCompleteMission();
        }
    }

    protected void ShouldSavePoint()
    {
        if (enemyMaster.enemy.isCheckInPoint)
            gamePlay.CallEventCheckPoint(transform.position);
    }
}

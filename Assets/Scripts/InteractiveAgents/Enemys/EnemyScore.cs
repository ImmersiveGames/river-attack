using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

[RequireComponent(typeof(EnemyMaster))]
public class EnemyScore : MonoBehaviour {

    protected EnemyMaster enemyMaster;
    private EnemyDifficulty enemyDifficulties;
    protected virtual void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventPlayerDestroyEnemy += SetScore;
    }
    protected virtual void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        if (GetComponent<EnemyDifficulty>())
        {
            enemyDifficulties = GetComponent<EnemyDifficulty>();
        }
    }
    protected virtual void OnDisable()
    {
        enemyMaster.EventPlayerDestroyEnemy -= SetScore;
    }

    private void SetScore(PlayerMaster playerMaster)
    {
        float score = enemyMaster.enemy.enemyScore.Value;
        if (enemyDifficulties != null)
        {
            EnemySetDifficulty MyDifficulty = enemyDifficulties.MyDifficulty;
            if (MyDifficulty.scoreMod.Value > 0)
                score *= MyDifficulty.scoreMod.Value;
        }
        playerMaster.playerSettings.score.ApplyChange((int)(score));
    }
}

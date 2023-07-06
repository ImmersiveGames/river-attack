using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
public class EnemyScore : MonoBehaviour
{

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
        Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
    }
    protected virtual void OnDisable()
    {
        enemyMaster.EventPlayerDestroyEnemy -= SetScore;
    }

    private void SetScore(PlayerMaster playerMaster)
    {
        float score = (enemyMaster.enemy.fbScore != string.Empty) ?
            (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(enemyMaster.enemy.fbScore).LongValue
            : enemyMaster.enemy.enemyScore;
        enemyMaster.enemy.enemyScore.ConstantValue = (int)score;
        if (enemyDifficulties != null)
        {
            EnemySetDifficulty MyDifficulty = enemyDifficulties.MyDifficulty;
            if (MyDifficulty.scoreMod != null && MyDifficulty.scoreMod.Value > 0)
                score *= MyDifficulty.scoreMod.Value;
        }
        playerMaster.playerSettings.score.ApplyChange((int)(score));
        Log((int)playerMaster.playerSettings.score.Value);
    }

    private void Log(int score)
    {
        if (score > GamePlaySettings.Instance.totalScore)
            GamePlaySettings.Instance.totalScore = score;
    }
}

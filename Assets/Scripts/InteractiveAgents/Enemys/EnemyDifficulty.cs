using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
public class EnemyDifficulty : MonoBehaviour {

    protected EnemyMaster enemyMaster;
    [SerializeField]
    private DifficultyList enemysDifficulty;
    public EnemySetDifficulty MyDifficulty { get; private set; }

    private void Awake()
    {
        if (enemysDifficulty != null)
            MyDifficulty = enemysDifficulty.difficulties[0];
    }

    protected virtual void OnEnable()
    {
        SetInitialReferences();
        ChangeDifficulty();
        enemyMaster.EventDestroyEnemy += ChangeDifficulty;
    }

    protected virtual void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
    }

    protected virtual void ChangeDifficulty()
    {
        if (MyDifficulty.scoreMod != null && enemysDifficulty != null)
        {
            MyDifficulty = GetDifficult((int)(GamePlayMaster.Instance.HightScorePlayers()));
        }
    }
    public EnemySetDifficulty GetDifficult(string diffcultyName)
    {
        return enemysDifficulty.difficulties.Find(x => x.name == diffcultyName);
    }
    public EnemySetDifficulty GetDifficult(int score)
    {
        return enemysDifficulty.difficulties.Find(x => x.scoreToChange.Value >= (score));
    }
    public DifficultyList GetDifficultList()
    {
        return enemysDifficulty;
    }

    protected virtual void OnDisable()
    {
        enemyMaster.EventDestroyEnemy -= ChangeDifficulty;
    }
}

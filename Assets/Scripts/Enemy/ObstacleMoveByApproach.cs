using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyUtils.Variables;
public abstract class ObstacleMoveByApproach : MonoBehaviour
{

    [SerializeField]
    protected float radiusPlayerProximity;
    [SerializeField]
    [Range(.1f, 5)]
    protected float timeToCheck;
    [SerializeField]
    [MinMaxRange(0f, 10f)]
    protected FloatRanged randomPlayerDistanceNear;

    #region GizmoSettings
    [Header("Gizmo Settings")]
    [SerializeField]
    public DifficultyList enDifList;
    [HideInInspector]
    public string dificultType;
    [SerializeField]
    private Color gizmoColor = new Color(255, 0, 0, 150);
    #endregion
    protected ObstacleMoviment obstacleMoviment;
    private GamePlayMaster gamePlay;
    private EnemyDifficulty enemyDifficulties;
    protected float playerDistance;

    public float RadiusPlayerProximity { get { return radiusPlayerProximity; } set { radiusPlayerProximity = value; } }
    public float TimeToCheck { get { return timeToCheck; } set { timeToCheck = value; } }
    public FloatRanged RandomPlayerDistanceNear { get { return randomPlayerDistanceNear; } set { randomPlayerDistanceNear = value; } }

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventResetEnemys += ResetPatrol;
        InvokeRepeating("ApproachPlayer", 0, timeToCheck);
    }

    protected virtual void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        obstacleMoviment = GetComponent<ObstacleMoviment>();
        if (GetComponent<EnemyDifficulty>() != null)
        {
            enemyDifficulties = GetComponent<EnemyDifficulty>();
            if (radiusPlayerProximity > 0 || randomPlayerDistanceNear.maxValue > 0)
            {
                obstacleMoviment.canMove = false;
            }
        }
    }

    protected float GetPlayerDistance()
    {
        return (enemyDifficulties.MyDifficulty.multplyPlayerDistance > 0) ? radiusPlayerProximity * enemyDifficulties.MyDifficulty.multplyPlayerDistance : radiusPlayerProximity;
    }
    private void ResetPatrol()
    {
        if (radiusPlayerProximity > 0 || randomPlayerDistanceNear.maxValue > 0)
            obstacleMoviment.canMove = false;
    }
    protected float RangePatrol
    {
        get { return Random.Range(randomPlayerDistanceNear.minValue, randomPlayerDistanceNear.maxValue); }
    }

    protected virtual void ApproachPlayer()
    {
        //implementar no concreto
    }
    void OnDrawGizmosSelected()
    {
        if (radiusPlayerProximity > 0 || randomPlayerDistanceNear.maxValue > 0)
        {
            float circleDistance = radiusPlayerProximity;
            if (randomPlayerDistanceNear.maxValue > 0)
                circleDistance = randomPlayerDistanceNear.maxValue;
            if (GetComponent<EnemyDifficulty>() != null && radiusPlayerProximity != circleDistance)
            {
                EnemySetDifficulty MyDifficulty = GetComponent<EnemyDifficulty>().GetDifficult(dificultType);
                circleDistance *= MyDifficulty.multplyPlayerDistance;
            }
            Gizmos.color = gizmoColor;
            Gizmos.DrawWireSphere(transform.position, circleDistance);
        }
    }
    private void OnDisable()
    {
        gamePlay.EventResetEnemys -= ResetPatrol;
    }
}

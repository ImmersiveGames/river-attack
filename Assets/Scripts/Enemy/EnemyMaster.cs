using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{
    public Enemy enemy;
    public bool IsDestroyed;
    //public bool goalLevel;
    public bool ignoreWall;
    //public bool ignoreEnemys;
    protected Vector3 startPosition;
    public Vector3 EnemyStartPosition { get { return startPosition; } }

    protected GamePlayMaster gamePlay;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventDestroyEnemy;
    public event GeneralEventHandler EventChangeSkin;

    public delegate void MovimentEventHandler(Vector3 pos);
    public event MovimentEventHandler EventMovimentEnemy;
    public event MovimentEventHandler EventFlipEnemy;

    public delegate void EnemyEventHandler(PlayerMaster playerMaster);
    public event EnemyEventHandler EventPlayerDestroyEnemy;

    private void Awake()
    {
        gameObject.name = enemy.name;

        startPosition = transform.position;
        IsDestroyed = false;
    }

    protected virtual void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventResetEnemys += OnInitializeEnemy;
    }

    protected virtual void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        //Debug.Log(GameSettings.Instance.enemyTag);
        gameObject.tag = GameSettings.Instance.enemyTag;
        gameObject.layer = LayerMask.NameToLayer(GameSettings.Instance.enemyLayer);
    }

    protected virtual void OnInitializeEnemy()
    {
        if (!enemy.canRespawn && IsDestroyed)
            Destroy(this.gameObject, 0.1f);
        else
        {
            MyUtils.Tools.ToggleChildrens(this.transform, true);
            transform.position = startPosition;
            IsDestroyed = false;
        }

    }

    public void SetTagLayer(GameObject[] objects, string mytag, int mylayer)
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].tag = mytag;
            objects[i].layer = mylayer;
        }
    }

    protected virtual void OnDisable()
    {
        gamePlay.EventResetEnemys -= OnInitializeEnemy;
    }

    #region Calls
    public void CallEventDestroyEnemy()
    {
        if (EventDestroyEnemy != null)
        {
            EventDestroyEnemy();
        }
    }
    public void CallEventMovimentEnemy(Vector3 pos)
    {
        if (EventMovimentEnemy != null)
        {
            EventMovimentEnemy(pos);
        }
    }
    public void CallEventFlipEnemy(Vector3 pos)
    {
        if (EventFlipEnemy != null)
        {
            EventFlipEnemy(pos);
        }
    }
    public void CallEventDestroyEnemy(PlayerMaster playerMaster)
    {
        if (EventDestroyEnemy != null)
        {
            EventDestroyEnemy();
        }
        if (EventPlayerDestroyEnemy != null)
        {
            EventPlayerDestroyEnemy(playerMaster);
        }
    }

    public void CallEventChangeSkin()
    {
        if (EventChangeSkin != null)
        {
            EventChangeSkin();
        }
    }
    #endregion
}

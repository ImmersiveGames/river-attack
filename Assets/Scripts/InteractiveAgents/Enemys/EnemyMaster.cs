using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMaster : MonoBehaviour
{

    public Enemy enemy;
    public bool IsDestroyed;
    public bool goalLevel;
    protected Vector3 startPosition;
    public Vector3 EnemyStartPosition { get { return startPosition; } }

    protected GamePlayMaster gamePlay;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventDestroyEnemy;

    public delegate void MovimentEventHandler(Vector3 pos);
    public event MovimentEventHandler EventMovimentEnemy;
    public event MovimentEventHandler EventFlipEnemy;

    public delegate void EnemyEventHandler(PlayerMaster playerMaster);
    public event EnemyEventHandler EventPlayerDestroyEnemy;

    private void Awake()
    {
        gameObject.name = enemy.name;
        gameObject.tag = GameSettings.Instance.enemyTag;
        startPosition = transform.position;
        IsDestroyed = false;
    }

    protected virtual void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventResetEnemys += OnInitializeEnemy;
        gamePlay.EventResetEnemys += ToggleChildrens;
    }

    protected virtual void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
    }

    private void ToggleChildrens()
    {
        if(enemy.canRespawn)
            Utils.Tools.ToggleChildrens(this.transform, true);
    }

    protected virtual void OnInitializeEnemy()
    {
        if (!enemy.canRespawn && IsDestroyed)
            Destroy(this.gameObject,0.1f);
        else
        {
            if (GetComponent<SpriteRenderer>())
                GetComponent<SpriteRenderer>().enabled = true;
                IsDestroyed = false;
            gameObject.SetActive(true);
            transform.position = startPosition;
        }
    }

    protected virtual void OnDisable()
    {
        gamePlay.EventResetEnemys -= OnInitializeEnemy;
        gamePlay.EventResetEnemys -= ToggleChildrens;
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
    #endregion
}

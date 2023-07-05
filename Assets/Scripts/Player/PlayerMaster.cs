using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Variables;

public class PlayerMaster : MonoBehaviour
{
    [SerializeField]
    private bool isReady;
    [SerializeField]
    public Player playerSettings;
    [Header("Player Position")]
    [SerializeField]
    public bool useLevelPlayerSpawn;
    [SerializeField]
    private Vector3 startPlayerPosition;
    [SerializeField]
    private Vector3 startPlayerRotation;
    [SerializeField]
    private List<ItemResults> ListEnemysHit;
    [SerializeField]
    private List<ItemResults> ListCollectiblesCatch;
    [SerializeField]
    public int subWealth { get; private set; }

    //public Vector3 StartPlayerPosition { get { return startPlayerPosition}; private set { startPlayerPosition = value}; }
    //public List<ItemResults> GetPrevListEnemyHit { get { return ListEnemysHit; } }
    //public List<ItemResults> GetPrevListCollectiblesCatch { get { return ListCollectiblesCatch; } }
    public bool HasPlayerReady { get { return isReady; } }

    private GamePlayMaster gamePlay;

    #region Events
    public delegate void ControllerEventHandler(Vector3 dir);
    public event ControllerEventHandler EventControllerMovement;

    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventPlayerShoot;
    public event GeneralEventHandler EventPlayerDestroy;
    public event GeneralEventHandler EventPlayerReload;
    public event GeneralEventHandler EventPlayerAddLive;

    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler EventIncresceHealth;
    public event HealthEventHandler EventDecresceHealth;
    public event HealthEventHandler EventLowHealth;

    public delegate void SkinChangeEventHandler(ShopProductSkin skin);
    public event SkinChangeEventHandler EventChangeSkin;
    #endregion

    private void OnEnable()
    {
        gamePlay = GamePlayMaster.Instance;
        gamePlay.EventCheckPoint += UpdateSavePoint;
        EventPlayerReload += CleanPlayer;
    }
    private void Start()
    {
        CleanPlayer();
        startPlayerPosition = transform.position;
    }
    private void CleanPlayer(Vector3 dummy)
    {
        CleanPlayer();
    }
    private void CleanPlayer()
    {
        ListEnemysHit.Clear();
        ListCollectiblesCatch.Clear();
        subWealth = 0;
    }

    public bool ShouldPlayerBeReady
    {
        get
        {
            if (GamePlayMaster.Instance.ShouldBePlayingGame && isReady == true)
                return true;
            else
                return false;
        }
    }

    public void SetPlayerReady(bool ready)
    {
        isReady = ready;
    }

    public Vector3 GetStartPosition()
    {
        return startPlayerPosition;
    }

    public Quaternion GetStartRotation()
    {
        return Quaternion.Euler(startPlayerRotation);
    }

    public void UpdateSavePoint(Vector3 position)
    {
        startPlayerPosition = new Vector3(startPlayerPosition.x, transform.position.y, position.z);
        //UpdateListResults();
    }

    private void UpdateListResults()
    {
        playerSettings.UpdateEnemys(ListEnemysHit);
        ListEnemysHit.Clear();
        playerSettings.UpdateEnemys(ListCollectiblesCatch);
        ListCollectiblesCatch.Clear();
        playerSettings.wealth.ApplyChange(subWealth);
        subWealth = 0;
    }

    public ItemResults ContainEnemy(Enemy enemy)
    {
        return ListEnemysHit.Find(x => x.enemy == enemy);
    }


    public bool CouldCollectItem(int max, Collectibles collectible)
    {
        ItemResults itemResults = ListCollectiblesCatch.Find(x => x.enemy == collectible);
        if (itemResults != null)
        {
            if (max != 0 && itemResults.quantity >= max)
                return false;
        }
        return true;
    }

    public void AddCollectiblesList(Collectibles collectibles, int qnt = 1)
    {
        if (collectibles.collectValuable != 0)
        {
            subWealth += collectibles.collectValuable;
        }
        AddResultList(ListCollectiblesCatch, collectibles, qnt);
    }

    public void AddHitList(Enemy obstacle, int qnt = 1)
    {
        AddResultList(ListEnemysHit, obstacle, qnt);
    }

    private void AddResultList(List<ItemResults> list, Enemy enemy, int qnt =1)
    {
        ItemResults itemResults = list.Find(x => x.enemy == enemy);
        if (itemResults != null)
        {
            if (enemy.GetType() == typeof(Collectibles))
            {
                Collectibles collectibles = (Collectibles)enemy;
                if (itemResults.quantity + qnt < collectibles.maxCollectible)
                    itemResults.quantity += qnt;
            }
            else
                itemResults.quantity += qnt;
        }
        else
        {
            itemResults = new ItemResults(enemy, qnt);
            list.Add(itemResults);
        }
    }

    private void OnDisable()
    {
        gamePlay.EventCheckPoint -= UpdateSavePoint;
        EventPlayerReload += CleanPlayer;
    }

    #region Call Events
    public void CallEventPlayerDestroy()
    {
        isReady = false;
        if (EventPlayerDestroy != null)
        {
            EventPlayerDestroy();
        }
    }

    public void CallEventPlayerReload()
    {
        if (EventPlayerReload != null)
        {
            EventPlayerReload();
        }
    }
    public void CallEventControllerMovemant(Vector3 dir)
    {
        if (EventControllerMovement != null)
        {
            EventControllerMovement(dir);
        }
    }
    public void CallEventPlayerShoot()
    {
        if (EventPlayerShoot != null)
        {
            EventPlayerShoot();
        }
    }
    public void CallEventPlayerAddLive()
    {
        if (EventPlayerAddLive != null)
        {
            EventPlayerAddLive();
        }
    }
    public void CallEventIncresceHealth(int health)
    {
        if (EventIncresceHealth != null)
        {
            EventIncresceHealth(health);
        }
    }
    public void CallEventDecresceHealth(int health)
    {
        if (EventDecresceHealth != null)
        {
            EventDecresceHealth(health);
        }
    }
    public void CallEventLowHealth(int health)
    {
        if (EventLowHealth != null)
        {
            EventLowHealth(health);
        }
    }

    public void CallEventChangeSkin(ShopProductSkin skin)
    {
        if (EventChangeSkin != null)
        {
            EventChangeSkin(skin);
        }
    }
    #endregion
}

using BayatGames.SaveGameFree;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class PlayerMaster : MonoBehaviour
{
    public int idPlayer { get; private set; }
    public Player playerSettings;
    public bool HasPlayerReady;

    public int subWealth { get; private set; }
    [SerializeField]
    private List<EnemysResults> ListEnemysHit;
    [SerializeField]
    private List<EnemysResults> ListCollectiblesCatch;

    private CameraMaster myCam;
    private GamePlayMaster gamePlay;

    public Vector3 startPlayerPosition;
    private Dictionary<string, object> playerDefaults = new Dictionary<string, object>();

    public enum PlayerState { None, Accelerate, Deaccelerate, Reduce}
    public PlayerState playerState;

    #region Delagetes
    public delegate void GeneralEventHandler();
    public event GeneralEventHandler EventPlayerShoot;
    public event GeneralEventHandler EventPlayerDestroy;
    public event GeneralEventHandler EventPlayerReload;
    public event GeneralEventHandler EventPlayerBomb;
    public event GeneralEventHandler EventPlayerAddLive;
    public event GeneralEventHandler EventPlayerHit;

    public delegate void HealthEventHandler(int health);
    public event HealthEventHandler EventIncresceHealth;
    public event HealthEventHandler EventDecresceHealth;

    public delegate void ControllerEventHandler(Vector3 dir);
    public event ControllerEventHandler EventControllerMovement;

    public delegate void SkinChangeEventHandler(ShopProductSkin skin);
    public event SkinChangeEventHandler EventChangeSkin;

    #endregion

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventPausePlayGame += PausePlayer;
        EventPlayerReload += RespawnPosition;
        gamePlay.EventCheckPoint += UpdateSavePoint;
        gamePlay.EventUnPausePlayGame += UnPausePlayer;
        gamePlay.EventCompletePath += SaveWealth;
    }
    private void Start()
    {
        FBPlayerDefault();
        SetFBSetupCfg();
        if (GamePlaySettings.Instance.livesSpents <= 0 && playerSettings.lives.Value <= 0)
        {
            playerSettings.lives.SetValue(playerSettings.startLives);
        }
    }

    private void SetInitialReferences()
    {
        HasPlayerReady = false;
        gamePlay = GamePlayMaster.Instance;   
    }

    public void Init(Player player, int id)
    {
        idPlayer = id;
        playerSettings = player;
        name = playerSettings.name;
        GameSettings gameSettings = GameSettings.Instance;
        tag = gameSettings.playerTag;
        gameObject.layer = LayerMask.NameToLayer(gameSettings.playerLayer);
        SetTagLayerChild();
        startPlayerPosition = playerSettings.spawnPosition;
        transform.position = playerSettings.spawnPosition;
        transform.rotation = Quaternion.Euler(playerSettings.spawnRotation);
        playerSettings.lives.SetValue(playerSettings.startLives);
        if (playerSettings.lives.Value <= 0 && gameSettings.gameMode.modeId == gameSettings.GetGameModes(0).modeId)
            playerSettings.InitPlayer();
        else if (playerSettings.lives.Value <= 0 && gameSettings.gameMode.modeId != gameSettings.GetGameModes(0).modeId)
            playerSettings.ChangeLife(1);
        if (gameSettings.gameMode.modeId == gameSettings.GetGameModes(0).modeId ||
            GameManager.Instance.levelsFinish.Count <= 0)
            playerSettings.score.SetValue(0);
        GameManagerSaves.Instance.LoadPlayer(ref playerSettings);
        //playerSettings.LoadValues();        
    }

    private void FBPlayerDefault()
    {
        playerDefaults.Add("player_speed_horizontal", playerSettings.speedHorizontal);
        playerDefaults.Add("player_speed_vertical", playerSettings.speedVertical);
        playerDefaults.Add("player_speed_accelUp", playerSettings.multiplyVelocityUp);
        playerDefaults.Add("player_speed_accelDown", playerSettings.multuplyVelocityDown);
        playerDefaults.Add("player_speed_shoot", playerSettings.shootVelocity);
        playerDefaults.Add("player_startLives", playerSettings.startLives);
        playerDefaults.Add("player_maxLives", playerSettings.maxLives);
        playerDefaults.Add("player_maxHP", playerSettings.maxHP);
        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(playerDefaults);
    }

    private void SetFBSetupCfg()
    {
        Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
        playerSettings.speedHorizontal = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_speed_horizontal").DoubleValue;
        playerSettings.speedVertical = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_speed_vertical").DoubleValue;
        playerSettings.multiplyVelocityUp = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_speed_accelUp").DoubleValue;
        playerSettings.multuplyVelocityDown = (float)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_speed_accelDown").DoubleValue;
        playerSettings.startLives.ConstantValue = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_startLives").LongValue;
        playerSettings.maxLives.ConstantValue = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_maxLives").LongValue;
        playerSettings.maxHP.ConstantValue = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("player_maxHP").LongValue;
        GameSettings.Instance.numContinue = playerSettings.startLives;
    }
    public void SetFixCam(bool fix)
    {
        Camera.main.GetComponent<CameraMaster>().cheasePlayer = fix;
    }

    public void UpdateSavePoint(Vector3 position)
    {
        startPlayerPosition = new Vector3(position.x, transform.position.y, position.z);
        SaveWealth();
        if (GameManager.Instance.firebase.MyFirebaseApp != null && GameManager.Instance.firebase.dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Firebase.Analytics.Parameter[] FinishPath = {
            new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, gamePlay.GetActualLevel().GetName),
            new Firebase.Analytics.Parameter("Milstone", gamePlay.GetActualPath())
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("FinishPath", FinishPath);
        }
        GameManagerSaves.Instance.SavePlayer(playerSettings);
        //playerSettings.SaveValues();
    }

    private void SaveWealth()
    {
        playerSettings.wealth.ApplyChange(subWealth);
        subWealth = 0;
    }

    private void RespawnPosition()
    {
        transform.position = startPlayerPosition;
        transform.localRotation = Quaternion.Euler(playerSettings.spawnRotation);
    }

    private void PausePlayer()
    {
        HasPlayerReady = false;
    }

    private void UnPausePlayer()
    {
        HasPlayerReady = true;
    }

    public void SetTagLayerChild()
    {
        foreach (Transform child in transform)
        {
            child.tag = GameSettings.Instance.playerTag;
            child.gameObject.layer = LayerMask.NameToLayer(GameSettings.Instance.playerLayer);
        }
    }

    public bool ShouldPlayerBeReady
    {
        get
        {
            if (GamePlayMaster.Instance.ShouldBePlayingGame && HasPlayerReady == true)
                return true;
            else
                return false;
        }
    }

    public EnemysResults ContainEnemy(Enemy enemy)
    {
        return ListEnemysHit.Find(x => x.enemy == enemy);
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
        AddResultList(GamePlaySettings.Instance.HitEnemys, obstacle, qnt);
    }

    private void AddResultList(List<EnemysResults> list, Enemy enemy, int qnt = 1)
    {
        EnemysResults itemResults = list.Find(x => x.enemy == enemy);
        if (itemResults != null)
        {
            if (enemy.GetType() == typeof(Collectibles))
            {
                Collectibles collectibles = (Collectibles)enemy;
                if (itemResults.quantity + qnt < collectibles.maxCollectible)
                    itemResults.quantity += qnt;
                else
                    itemResults.quantity = collectibles.maxCollectible;
            }
            else
                itemResults.quantity += qnt;
        }
        else
        {
            itemResults = new EnemysResults(enemy, qnt);
            list.Add(itemResults);
        }
    }

    public bool CouldCollectItem(int max, Collectibles collectible)
    {
        EnemysResults itemResults = ListCollectiblesCatch.Find(x => x.enemy == collectible);
        if (itemResults != null)
        {
            if (max != 0 && itemResults.quantity >= max)
                return false;
        }
        return true;
    }
    private void OnDisable()
    {
        gamePlay.EventPausePlayGame -= PausePlayer;
        EventPlayerReload -= RespawnPosition;
        gamePlay.EventCheckPoint -= UpdateSavePoint;
        gamePlay.EventUnPausePlayGame -= UnPausePlayer;
        gamePlay.EventCompletePath -= SaveWealth;
    }

    #region Calls

    public void CallEventPlayerDestroy()
    {
        //HasPlayerReady = false;
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

    public void CallEventPlayerShoot()
    {
        if (EventPlayerShoot != null)
        {
            EventPlayerShoot();
        }
    }

    public void CallEventPlayerBomb()
    {
        if (EventPlayerBomb != null)
        {
            EventPlayerBomb();
        }
    }

    public void CallEventPlayerAddLive()
    {
        if (EventPlayerAddLive != null)
        {
            EventPlayerAddLive();
        }
    }
    public void CallEventPlayerHit()
    {
        if (EventPlayerHit != null)
        {
            EventPlayerHit();
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

    public void CallEventControllerMovemant(Vector3 dir)
    {
        if (EventControllerMovement != null)
        {
            EventControllerMovement(dir);
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

using Firebase.RemoteConfig;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayMaster : Singleton<GamePlayMaster>
{
    [SerializeField]
    private bool isGameBeat;
    [SerializeField]
    private bool isPausedPlayGame;
    [SerializeField]
    private bool isPathComplete;
    [SerializeField]
    private bool godMode;

    public bool GODMODE { get { return godMode; } }

    public bool IsPlayGamePause { get { return isPausedPlayGame; } }
    public bool IsGameBeat { get { return isGameBeat; } }
    [Header("Level Settings"), SerializeField]
    private Levels classicLevel;
    [SerializeField]
    private Player[] numPlayers;
    [SerializeField]
    private GameObject prefabPlayer;
    public List<GameObject> ListPlayer { get; private set; }
    private int actualPath;
    private Dictionary<string, object> gameplayDefault = new Dictionary<string, object>();

    private GameManager gameManager;
    private GameSettings gameSettings;
    private GamePlayAudio gamePlayAudio;
    public GamePlayAudio.LevelType actualBGM { get; set; }

    #region Delegates
    public delegate void GamePlayManagerEventHandler();
    public event GamePlayManagerEventHandler EventPausePlayGame;
    public event GamePlayManagerEventHandler EventUnPausePlayGame;
    public event GamePlayManagerEventHandler EventCompletePath;
    public event GamePlayManagerEventHandler EventCompleteGame;
    public event GamePlayManagerEventHandler EventGameOver;
    public event GamePlayManagerEventHandler EventShouldContinue;
    public event GamePlayManagerEventHandler EventStartPath;
    public event GamePlayManagerEventHandler EventResetEnemys;
    public event GamePlayManagerEventHandler EventResetPlayers;
    public event GamePlayManagerEventHandler EventUICollectable;

    public delegate void PlayerPositionEventHandler(Vector3 position);
    public event PlayerPositionEventHandler EventCheckPoint;
    public event PlayerPositionEventHandler EventCheckPlayerPosition;

    public delegate void PowerUpEventHandler(bool active);
    public event PowerUpEventHandler EventRapidFire;

    public delegate void GameManagerRestartPlayer(int lives);
    public event GameManagerRestartPlayer EventRestartPlayer;

    public delegate void CollectableEventHandle(Collectibles collectibles);
    public event CollectableEventHandle EventCollectItem;

    public delegate void ShakeCamEventHandle(float power, float intime);
    public event ShakeCamEventHandle EventShakeCam;

    #endregion

    private void OnEnable()
    {
        SetInitialReferences();
        isPathComplete = false;
        GameObject levelroot = new GameObject();
        actualPath = 0;
        levelroot.name = gameManager.actualLevel.levelName;
        gameManager.actualLevel.CreateLevel(levelroot.transform);
        actualBGM = gameManager.actualLevel.startLevelBGM;
        gamePlayAudio.PlayBGM(actualBGM);
        SpawnPlayers(numPlayers.Length);
        EventCheckPlayerPosition += CheckPoolLevel;
        gameplayDefault.Add("config_gamePlay_godmod", false);
        FirebaseRemoteConfig.SetDefaults(gameplayDefault);
    }

    private void Start()
    {
        FirebaseRemoteConfig.ActivateFetched();

        godMode = (godMode) ? godMode : FirebaseRemoteConfig.GetValue("config_gamePlay_godmod").BooleanValue;
    }

    private void SpawnPlayers(int numplayers)
    {
        ListPlayer = new List<GameObject>();
        for (int i = 0; i < numplayers; i++)
        {
            ListPlayer.Add(Instantiate(prefabPlayer));
            ListPlayer[i].SetActive(true);
            ListPlayer[i].name = "Player" + i;
            ListPlayer[i].GetComponent<PlayerMaster>().Init(numPlayers[i], i);
        }
    }

    private void SetInitialReferences()
    {
        gameManager = GameManager.Instance;
        gameSettings = GameSettings.Instance;
        gamePlayAudio = GamePlayAudio.Instance;
        if (gameSettings.gameMode.modeId == gameSettings.GetGameModes(0).modeId)
        {
            gameManager.actualLevel = classicLevel;
        }
    }

    public GameObject GetPlayer(int id)
    {
        return ListPlayer[id];
    }

    public PlayerMaster GetPlayerMaster(int id)
    {
        return ListPlayer[id].GetComponent<PlayerMaster>();
    }

    public Player GetPlayerSettings(int id)
    {
        return ListPlayer[id].GetComponent<PlayerMaster>().playerSettings;
    }

    public void ReadyPlayer(bool ready)
    {
        for (int i = 0; i < ListPlayer.Count; i++)
        {
            ListPlayer[i].GetComponent<PlayerMaster>().HasPlayerReady = ready;
        }
    }
    public Levels GetActualLevel()
    {
        return gameManager.actualLevel;
    }

    public int GetActualPath()
    {
        return actualPath;
    }

    private void CheckPoolLevel(Vector3 pos)
    {
        if (!isPathComplete && (gameManager.actualLevel.levelMilstones[actualPath] - pos).z <= 0)
        {
            gameManager.actualLevel.CallUpdatePoolLevel(actualPath);
            actualPath++;
        }
    }

    public void GamePlayPause(bool setpause)
    {
        isPausedPlayGame = setpause;
    }

    public int HightScorePlayers()
    {
        if (ListPlayer.Count > 0)
        {
            int score = 0;
            foreach (GameObject pl in ListPlayer)
            {
                if (score < pl.GetComponent<PlayerMaster>().playerSettings.score.Value)
                    score = (int)pl.GetComponent<PlayerMaster>().playerSettings.score.Value;
            }
            return score;
        }
        else
            return 1;
    }

    public void SaveAllPlayers()
    {
        for (int i = 0; i < numPlayers.Length; i++)
        {
            GameManagerSaves.Instance.SavePlayer(numPlayers[i]);
        }
    }

    public bool ShouldPlayReady
    {
        get
        {
            for (int i = 0; i < ListPlayer.Count; i++)
            {
                if (ListPlayer[i].GetComponent<PlayerMaster>().HasPlayerReady)
                    return true;
            }
            return false;
        }
    }

    public bool ShouldStartPath
    {
        get
        {
            return (isPausedPlayGame && !gameManager.isGamePaused && !gameManager.isGameOver && !isGameBeat && !isPathComplete) ? true : false;
        }
    }

    public bool ShouldBePlayingGame
    {
        get
        {
            return !gameManager.isGamePaused && !isPausedPlayGame && (gameManager.isGameOver || isPathComplete) ? false : true;
        }
    }

    public bool ShouldBeInGameOver
    {
        get
        {
            return (gameManager.isGameOver && !gameManager.isGamePaused && !isPausedPlayGame) ? true : false;
        }
    }

    public bool ShouldBeFinishPath
    {
        get
        {
            return (isPathComplete && !isPausedPlayGame && !gameManager.isGameOver && !gameManager.isGamePaused) ? true : false;
        }
    }

    public bool ShouldFinishGame
    {
        get
        {
            return (isGameBeat && isPathComplete && !gameManager.isGameOver && !gameManager.isGamePaused) ? true : false;
        }
    }

    #region Calls

    public void CallEventPausePlayGame()
    {
        isPausedPlayGame = true;
        if (EventPausePlayGame != null)
            EventPausePlayGame();
    }

    public void CallEventUnPausePlayGame()
    {
        isPausedPlayGame = false;
        if (EventUnPausePlayGame != null)
            EventUnPausePlayGame();
    }

    public void CallEventCompletePath()
    {
        isPathComplete = true;
        if (EventCompletePath != null)
            EventCompletePath();
    }

    public void CallEventCompleteGame()
    {
        isGameBeat = true;
        if (EventCompleteGame != null)
            EventCompleteGame();
    }
    public void CallEventGameOver()
    {
        gameManager.isGameOver = true;
        GamePlayAudio.Instance.StopBGM();
        if (EventGameOver != null)
            EventGameOver();
    }

    public void CallEventShouldContinue()
    {
        if (EventShouldContinue != null)
        {
            EventShouldContinue();
        }
    }

    public void CallEventStartPath()
    {
        if (EventStartPath != null)
            EventStartPath();
    }

    public void CallEventResetEnemys()
    {
        if (EventResetEnemys != null)
            EventResetEnemys();
    }

    public void CallEventResetPlayers()
    {
        if (EventResetPlayers != null)
            EventResetPlayers();
    }

    public void CallEventCheckPoint(Vector3 position)
    {
        if (EventCheckPoint != null)
            EventCheckPoint(position);
    }

    public void CallEventCheckPlayerPosition(Vector3 position)
    {
        if (EventCheckPlayerPosition != null)
            EventCheckPlayerPosition(position);
    }

    public void CallEventRapidFire(bool active)
    {
        if (EventRapidFire != null)
            EventRapidFire(active);
    }
    public void CallEventCollectable(Collectibles collectibles)
    {
        if (EventCollectItem != null)
        {
            EventCollectItem(collectibles);
        }
        if (EventUICollectable != null)
        {
            EventUICollectable();
        }
    }

    public void CallEventRestartPlayer(int lives)
    {
        gameManager.SetupGame();
        if (EventRestartPlayer != null)
        {
            EventRestartPlayer(lives);
        }
    }
    public void CallEventShakeCam(float power, float intime)
    {
        if (EventShakeCam != null)
        {
            EventShakeCam(power, intime);
        }
    }
    #endregion

    private void OnDisable()
    {
        EventCheckPlayerPosition -= CheckPoolLevel;
        gamePlayAudio.StopBGM();
        StopAllCoroutines();
    }
    protected override void OnDestroy() { }
}

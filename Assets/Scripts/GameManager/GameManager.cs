using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isGameOver;
    public bool isGamePaused;
    public bool isGameBaet;
    public bool internetConnection;
    public static bool muteAudio;
    [SerializeField]
    private GameSettings gameSettings;
    [SerializeField]
    public GamePlaySettings gamePlayLog;
    [SerializeField]
    public List<Levels> levelsFinish = new List<Levels>();
    [SerializeField]
    public Levels actualLevel;
    [SerializeField]
    private Player[] numPlayer;
    private Dictionary<string, object> gameplayDefault = new Dictionary<string, object>();

    [HideInInspector]
    public GameManagerFirebase firebase;
    private GameManagerSaves gameSaves;

    private void OnEnable()
    {
        if (InternetCheck.InternetConnection) internetConnection = true;
        gameSaves = GameManagerSaves.Instance;
    }

    private void Start()
    {
        gameplayDefault.Add("config_resetSaves", false);
        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(gameplayDefault);
        Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
        bool resetSaves = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("config_resetSaves").BooleanValue;
        if (resetSaves)
            gameSaves.SaveGameClear();
        gameSaves.LoadGamePlay(gamePlayLog);
        levelsFinish = gameSaves.LoadLevelComplete("levelComplete");
        firebase = new GameManagerFirebase();
        StartCoroutine(LoadAsyncScene());
    }
    public void SetupGame()
    {
        isGameOver = false;
        isGamePaused = false;
    }

    public void TogglePauseGame()
    {
        if (isGamePaused)
        {
            Time.timeScale = 1;
            isGamePaused = false;
        }
        else
        {
            Time.timeScale = 0;
            isGamePaused = true;
        }
    }

    public IEnumerator LoadAsyncScene()
    {
        FadeScenesManager.Instance.loadAsync.SetActive(true);
        while (firebase.dependencyStatus != Firebase.DependencyStatus.Available)
        {
            yield return null;
        }
        FadeScenesManager.Instance.loadAsync.SetActive(false);
    }

    public Player GetFirstPlayer(int num = 0)
    {
        return numPlayer[num];
    }
    private void OnDisable()
    {
        gamePlayLog.totalTime += Time.unscaledTime;
        gameSaves.SaveGamePlay(gamePlayLog);
    }
    protected override void OnDestroy() { }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class GamePlayManager : MonoBehaviour
{
    private GamePlayMaster gamePlayMaster;
    private GameManagerFirebase firebase;
    private GamePlaySettings gamePlayLog;
    private GameManagerSaves gameSaves;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        gamePlayMaster = GetComponent<GamePlayMaster>();
        firebase = GameManager.Instance.firebase;
        gamePlayLog = GamePlaySettings.Instance;
        gameSaves = GameManagerSaves.Instance;
    }

    private IEnumerator Start()
    {
        while (!LoadSceneManager.Instance.completeLoad)// && firebase.dependencyStatus != Firebase.DependencyStatus..FBStatus.None)
        {
            yield return null;
        }
        if (firebase == null || firebase.dependencyStatus != Firebase.DependencyStatus.Available)
        {
            firebase = new GameManagerFirebase();
            yield return null;
        }

        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        // configurações inicias
        SetConfigGame();
        // courotina de inicio das missões
        yield return StartCoroutine(MissionStarting());

        // courotina da missão em si
        yield return StartCoroutine(MissionPlaying());

        yield return StartCoroutine(GameOverState());

        // courotina do final da missão
        yield return StartCoroutine(MissionEnding());

        yield return StartCoroutine(BeatGameState());

        //verifica se o jogo terminou
        yield return null;
    }

    private void SetConfigGame()
    {
        gamePlayMaster.CallEventPausePlayGame();
        GameManager.Instance.SetupGame();
    }

    private IEnumerator MissionStarting()
    {
        if (gamePlayMaster.ShouldStartPath)
        {
            //Debug.Log("Iniciou a fase");
            gamePlayMaster.CallEventStartPath();
            while (gamePlayMaster.IsPlayGamePause)
            {
                //Debug.Log("Esta pausado");
                yield return null;
            }
        }
    }

    private IEnumerator MissionPlaying()
    {
        while (gamePlayMaster.ShouldBePlayingGame)
        {
            //Debug.Log("Jogando");
            yield return null;
        }
        GameManagerSaves.Instance.SaveGamePlay(GameManager.Instance.gamePlayLog);
    }

    private IEnumerator MissionEnding()
    {
        if (gamePlayMaster.ShouldBeFinishPath)
        {
            //Debug.Log("Terminou a fase");
            gamePlayMaster.CallEventCompletePath();
            yield return new WaitForSeconds(3);
        }
        yield return null;
    }

    private IEnumerator GameOverState()
    {
        if (gamePlayMaster.ShouldBeInGameOver)
        {
            //Debug.Log("GameOver");
            yield return new WaitForSeconds(1);

            if (GameSettings.Instance.gameMode.modeId == GameSettings.Instance.GetGameModes("Classic").modeId)
                LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("MainMenu").sceneId);
            else
                LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("HUD").sceneId);
            yield return null;
        }
    }

    private IEnumerator BeatGameState()
    {
        if (gamePlayMaster.ShouldFinishGame)
        {
            //Debug.Log("TERMINOU O JOGO!!!!");
            yield return new WaitForSeconds(2);
            LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("EndGame").sceneId);
            yield return null;
        }
    }

    private void OnDisable()
    {
        gamePlayLog.totalTime += Time.unscaledTime;
        gameSaves.SaveGamePlay(gamePlayLog);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.NewLocalization;

public class GamePlayManager : MonoBehaviour
{
    [SerializeField]
    private GameObject panelMessage;
    [SerializeField]
    private GameObject panelResults;
    public EasyTween[] EasyTweenStart;
    [Header("Translate")]
    public LocalizationString msnCongratulation;
    public LocalizationString msnGameOver;
    //[SerializeField]
    //private ListLevels finishLevels;

    private GamePlayMaster gamePlay;
    private GameManager gameManager;
    

    private void Awake()
    {
        GamePlayMaster.Instance.levelComplete = false;
        GameManager.Instance.ActualLevel.CreateLevel();
        PlayerStart(GameSettings.Instance.GamePlayer);
        panelResults.SetActive(false);
    }

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        gameManager = GameManager.Instance;
    }

    private IEnumerator Start()
    {
        while (!LoadSceneManager.Instance.completeLoad)
        {
            yield return null;
        }
        StartCoroutine(GameLoop());
    }

    private void PlayerStart(GameObject playerPrefab)
    {
        playerPrefab.SetActive(false);
        GameObject firstPlayer = Instantiate<GameObject>(playerPrefab);
        PlayerMaster playerMaster = firstPlayer.GetComponent<PlayerMaster>();
        LevelPlayerSpawn levelPlayerSpawn = FindObjectOfType<LevelPlayerSpawn>();
        firstPlayer.transform.position = (playerMaster.useLevelPlayerSpawn && levelPlayerSpawn != null) ? levelPlayerSpawn.transform.position : playerMaster.GetStartPosition();
        firstPlayer.transform.rotation = (playerMaster.useLevelPlayerSpawn && levelPlayerSpawn != null) ? levelPlayerSpawn.transform.rotation : playerMaster.GetStartRotation();
        firstPlayer.SetActive(true);
        playerMaster.CallEventPlayerReload();
        GamePlayMaster.Instance.SetPlayers(playerMaster);
        GamePlayMaster.Instance.PausePlayGame();
    }

    private void SetConfigGame()
    {
        // reunicia sempre que um jogo começa
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

    private IEnumerator MissionEnding()
    {
        if (gameManager.ShouldBeInGame)
        {
            Debug.Log("Terminou a fase?");
            gameManager.levelsFinish.Add(gameManager.ActualLevel);
            panelMessage.SetActive(true);
            LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
            panelMessage.GetComponentInChildren<Text>().text = translate.Translate(msnCongratulation, LocalizationTranslate.StringFormat.AllUpcase);
            EasyTweenStart[0].OpenCloseObjectAnimation();
            yield return new WaitForSeconds(EasyTweenStart[0].GetAnimationDuration());
            EasyTweenStart[0].OpenCloseObjectAnimation();
            yield return new WaitForSeconds(EasyTweenStart[0].GetAnimationDuration());
            panelResults.SetActive(true);
            yield return null;
        }
    }

    private IEnumerator MissionPlaying()
    {
        Debug.Log("jogando a fase");

        while (gameManager.ShouldBeInGame && !gamePlay.levelComplete)
        {
            yield return null;
        }
    }

    private IEnumerator MissionStarting()
    {
        Debug.Log("Começando a fase");
        GamePlayMaster.Instance.CallEventStartMission(gameManager.ActualLevel);
        EasyTweenStart[0].OpenCloseObjectAnimation();
        panelMessage.GetComponentInChildren<Text>().text = gameManager.ActualLevel.GetName;
        yield return new WaitForSeconds(EasyTweenStart[0].GetAnimationDuration());
        
        EasyTweenStart[0].OpenCloseObjectAnimation();
        yield return new WaitForSeconds(EasyTweenStart[0].GetAnimationDuration());
        GamePlayMaster.Instance.UnPausePlayGame();
        yield return null;
    }

    private IEnumerator GameOverState()
    {
        if (gameManager.isGameOver) {
            panelMessage.SetActive(true);
            LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
            panelMessage.GetComponentInChildren<Text>().text = translate.Translate(msnGameOver, LocalizationTranslate.StringFormat.AllUpcase);
            EasyTweenStart[0].OpenCloseObjectAnimation();
            yield return new WaitForSeconds(EasyTweenStart[0].GetAnimationDuration());
            EasyTweenStart[0].OpenCloseObjectAnimation();
            yield return new WaitForSeconds(EasyTweenStart[0].GetAnimationDuration());
            ///
            // check if continue;
            if (GameSettings.Instance.CheckIfContinue)
                gamePlay.CallEventContinue();
            else
                gamePlay.CallEventGameOver();
        }
        yield return null;
    }

    private IEnumerator BeatGameState()
    {
        if (gameManager.isGameBeat)
        {
            Debug.Log("TERMINOU O JOGO!!!!");
        }
        yield return null;
    }
}

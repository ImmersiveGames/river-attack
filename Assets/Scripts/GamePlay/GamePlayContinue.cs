using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Utils.Variables;
using Utils.NewLocalization;

public class GamePlayContinue : MonoBehaviour {
    [SerializeField]
    private GameObject panelContinue;
    [SerializeField]
    private Text continueMessage;
    [SerializeField]
    private string idRewardVideo;
    [SerializeField]
    private int livesToEarn;
    [SerializeField]
    private IntVariable continues;
    [Header("Translate")]
    public LocalizationString[] menssagesLocalize;
    private GamePlayMaster gamePlay;
    private LoadSceneManager loadScene;
    private GameSettings gameSettings;
    private LocalizationTranslate translate;

    private void Awake()
    {
        panelContinue.SetActive(false);
    }
    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventContinue += ContinueGame;
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        loadScene = LoadSceneManager.Instance;
        gameSettings = GameSettings.Instance;
        translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
    }

    private void ContinueGame()
    {
        panelContinue.SetActive(true);
        if (gameSettings.GameMode == GameSettings.GameModes.Classic)
        {
            string s = translate.Translate(menssagesLocalize[0], LocalizationTranslate.StringFormat.FirstLetterUp);
            continueMessage.text = string.Format(s, continues);
            // controla a quantidade de continue, mas ja chega aquiu contando que existem continues
            //continueMessage.text = LanguageSettings.Instance.GetText("Do you want to continue your raid?  Watch the movie to earn an extra life! \n reast {0} continues");
            //continueMessage.text = string.Format(continueMessage.text, continues);
        }
        else
        {
            string s = translate.Translate(menssagesLocalize[1], livesToEarn, LocalizationTranslate.StringFormat.FirstLetterUp);
            continueMessage.text = string.Format(s, livesToEarn);
            //continueMessage.text = LanguageSettings.Instance.GetText("Do you want to continue your raid?  Watch the movie to earn {0} extra life!");
            //continueMessage.text = string.Format(continueMessage.text, livesToEarn);
        }
    }

    public void PlayRewardVideo()
    {
        ShowOptions showOptions = new ShowOptions
        {
            resultCallback = CallBackRewardVideo
        };
        AdManager.Instance.ShowAd(showOptions, idRewardVideo);
    }

    public void ExitButton()
    {
        GameManager.Instance.ContinueGame(1);
        LoadScene();
    }

    private void CallBackRewardVideo(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                //Debug.Log("Ad Finished. Rewarding player...");
                GameManager.Instance.ContinueGame(livesToEarn);
                LoadScene(true);
                break;
            default:
               // Debug.Log("Recusou o ad");
                GameManager.Instance.ContinueGame(1);
                LoadScene();
                break;
        }
        
    }

    private void LoadScene(bool result = false)
    {
        if (loadScene != null && gameSettings.GameMode == GameSettings.GameModes.Mission)
            loadScene.LoadScene((int)GameSettings.GameScenes.HUB);
        else
        {
            if (result)
            {
                //TODO: preciso Reiniciar o jogo apartir do ultimo savepoint
                panelContinue.SetActive(false);
            }
            else
                loadScene.LoadScene((int)GameSettings.GameScenes.MenuInitial);
        }
    }

    private void OnDisable()
    {
        gamePlay.EventContinue -= ContinueGame;
    }
}

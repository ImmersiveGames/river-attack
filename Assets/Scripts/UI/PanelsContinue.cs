using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using MyUtils.NewLocalization;
using System;
using System.Collections;

public class PanelsContinue : AdMobRewards
{
    [SerializeField]
    private GameObject panelContinueClassic, panelContinueMission;
    [SerializeField]
    private Text continueText;
    [SerializeField]
    private LocalizationString continueString;

    private GamePlayMaster gamePlay;

    private void Awake()
    {
        panelContinueClassic.SetActive(false);
        panelContinueMission.SetActive(false);
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetInitialReferences();
        gamePlay.EventShouldContinue += EnablePanel;
    }

    protected override void Start()
    {
        base.Start();
        LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
        continueText.text = String.Format(translate.Translate(continueString, LocalizationTranslate.StringFormat.AllUpcase, 3), 3);
    }

    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
    }

    public override void ShowBanner()
    {
        base.ShowBanner();
#if UNITY_EDITOR
        Reward("teste", 3);
        panelContinueClassic.SetActive(false);
#endif
        GamePlayAudio.Instance.PlayBGM(gamePlay.actualBGM);
    }

    protected override void Reward(string rewardtype, double qnt)
    {
        if (adTest)
            gamePlay.CallEventRestartPlayer(3);
        else
            gamePlay.CallEventRestartPlayer((int)qnt);
        gamePlay.CallEventResetPlayers();   
    }

    private void CancelReward()
    {
        gamePlay.CallEventGameOver();
    }
    public void ButtonQuitPlayGame()
    {
        gamePlay.CallEventUnPausePlayGame();
    }

    #region Handlers
    public override void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        base.HandleRewardBasedVideoFailedToLoad(sender, args);
    }
    public override void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        base.HandleRewardBasedVideoClosed(sender, args);
        if (!GameManager.Instance.isGameOver)
        {
            panelContinueClassic.SetActive(false);
        }
    }

    public override void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        base.HandleRewardBasedVideoRewarded(sender, args);
    }

    public override void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        base.HandleRewardBasedVideoLeftApplication(sender, args);
        panelContinueClassic.SetActive(false);
    }
    #endregion

    private void EnablePanel()
    {
        if (InternetCheck.InternetConnection)
        {
            if (GameSettings.Instance.gameMode == GameSettings.Instance.GetGameModes(0))
                panelContinueClassic.SetActive(true);
            else
                panelContinueClassic.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            GamePlayAudio.Instance.PlayBGM(gamePlay.actualBGM);
            gamePlay.CallEventUnPausePlayGame();
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        gamePlay.EventShouldContinue -= EnablePanel;
    }
}

using MyUtils.NewLocalization;
using UnityEngine;
using UnityEngine.UI;

public class PanelSplashText : MonoBehaviour {
    [SerializeField]
    private Text textField;

    public void SetSplashText(LocalizationString splashText)
    {
        LocalizationTranslate translate = new LocalizationTranslate(LocalizationSettings.Instance.GetActualLanguage());
        textField.text = translate.Translate(splashText, LocalizationTranslate.StringFormat.AllUpcase); 
    }
    public void SetSplashText(string text)
    {
        textField.text = text;
    }
    /// <summary>
    /// Call in Animation
    /// </summary>
    public void UnPausedGame()
    {
        GamePlayMaster.Instance.GamePlayPause(false);
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Call in Animation
    /// </summary>
    public void ContinueGame()
    {
        GamePlayMaster.Instance.CallEventShouldContinue();
        gameObject.SetActive(false);
    }
    /// <summary>
    /// Call in animation
    /// </summary>
    public void CompletePath()
    {
        //Debug.Log("Completou a fase mandar para a HUD ou Para a tela inicial e verificar se terminou o jogo");
        if (GameSettings.Instance.gameMode.modeId == GameSettings.Instance.GetGameModes(0).modeId)
        {
            GamePlayMaster.Instance.CallEventCompleteGame();
        }
        else
        {
            GameSettings.Instance.ChangeGameScene(GameSettings.Instance.GetGameScenes("HUD"));
        }
    }
}

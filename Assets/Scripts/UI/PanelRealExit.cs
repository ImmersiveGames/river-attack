using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelRealExit : MonoBehaviour
{
    private GamePlayMaster gamePlay;

    private void OnEnable()
    {
        gamePlay = GamePlayMaster.Instance;
    }

    public void ButtonPlayGame()
    {
        Time.timeScale = 1;
        gamePlay.CallEventUnPausePlayGame();
        gameObject.SetActive(false);
    }

    public void ButtonQuitPlayGame()
    {
        gamePlay.SaveAllPlayers();
        gameObject.SetActive(false);
        Time.timeScale = 1;
        if (GameSettings.Instance.gameMode.modeId == GameSettings.Instance.GetGameModes("Classic").modeId)
            LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("MainMenu").sceneId);
        else
            LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("HUD").sceneId);
            
        gamePlay.CallEventUnPausePlayGame();
    }
}

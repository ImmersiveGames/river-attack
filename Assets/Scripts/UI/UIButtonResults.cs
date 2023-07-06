using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonResults : MonoBehaviour
{

    public GameObject panelResult;
    private GameSettings gameSettings;

    private void OnEnable()
    {
        gameSettings = GameSettings.Instance;
    }

    public void FinishResult()
    {
        panelResult.SetActive(false);
        if (gameSettings.gameMode.modeId == gameSettings.GetGameModes(1).modeId)
        {
            gameSettings.ChangeGameScene(gameSettings.GetGameScenes("HUB"));
        }        
    }
}

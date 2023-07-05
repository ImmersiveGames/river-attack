using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    public bool isGameOver;
    [SerializeField]
    public bool isGameBeat;
    [SerializeField]
    private bool isGamePaused;
    [SerializeField]
    private GameSettings gameSettings;
    [SerializeField]
    private Levels actualLevel;
    [SerializeField]
    public ListLevels levelsFinish;
    //[SerializeField]
    //private Enemy typeCoins;

    //public Enemy TypeCoins { get { return typeCoins; } }
    //TODO: Criar uma lista mestra em um Scriptable provavelmente no que fará as listas de progresso 
    [SerializeField]
    private List<Player> players = new List<Player>();

    public bool ShouldBeInGame { get { return (!isGameOver && !isGameBeat) ? true : false; } }
    public Levels ActualLevel { get { return actualLevel; } }
    public List<Player> Players { get { return players; } }

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
    public void SetActualLevel(Levels level)
    {
        actualLevel = level;
    }

    public void ContinueGame(int lives)
    {
        GameManager.Instance.isGameOver = false;
        if (gameSettings.GameMode == GameSettings.GameModes.Classic)
            gameSettings.ChangeContinue(-1);

        for (int i = 0; i < players.Count; i++)
            players[i].lives.SetValue(lives);
    }

    protected override void OnDestroy(){ }
}

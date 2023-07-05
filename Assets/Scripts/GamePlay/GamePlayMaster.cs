using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GamePlayMaster : Singleton<GamePlayMaster>
{
    [SerializeField]
    public bool uiController;
    [SerializeField]
    private bool isPausedPlayGame;
    [SerializeField]
    public bool levelComplete;
    [SerializeField]
    [ReadOnly]
    private List<PlayerMaster> listPlayersMaster = new List<PlayerMaster>();
    
    #region Delegates
    public delegate void GameManagerEventHandler();
    public event GameManagerEventHandler EventResetEnemys;
    public event GameManagerEventHandler EventPauseGame;
    public event GameManagerEventHandler EventBeatGame;
    public event GameManagerEventHandler EventCompleteMission;
    public event GameManagerEventHandler EventExitLevel;
    public event GameManagerEventHandler EventGameOver;
    public event GameManagerEventHandler EventContinue;
    public event GameManagerEventHandler EventUICollectable;

    public delegate void GameManagerMissionEventHandler(Levels level);
    public event GameManagerMissionEventHandler EventStartMission;
    public event GameManagerMissionEventHandler EventFailMission;

    public delegate void GameManagerCheckPointEventHandler(Vector3 position);
    public event GameManagerCheckPointEventHandler EventCheckPoint;


    public delegate void PowerUpEventHandler(bool active);
    public event PowerUpEventHandler EventRapidFire;

    #endregion

    public bool IsPausedGame { get { return isPausedPlayGame; } }

    public void PausePlayGame() { isPausedPlayGame = true; }

    public void UnPausePlayGame() { isPausedPlayGame = false; }

    public bool ShouldBePlayingGame
    {
        // devo estar jogando ou sair?
        get
        {
            if (GameManager.Instance.isGameOver || GameManager.Instance.isGameBeat || isPausedPlayGame || listPlayersMaster.Count <= 0)
                return false;
            else
                return true;
        }
    }

    public void SetPlayers(PlayerMaster player)
    {
        listPlayersMaster.Add(player);
    }

    public PlayerMaster GetPlayer(int index)
    {
        if (listPlayersMaster.Count > 0 || index < listPlayersMaster.Count)
            return listPlayersMaster[index];
        else
            return null;
    }

    public void RemovePlayer(PlayerMaster player)
    {
        if (listPlayersMaster.Contains(player))
        {
            listPlayersMaster.Remove(player);
        }
    }

    public List<PlayerMaster> GetAllPlayer()
    {
        return listPlayersMaster;
    }

    public int SumScorePlayers()
    {
        float score = 0;
        score = listPlayersMaster.Sum(item => item.playerSettings.score.Value);
        score /= listPlayersMaster.Count;
        return (int)score;
    }

    public int HightScorePlayers()
    {
        if (listPlayersMaster.Count > 0)
            return listPlayersMaster.Max(x => x.playerSettings.score.Value);
        else
            return 0;
    }

    #region Calls

    public void CallEventResetEnemys()
    {
        if (EventResetEnemys != null)
            EventResetEnemys();
    }
    public void CallEventPauseGame()
    {
        isPausedPlayGame = true;
        if (EventPauseGame != null)
        {
            EventPauseGame();
        }
    }
    public void CallEventBeatGame()
    {
        GameManager.Instance.isGameBeat = true;
        if (EventBeatGame != null)
        {
            EventBeatGame();
        }
    }
    public void CallEventExitLevel()
    {
        if (EventExitLevel != null)
        {
            EventExitLevel();
        }
    }
    public void CallEventGameOver()
    {
        GameManager.Instance.isGameOver = true;
        if (EventGameOver != null)
        {
            EventGameOver();
        }
    }
    public void CallEventContinue()
    {
        if (EventContinue != null)
        {
            EventContinue();
        }
    }
    public void CallEventUICollectable()
    {
        if (EventUICollectable != null)
        {
            EventUICollectable();
        }
    }
    public void CallEventStartMission(Levels level)
    {
        if (EventStartMission != null)
            EventStartMission(level);
    }
    public void CallEventFailMission(Levels level)
    {
        if (EventFailMission != null)
            EventFailMission(level);
    }
    public void CallEventCompleteMission()
    {
        if (EventCompleteMission != null)
            EventCompleteMission();
    }
    public void CallEventCheckPoint(Vector3 position)
    {
        if (EventCheckPoint != null)
            EventCheckPoint(position);
    }

    public void CallEventRapidFire(bool active)
    {
        if (EventRapidFire != null)
        {
            EventRapidFire(active);
        }
    }
    #endregion
    protected override void OnDestroy() { }
}

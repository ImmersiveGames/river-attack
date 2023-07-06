/// <summary>
/// Namespace:      None
/// Class:          PlayerLives
/// Description:    Controla as vidas do player
/// Author:         Renato Innocenti                    Date: 29/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: 1.0           Date: 29/03/2018       Description: Separação das vidas do combustivel
/// </summary>
///
using System;
using System.Collections;
using UnityEngine;
using MyUtils.Variables;
[RequireComponent(typeof(PlayerMaster))]
public class PlayerLives : MonoBehaviour
{
    [SerializeField]
    private float timetoReSpawn = 1.8f;
    #region Variable Private Inspector
    private PlayerMaster playerMaster;
    private Player player;
    private GamePlayMaster gamePlayMaster;

    //[SerializeField]
    //public IntVariable varForExtraLife;
    [SerializeField]
    private int scoreForExtraLife;
    private int myscore;
    //#endregion
    //#region Variable Private References
    //private PlayerMaster playerMaster;
    //private GamePlayMaster gamePlay;
    //private int _score;
    //private int nextlive;
    #endregion

    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerDestroy += KillPlayer;
        playerMaster.EventPlayerHit += GainExtraLive;
        gamePlayMaster.EventRestartPlayer += RevivePlayer;
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        player = playerMaster.playerSettings;
        gamePlayMaster = GamePlayMaster.Instance;
    }

    private void Start()
    {
        myscore = (int)player.score.Value + scoreForExtraLife;
    }

    private void AddLives(int newlives)
    {
        if (player.maxLives > 0 && (player.lives.Value + newlives) > player.maxLives)
            player.lives.ApplyChange(player.maxLives);
        else
            player.lives.ApplyChange(newlives);
        playerMaster.CallEventPlayerAddLive();
    }

    private void KillPlayer()
    {
        player.ChangeLife(-1);
        LogLives(1);
        if (player.lives.Value <= 0 && !GameManager.Instance.isGameOver)
        {
            gamePlayMaster.CallEventGameOver();
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ReSpawn());
        }
    }

    public void RevivePlayer(int numlives)
    {
        player.ChangeLife(numlives);
        //StopAllCoroutines();
        StartCoroutine(ReSpawn());
    }

    private IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(timetoReSpawn);
        if (gamePlayMaster.ShouldBePlayingGame)
        {
            playerMaster.CallEventPlayerReload();
            gamePlayMaster.CallEventResetPlayers();
            gamePlayMaster.CallEventResetEnemys();
            gamePlayMaster.GamePlayPause(false);
            //gamePlayMaster.CallEventUnPausePlayGame();
        }
    }

    private void GainExtraLive()
    {
        if (player.score.Value >= myscore)
        {
            int rest = (int)player.score.Value - myscore;
            int life = 1;
            if (rest >= scoreForExtraLife) life += rest / scoreForExtraLife;
            AddLives(life);
            myscore = ((int)player.score.Value - rest) + scoreForExtraLife * life;
        }
    }

    private void LogLives(int lives)
    {
        GamePlaySettings.Instance.livesSpents += Mathf.Abs(lives);
        if (GameManager.Instance.firebase.MyFirebaseApp != null && GameManager.Instance.firebase.dependencyStatus == Firebase.DependencyStatus.Available)
        {
            Firebase.Analytics.Parameter[] KillerPlayer = {
            new Firebase.Analytics.Parameter(Firebase.Analytics.FirebaseAnalytics.ParameterLevelName, gamePlayMaster.GetActualLevel().GetName),
            new Firebase.Analytics.Parameter("Milstone", gamePlayMaster.GetActualPath()),
            new Firebase.Analytics.Parameter("Killpos_x", transform.position.x),
            new Firebase.Analytics.Parameter("Killpos_z", transform.position.z)
            };
            Firebase.Analytics.FirebaseAnalytics.LogEvent("KillerPlayer", KillerPlayer);
        }
    }

    private void OnDisable()
    {
        playerMaster.EventPlayerDestroy -= KillPlayer;
        playerMaster.EventPlayerHit -= GainExtraLive;
        gamePlayMaster.EventRestartPlayer -= RevivePlayer;
    }
}
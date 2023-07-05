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
using System.Collections;
using UnityEngine;
using Utils.Variables;
[RequireComponent(typeof(PlayerMaster))]
public class PlayerLives : MonoBehaviour
{
    #region Variable Private Inspector
    [SerializeField]
    private IntVariable lives;
    [SerializeField]
    private int maxlives;
    [SerializeField]
    private float timetoReSpawn = 1.8f;
    [SerializeField]
    public IntVariable varForExtraLife;
    [SerializeField]
    private int scoreForExtraLife;
    #endregion
    #region Variable Private References
    private PlayerMaster playerMaster;
    private GamePlayMaster gamePlay;
    private int _score;
    private int nextlive;
    #endregion

    private void Awake()
    {
        _score = varForExtraLife.Value;
    }

    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerDestroy += KillPlayer;
        playerMaster.EventPlayerReload += PlayerSetup;
    }
    /// <summary>
    /// Configura as referencias inicias
    /// </summary>
    /// 
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        gamePlay = GamePlayMaster.Instance;
    }
    /// <summary>
    /// Mata o jogador
    /// </summary>
    /// 
    private void KillPlayer()
    {
        AddLives(-1);
        if (lives.Value <= 0 && !GameManager.Instance.isGameOver)
        {
            gamePlay.RemovePlayer(playerMaster);
            if (gamePlay.GetAllPlayer().Count < 1)
            {
                GameManager.Instance.isGameOver = true;
            }
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(ReSpawn());
        }
    }
    /// <summary>
    /// Inicia os procedimento com uma pausa para reviver o jogador
    /// </summary>
    /// <returns></returns>
    /// 
    private IEnumerator ReSpawn()
    {
        yield return new WaitForSeconds(timetoReSpawn);
        if (GameManager.Instance.ShouldBeInGame)
        {
            playerMaster.CallEventPlayerReload();
        }
    }
    /// <summary>
    /// Configura as propriedades iniciais do jogador
    /// </summary>
    /// 
    private void PlayerSetup()
    {
        transform.position = playerMaster.GetStartPosition();
        if (GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().enabled = true;
        gamePlay.CallEventResetEnemys();
        gamePlay.UnPausePlayGame();
    }

    public void AddLives(int newlives)
    {
        if (maxlives > 0 && (lives.Value + newlives) > maxlives)
            lives.ApplyChange(maxlives);
        else
            lives.ApplyChange(newlives);
    }

    private void LateUpdate()
    {
        if (_score != varForExtraLife.Value)
        {
            int quant = varForExtraLife.Value / scoreForExtraLife;
            if (nextlive < quant && quant > 0)
            {
                AddLives(quant - nextlive);
                playerMaster.CallEventPlayerAddLive();
                nextlive = quant;
            }
            _score = varForExtraLife.Value;
        }
    }

    /// <summary>
    /// Executa quando desativa o objeto
    /// </summary>
    /// 
    private void OnDisable()
    {
        playerMaster.EventPlayerDestroy -= KillPlayer;
        playerMaster.EventPlayerReload -= PlayerSetup;
    }
}
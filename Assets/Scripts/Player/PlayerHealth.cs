/// <summary>
/// Namespace:      None
/// Class:          PlayerHealth
/// Description:    Controla a Saude e vida do jogador e todas as formas do jogador ser derrotado
/// Author:         Renato Innocenti                    Date: 26/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v1.0           Date: 26/03/2018       Description: morte por vida e hp como combustivel
/// Name: v1.1          Data: 29/03/2018        Description: separando as vidas nesse script
/// </summary>
///
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]
public class PlayerHealth : MonoBehaviour
{
    #region Variable Private Inspector
    [Header("Fuel")]
    [SerializeField]
    private bool autoDecreaseHP;
    [SerializeField]
    private int reduceFuelRate;
    [SerializeField]
    public int alertHP;
    [SerializeField]
    private AudioEventSample playerAlert;
    [SerializeField]
    [Range(0, 5)]
    private float reduceFuelCadency;

    #endregion
    #region Variable Private System
    private float nextDescesceFuel;
    private AudioSource audioSource;

    #endregion
    #region Variable Private References
    private PlayerMaster playerMaster;
    private Player playerStats;
    #endregion
    /// <summary>
    /// Executa quando ativar o objeto
    /// </summary>
    /// 
    private void OnEnable()
    {
        SetInitialReferences();
        PlayerSetup();
        playerMaster.EventIncresceHealth += IncreasceFuel;
        playerMaster.EventPlayerReload += PlayerSetup;
    }
    /// <summary>
    /// Configura as referencias inicias
    /// </summary>
    /// 
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        playerStats = playerMaster.playerSettings;
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// ativa a cada atualização de frame
    /// </summary>
    private void Update()
    {
        if (autoDecreaseHP && !GamePlayMaster.Instance.GODMODE)
        {
            AutoReduceHP(reduceFuelRate, reduceFuelCadency);
        }
    }

    private void AutoReduceHP(int reduce, float time)
    {
        if (playerMaster.ShouldPlayerBeReady && playerStats.actualHP.Value > 0 && Time.time > nextDescesceFuel)
        {
            nextDescesceFuel = Time.time + time;
            ReduceFuel(reduce);
            CheckHealth();
            Log(reduce);
        }
    }

    private void CheckHealth()
    {
        if (playerAlert != null && playerMaster.ShouldPlayerBeReady && playerStats.actualHP.Value <= alertHP && !playerAlert.isPlaying(audioSource))
        {
            playerAlert.Play(audioSource);
        }
        else if (playerAlert != null && playerMaster.ShouldPlayerBeReady && playerStats.actualHP.Value > alertHP && playerAlert.isPlaying(audioSource))
        {
            playerAlert.Stop(audioSource);
        }
    }

    private void Log(int reduce)
    {
        GamePlaySettings.Instance.fuelSpents += reduce;
    }

    /// <summary>
    /// Reduz o combustivel do player
    /// </summary>
    /// <param name="fuel">valor a reduzir</param>
    /// 
    public void ReduceFuel(int fuel)
    {
        playerStats.actualHP.ApplyChange(-fuel);
        if (playerStats.actualHP.Value <= 0)
        {
            playerStats.actualHP.SetValue(0);
            playerMaster.CallEventPlayerDestroy();
        }
    }
    /// <summary>
    /// Aumentar o combustivel
    /// </summary>
    /// <param name="fuel">valor para aumentar</param>
    public void IncreasceFuel(int fuel)
    {
        playerStats.actualHP.ApplyChange(fuel);
        if (playerStats.actualHP.Value > playerStats.maxHP)
        {
            playerStats.actualHP.SetValue(playerStats.maxHP);
        }
    }

    /// <summary>
    /// Configura as propriedades iniciais do jogador
    /// </summary>
    /// 
    private void PlayerSetup()
    {
        playerStats.actualHP.SetValue(playerStats.maxHP);
    }
    /// <summary>
    /// Executa quando desativa o objeto
    /// </summary>
    /// 
    private void OnDisable()
    {
        playerMaster.EventIncresceHealth -= IncreasceFuel;
        playerMaster.EventPlayerReload -= PlayerSetup;
    }
}
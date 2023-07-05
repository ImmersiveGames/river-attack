/// <summary>
/// Namespace:      None
/// Class:          PlayerCollider
/// Description:    Administra as colisões do jogador
/// Author:         Renato Innocenti                    Date: 26/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v1.0           Date: 26/03/2018       Description: v0.0
/// </summary>
///
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(Collider2D))]
public class PlayerCollider2D : MonoBehaviour
{
    #region Variable Private References
    private PlayerMaster playerMaster;
    private Collider2D mycol;
    #endregion

    /// <summary>
    /// Executa quando ativa o objeto
    /// </summary>
    /// 
    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerDestroy += ColliderOFF; // desliga o collider quando destroy o player
        playerMaster.EventPlayerReload += ColliderON; // liga o collider quando reinicia o player
    }
    /// <summary>
    /// Executa quando ativa o objeto
    /// </summary>
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        mycol = GetComponent<Collider2D>();
    }
    /// <summary>
    /// Executa quando o player atinge um objeto com trigger collider
    /// </summary>
    /// <param name="collision">objeto coledido</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(GameSettings.Instance.enemyTag) || collision.gameObject.CompareTag(GameSettings.Instance.wallTag))
        {
            GamePlayMaster.Instance.PausePlayGame();
            playerMaster.CallEventPlayerDestroy();
        }
    }
    /// <summary>
    /// ativa o collider
    /// </summary>
    /// 
    private void ColliderON()
    {
        mycol.enabled = true;
    }
    /// <summary>
    /// Desativa o Collider
    /// </summary>
    /// 
    private void ColliderOFF(Levels level)
    {
        ColliderOFF();
    }
    private void ColliderOFF()
    {
        mycol.enabled = false;
    }
    /// <summary>
    /// Executa quando desativa o objeto
    /// </summary>
    /// 
    private void OnDisable()
    {
        playerMaster.EventPlayerDestroy -= ColliderOFF;
        playerMaster.EventPlayerReload -= ColliderON;
    }
}

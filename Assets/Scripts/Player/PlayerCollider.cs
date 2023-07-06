using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerCollider : MonoBehaviour {

    #region Variable Private References
    private PlayerMaster playerMaster;
    private GamePlayMaster gamePlay;
    private Collider mycol;
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
        gamePlay.EventCompletePath += ColliderOFF;
    }
    /// <summary>
    /// Executa quando ativa o objeto
    /// </summary>
    private void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        playerMaster = GetComponent<PlayerMaster>();
        mycol = GetComponent<Collider>();
    }
    /// <summary>
    /// Executa quando o player atinge um objeto com trigger collider
    /// </summary>
    /// <param name="collision">objeto coledido</param>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag(GameSettings.Instance.enemyTag) || collision.transform.root.CompareTag(GameSettings.Instance.enemyTag) || collision.CompareTag(GameSettings.Instance.wallTag))
        {
            if (gamePlay.GODMODE) return;
            gamePlay.CallEventPausePlayGame();
            playerMaster.CallEventPlayerHit();
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
        gamePlay.EventCompletePath -= ColliderOFF;
    }
}

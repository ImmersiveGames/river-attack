using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PlayerCollider : MonoBehaviour {

    #region Variable Private References
    private PlayerMaster playerMaster;
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
    }
    /// <summary>
    /// Executa quando ativa o objeto
    /// </summary>
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        mycol = GetComponent<Collider>();
    }
    /// <summary>
    /// Executa quando o player atinge um objeto com trigger collider
    /// </summary>
    /// <param name="collision">objeto coledido</param>
    private void OnTriggerEnter(Collider collision)
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

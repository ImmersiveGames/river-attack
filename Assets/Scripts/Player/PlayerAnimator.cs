/// <summary>
/// Namespace:      None
/// Class:          PlayerAnimator
/// Description:    Controla todas as animações do jogador
/// Author:         Renato Innocenti                    Date: 26/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v1.0           Date: 26/03/2018       Description: Animação de movimento e explosão
/// </summary>
///
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour {
    #region Variables Private References
    private PlayerMaster playerMaster;
    private Animator animator;
    #endregion
    /// <summary>
    /// Executa quando ativa o objeto
    /// </summary>
    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventPlayerShoot += AnimationShoot;
        playerMaster.EventControllerMovement += AnimationMoviment;
        playerMaster.EventPlayerDestroy += AnimationExplode;
        playerMaster.EventPlayerReload += AnimationIdle;
    }

    /// <summary>
    /// Configura as referencias iniciais
    /// </summary>
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        animator = GetComponent<Animator>();
    }
    private void AnimationMoviment(Vector3 dir)
    {
        if (dir.x == 0 && dir.y ==0)
        {
            animator.SetBool("Moviment", false);
        }
        else
        {
            animator.SetBool("Moviment", true);
        }
        animator.SetFloat("DirX", dir.x);
        animator.SetFloat("DirY", dir.y);
    }
    private void AnimationIdle()
    {
        animator.SetBool("Moviment", false);
        animator.SetTrigger("ResetPlayer");
    }
    /// <summary>
    /// Ativa as animações de atirar
    /// </summary>
    ///
    private void AnimationShoot()
    {
        animator.SetTrigger("PlayerShot");
    }
    /// <summary>
    /// Ativa as animações de explodir
    /// </summary>
    ///
    private void AnimationExplode()
    {
        //animator.SetBool("PlayerDead", true);
        animator.SetBool("Moviment", false);
    }
    /// <summary>
    /// Executa quando desativa o objeto
    /// </summary>
    /// 
    private void OnDisable()
    {
        playerMaster.EventPlayerShoot -= AnimationShoot;
        playerMaster.EventControllerMovement -= AnimationMoviment;
        playerMaster.EventPlayerDestroy -= AnimationExplode;
        playerMaster.EventPlayerReload -= AnimationIdle;
    }
}

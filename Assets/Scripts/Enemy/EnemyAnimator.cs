using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
public class EnemyAnimator : MonoBehaviour {

    public string ExplosionTrigger;
    public string onMove;
    public string onFlip;

    protected EnemyMaster enemyMaster;
    protected Animator animator;
    protected GamePlayMaster gamePlay;

    protected virtual void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventDestroyEnemy += ExplodeAnimation;
        enemyMaster.EventMovimentEnemy += MovimentAnimation;
        enemyMaster.EventFlipEnemy += FlipAnimation;
        enemyMaster.EventChangeSkin += SetInitialReferences;
        gamePlay.EventResetEnemys += ResetAnimation;
    }


    protected virtual void SetInitialReferences()
    {
        gamePlay = GamePlayMaster.Instance;
        enemyMaster = GetComponent<EnemyMaster>();
        animator = GetComponentInChildren<Animator>();
    }

    private void MovimentAnimation(Vector3 pos)
    {
        if (animator != null && !string.IsNullOrEmpty(onMove) && animator.gameObject.activeSelf)
        {
            if (pos.x != 0)
            {
                animator.SetBool(onMove, true);
            }
            else
            {
                animator.SetBool(onMove, false);
            }
            //animator.SetFloat(MovimentFloat, pos.x * 10);
        }
    }

    private void FlipAnimation(Vector3 face)
    {
        if (animator != null && !string.IsNullOrEmpty(onFlip))
            animator.SetBool(onFlip, !animator.GetBool(onFlip));
    }

    private void ResetAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(onMove))
            animator.SetBool(onMove, false);
        if (animator != null && !string.IsNullOrEmpty(onFlip))
            animator.SetBool(onFlip, false);
    }

    protected virtual void ExplodeAnimation()
    {
        if (animator != null && !string.IsNullOrEmpty(ExplosionTrigger))
        {
            animator.SetBool(ExplosionTrigger, true);
        }
    }
    public void RemoveAnimation()
    {
        if (animator != null && GetComponent<SpriteRenderer>())
            GetComponent<SpriteRenderer>().enabled = false;
        if (animator != null && GetComponent<MeshRenderer>())
            GetComponent<MeshRenderer>().enabled = false;
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    protected virtual void OnDisable()
    {
        enemyMaster.EventDestroyEnemy -= ExplodeAnimation;
        enemyMaster.EventMovimentEnemy -= MovimentAnimation;
        enemyMaster.EventFlipEnemy -= FlipAnimation;
        enemyMaster.EventChangeSkin -= SetInitialReferences;
        gamePlay.EventResetEnemys -= ResetAnimation;
        
    }
}

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
        playerMaster.EventControllerMovement += AnimationMoviment;
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
        animator.SetFloat("DirX", dir.x);
        animator.SetFloat("DirY", dir.y);
    }

    private void OnDisable()
    {
        playerMaster.EventControllerMovement -= AnimationMoviment;
    }
}

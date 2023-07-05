using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIRapidFire : MonoBehaviour {

    private GamePlayMaster gamePlay;
    private Animator animator;

    private void OnEnable()
    {
        SetInitialreferences();
        //gamePlayPoweup.EventRapidFire += RapidFire;
        gamePlay.EventRapidFire += RapidFire;
    }

    private void SetInitialreferences()
    {
        gamePlay = GamePlayMaster.Instance;
        animator = GetComponent<Animator>();
    }

    public void RapidFire(bool active)
    {
        if (animator != null)
        {
            animator.SetBool("RapidFire",active);
        }
    }
}

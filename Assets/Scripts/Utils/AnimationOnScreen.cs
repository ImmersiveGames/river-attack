using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationOnScreen : MonoBehaviour {
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private string animationTrigger;
    [SerializeField]
    private float timeLate;
    [SerializeField]
    private Activation activationType;
    public enum Activation { OnEnable, OnVisible}

    private void OnEnable()
    {
        if (activationType != Activation.OnEnable) return;
        if (timeLate > 0)
        {
            Invoke("ShowAnimation", timeLate);
        }
        else
        {
            ShowAnimation();
        }
    }
    private void OnBecameVisible()
    {
        if (activationType != Activation.OnVisible) return;
        if (timeLate > 0)
        {
            Invoke("ShowAnimation", timeLate);
        }
        else
        {
            ShowAnimation();
        }
        
    }

    private void ShowAnimation()
    {
        if (animator != null && animationTrigger != null)
        {
            animator.SetTrigger(animationTrigger);
        }
    }
}

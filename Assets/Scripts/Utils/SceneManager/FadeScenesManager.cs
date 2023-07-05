using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScenesManager : Singleton<FadeScenesManager>
{
    [Header("Fade Animations")]
    [SerializeField]
    private GameObject panelFade;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AnimationClip animationFadeIn;
    [SerializeField]
    private AnimationClip animationFadeOut;
    [SerializeField]
    private float waitbetweenFades;

    private void OnEnable()
    {
        panelFade.SetActive(false);
    }

    public IEnumerator FadeOut()
    {
        animator.SetInteger("Fade", 1);
        StartCoroutine(FadeAudio(animationFadeIn.length - 0.2f));
        yield return new WaitForSeconds(animationFadeOut.length);
    }

    public IEnumerator FadeIn()
    {
        yield return new WaitForSeconds(waitbetweenFades);
        animator.SetInteger("Fade", 2);
        StartCoroutine(FadeAudio(animationFadeIn.length - 0.2f, true));
        yield return new WaitForSeconds(animationFadeIn.length);
        animator.SetInteger("Fade", 0);
    }
    private IEnumerator FadeAudio(float timer, bool reverse = false)
    {
        float start = (!reverse) ? 0.0F : 1.0F;
        float end = (!reverse) ? 1.0F : 0.0F;
        float i = 0.0F;
        float step = 1.0F / timer;

        while (i <= 1.0F)
        {
            i += step * Time.deltaTime;
            AudioListener.volume = Mathf.Lerp(start, end, i);
            yield return new WaitForSeconds(step * Time.deltaTime);
        }
    }
    protected override void OnDestroy() { }
}

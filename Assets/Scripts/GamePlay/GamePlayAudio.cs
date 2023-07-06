using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GamePlayAudio : Singleton<GamePlayAudio>
{
    public AudioMixerSnapshot[] audioMixerSnapshots;
    public enum LevelType { Grass = 4, Florest = 2, Antique = 0, Desert = 1, Ice = 3, Swamp = 7, HUB = 5, MainTheme = 6 }
    public LevelType levelType;
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private List<AudioEventSample> listBGM;
    private bool inFadeIN, inFadeOut;

    public void PauseBGM()
    {
        if (audioMixerSnapshots.Length > 1)
            audioMixerSnapshots[1].TransitionTo(0);
    }
    public void UnPauseBGM()
    {
        if (audioMixerSnapshots.Length > 0)
            audioMixerSnapshots[0].TransitionTo(0);
    }



    public void PlayBGM(LevelType ltype)
    {
        int i = (int)ltype;
        listBGM[i].Play(audioSource);
    }

    public void ChangeBGM(LevelType ltype, float time)
    {
        int i = (int)ltype;
        Debug.Log("Troca");
        StartCoroutine(PlayBGM(audioSource, listBGM[i], time));
    }

    public void ChangeBGM(AudioEventSample track, float time)
    {
        StartCoroutine(PlayBGM(audioSource, track, time));
    }

    private IEnumerator PlayBGM(AudioSource source, AudioEventSample track, float time)
    {
        if (source.isPlaying)
            yield return StartCoroutine(FadeAudio(source, time, source.volume, 0));
        track.Play(source);
    }

    public void StopBGM()
    {
        if (audioSource)
            audioSource.Stop();
    }

    private IEnumerator FadeAudio(AudioSource source, float timer, float starts, float ends)
    {
        float start = starts;
        float end = ends;
        float i = 0.0F;
        float step = 1.0F / timer;
        while (i <= 1.0F)
        {
            i += step * Time.deltaTime;
            source.volume = Mathf.Lerp(start, end, i);
            yield return new WaitForSeconds(step * Time.deltaTime);
        }
        if (end <= 0)
            source.Stop();
    }
    private void Update()
    {
        if (Time.timeScale <=0)
        {
            PauseBGM();
        }
        else
        {
            UnPauseBGM();
        }
    }
    protected override void OnDestroy() { }

}

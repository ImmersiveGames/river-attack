using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioMaster : MonoBehaviour
{
    [SerializeField]
    public AudioEventClip startBGM;
    [SerializeField]
    public AudioEventClip loopingBGM;
    [SerializeField]
    public AudioEventClip endBGM;
    [SerializeField]
    public AudioMixerGroup audioMixerGroup;
    [SerializeField]
    public bool playOnEnable = false;

    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioMixerGroup != null) audioSource.outputAudioMixerGroup = audioMixerGroup;

    }

    private void Start()
    {
        if (playOnEnable)
            PlayBGM();
    }

    public void PlayBGM()
    {
        if (audioSource && loopingBGM != null)
        {
            StartCoroutine(StartPlay(audioSource, startBGM, loopingBGM));
        }

    }

    public void StopBGM()
    {
        if (audioSource && endBGM != null)
        {
            StartCoroutine(Stop(audioSource, endBGM));
        }
    }
    //private void OnDisable()
    //{ 
    //    if(playOnEnable)
    //    StopBGM();
    //}
    public IEnumerator StartPlay(AudioSource source, AudioEventClip startEventClip, AudioEventClip loopEventClip)
    {
        if (startEventClip.audioClip != null)
        {
            source.clip = startEventClip.audioClip;
            source.volume = Random.Range(startEventClip.volume.minValue, startEventClip.volume.maxValue);
            source.pitch = Random.Range(startEventClip.pitch.minValue, startEventClip.pitch.maxValue);
            source.loop = false;
            source.Play();
            while (source.isPlaying)
            {
                yield return null;
            }
        }
        source.clip = loopEventClip.audioClip;
        source.volume = Random.Range(loopEventClip.volume.minValue, loopEventClip.volume.maxValue);
        source.pitch = Random.Range(loopEventClip.pitch.minValue, loopEventClip.pitch.maxValue);
        source.loop = loopEventClip.loop;
        source.Play();
        yield return null;
    }

    public IEnumerator Stop(AudioSource source, AudioEventClip endEventClip)
    {
        source.clip = endEventClip.audioClip;
        source.volume = Random.Range(endEventClip.volume.minValue, endEventClip.volume.maxValue);
        source.pitch = Random.Range(endEventClip.pitch.minValue, endEventClip.pitch.maxValue);
        source.loop = false;
        source.Play();
        while (source.isPlaying)
        {
            yield return null;
        }
        source.Stop();
    }
}

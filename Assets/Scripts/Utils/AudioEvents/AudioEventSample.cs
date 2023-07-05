using System.Collections;
using UnityEngine.Audio;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioEventSample", menuName = "Audio Events/Sample Audio", order = 1)]
public class AudioEventSample : AudioEvent
{
    [SerializeField]
    public AudioEventClip audioSample;
    [SerializeField]
    public AudioMixerGroup audioMixerGroup;

    //TODO: Habilitar para Grupo de MIXagem;

    public override void Play(AudioSource source)
    {
        if (audioSample.audioClip == null) return;
        source.clip = audioSample.audioClip;
        source.volume = Random.Range(audioSample.volume.minValue, audioSample.volume.maxValue);
        source.pitch = Random.Range(audioSample.pitch.minValue, audioSample.pitch.maxValue);
        source.loop = audioSample.loop;
        source.Play();
    }

    public override void Stop(AudioSource source)
    {
        source.Stop();
    }

    public void UpdateChangePith(AudioSource source, float start, float end)
    {
        source.pitch = Mathf.Clamp(Time.time, start, end);
    }
}

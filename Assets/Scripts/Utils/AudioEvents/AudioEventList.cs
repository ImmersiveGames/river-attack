using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioEventList", menuName = "Audio Events/List Audio", order = 1)]

public class AudioEventList : AudioEvent
{

    [SerializeField]
    public List<AudioEventClip> audioList;

    public void Play(AudioSource source, string sampleName, float volume, float pitch)
    {
        if (audioList.Count == 0) return;
        AudioEventClip clip = audioList.Find(x => x.name == sampleName);
        if (clip != null)
        {
            source.clip = clip.audioClip;
            source.volume = volume;
            source.pitch = pitch;
            source.Play();
        }
    }
    public void Play(AudioSource source, string sampleName)
    {
        if (audioList.Count == 0) return;
        AudioEventClip clip = audioList.Find(x => x.name == sampleName);
        if (clip != null)
        {
            source.clip = clip.audioClip;
            source.volume = Random.Range(clip.volume.minValue, clip.volume.maxValue);
            source.pitch = Random.Range(clip.pitch.minValue, clip.pitch.maxValue);
            source.Play();
        }
    }
    public override void Play(AudioSource source)
    {
        if (audioList.Count == 0) return;
        AudioEventClip clip = audioList[Random.Range(0, audioList.Count)];
        source.clip = clip.audioClip;
        source.volume = Random.Range(clip.volume.minValue, clip.volume.maxValue);
        source.pitch = Random.Range(clip.pitch.minValue, clip.pitch.maxValue);
        source.Play();
    }

    public override void Stop(AudioSource source)
    {
        source.Stop();
    }
}

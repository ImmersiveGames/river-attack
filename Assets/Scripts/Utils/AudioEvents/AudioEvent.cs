using UnityEngine;
using Utils.Variables;

public abstract class AudioEvent : ScriptableObject {
    public abstract void Play(AudioSource source);
    public abstract void Stop(AudioSource source);
}
[System.Serializable]
public class AudioEventClip
{
    [SerializeField]
    public string name;
    [SerializeField]
    public AudioClip audioClip;
    [MinMaxRange(0, 1)]
    public FloatRanged volume;
    [MinMaxRange(-3, 3)]
    public FloatRanged pitch;
    public bool loop;
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]
public class UISoundsButton : MonoBehaviour, ISelectHandler , IPointerEnterHandler{

    [SerializeField]
    private AudioEventSample onSelect;
    [SerializeField]
    public AudioMixerGroup audioMixerGroup;

    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioMixerGroup != null) audioSource.outputAudioMixerGroup = audioMixerGroup;
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelect.PlayOnShot(audioSource);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        onSelect.PlayOnShot(audioSource);
    }
}

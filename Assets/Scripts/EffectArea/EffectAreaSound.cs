using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaSound : EnemySound {

    private EffectAreaMaster effectAreaMaster;
    [SerializeField]
    private AudioEventSample effectAreaSound;
    [SerializeField]
    private AudioEventSample effectAreaExitSound;

    protected override void OnEnable()
    {
        base.OnEnable();
        effectAreaMaster.EventAreaEffect += SoundAreaEffect;
        effectAreaMaster.EventExitAreaEffect += StopSound;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        effectAreaMaster.EventAreaEffect -= SoundAreaEffect;
        effectAreaMaster.EventExitAreaEffect -= StopSound;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        effectAreaMaster = GetComponent<EffectAreaMaster>();
    }

    private void SoundAreaEffect()
    {
        if (audioSource != null && effectAreaSound != null && !audioSource.isPlaying)
            effectAreaSound.Play(audioSource);
    }

    private void StopSound()
    {
        if (audioSource != null && effectAreaSound != null && audioSource.isPlaying)
        {
            effectAreaExitSound.Play(audioSource);
            //effectAreaSound.Stop(audioSource);
        }
            
    }
}

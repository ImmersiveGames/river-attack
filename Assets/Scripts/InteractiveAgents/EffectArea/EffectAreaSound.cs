using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAreaSound : EnemySound {

    private EffectAreaMaster effectAreaMaster;
    [SerializeField]
    private AudioEventSample effectAreaSound;

    protected override void OnEnable()
    {
        base.OnEnable();
        effectAreaMaster.AreaEffectEvent += SoundAreaEffect;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        effectAreaMaster.AreaEffectEvent -= SoundAreaEffect;
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
}

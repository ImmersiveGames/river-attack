using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollectiblesMaster))]
public class CollectiblesSound : EnemySound
{
    [SerializeField]
    private AudioEventSample collectSound;

    protected CollectiblesMaster collectiblesMaster;

    protected override void OnEnable()
    {
        base.OnEnable();
        collectiblesMaster.CollectibleEvent += CollectSound;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        collectiblesMaster.CollectibleEvent -= CollectSound;
    }

    protected override void SetInitialReferences()
    {
        base.SetInitialReferences();
        collectiblesMaster = GetComponent<CollectiblesMaster>();
    }

    private void CollectSound(PlayerMaster playerMaster)
    {
        if (audioSource != null && collectSound != null)
            collectSound.Play(audioSource);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyMaster))]
[RequireComponent(typeof(AudioSource))]
public class EnemySound : MonoBehaviour {

    protected EnemyMaster enemyMaster;
    protected AudioSource audioSource;
    [SerializeField]
    private AudioEventSample enemyExplodeAudio;

    protected virtual void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventDestroyEnemy += ExplodeSound;
    }
    protected virtual void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        audioSource = GetComponent<AudioSource>();
    }
    protected virtual void OnDisable()
    {
        enemyMaster.EventDestroyEnemy -= ExplodeSound;
    }
    private void ExplodeSound()
    {
        if (audioSource != null && enemyExplodeAudio != null)
            enemyExplodeAudio.Play(audioSource);
    }
}

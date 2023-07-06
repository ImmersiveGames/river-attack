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

    private GamePlayMaster gamePlay;

    protected virtual void OnEnable()
    {
        SetInitialReferences();
        enemyMaster.EventDestroyEnemy += ExplodeSound;
        gamePlay.EventResetEnemys += StopSound;
    }
    protected virtual void SetInitialReferences()
    {
        enemyMaster = GetComponent<EnemyMaster>();
        audioSource = GetComponent<AudioSource>();
        gamePlay = GamePlayMaster.Instance;
    }
    protected virtual void OnDisable()
    {
        enemyMaster.EventDestroyEnemy -= ExplodeSound;
        gamePlay.EventResetEnemys -= StopSound;
    }
    private void StopSound()
    {
        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }
    private void ExplodeSound()
    {
        if (audioSource != null && enemyExplodeAudio != null)
            enemyExplodeAudio.Play(audioSource);
    }
}

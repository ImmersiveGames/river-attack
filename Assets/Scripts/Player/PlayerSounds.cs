/// <summary>
/// Namespace:      None
/// Class:          PlayerSounds
/// Description:    Controla a emissão de som do player
/// Author:         Renato Innocenti                    Date: 26/03/2018
/// Notes:          copyrights 2017-2018 (c) immersivegames.com.br - contato@immersivegames.com.br       
/// Revision History:
/// Name: v1.0           Date: 26/03/2018       Description: Sons Para Motor, Explosão e Tiro
/// </summary>
///
using System.Collections;
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    #region Variable Private Inspector
    [SerializeField]
    private AudioEventSample playerEngineAudio;
    [SerializeField]
    private AudioEventSample playerStartAccelEngineAudio;
    [SerializeField]
    private AudioEventSample playerAceceEngineAudio;
    [SerializeField]
    private AudioEventSample playerDeaceceEngineAudio;
    [SerializeField]
    [Range(.1f, 3)]
    private float enginePitchDown = .5f;
    [SerializeField]
    private AudioEventSample playerExplosion;
    #endregion

    #region Variable Private References
    private PlayerMaster playerMaster;
    private AudioSource audioSource;
    private GamePlayMaster gamePlay;

    #endregion

    private void OnEnable()
    {
        SetInitialReferences();
        playerMaster.EventControllerMovement += SoundEngine;
        playerMaster.EventPlayerDestroy += SoundExplosion;
        gamePlay.EventPausePlayGame += SoundStop;
        gamePlay.EventCompletePath += SoundStop;
    }

    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        audioSource = GetComponent<AudioSource>();
        gamePlay = GamePlayMaster.Instance;
    }

    private void SoundEngine(Vector3 dir)
    {
        if (playerEngineAudio != null && playerMaster.ShouldPlayerBeReady)
        {
            if (dir.y > 0 && playerMaster.playerState != PlayerMaster.PlayerState.Accelerate)
            {
                playerMaster.playerState = PlayerMaster.PlayerState.Accelerate;
                StartCoroutine(ChangeEngine(playerStartAccelEngineAudio, playerAceceEngineAudio));
            }
            if (dir.y < 0 && playerMaster.playerState != PlayerMaster.PlayerState.Reduce)
            {
                playerMaster.playerState = PlayerMaster.PlayerState.Reduce;
                playerEngineAudio.UpdateChangePith(audioSource, playerEngineAudio.audioSample.pitch.maxValue, enginePitchDown);
            }
            else if(dir.y == 0 && playerMaster.playerState != PlayerMaster.PlayerState.None)
            {
                if(playerMaster.playerState == PlayerMaster.PlayerState.Accelerate)
                    StartCoroutine(ChangeEngine(playerDeaceceEngineAudio, playerEngineAudio));
                if (playerMaster.playerState == PlayerMaster.PlayerState.Reduce)
                    playerEngineAudio.UpdateChangePith(audioSource, audioSource.pitch, playerEngineAudio.audioSample.pitch.maxValue);
                playerMaster.playerState = PlayerMaster.PlayerState.None;
            }
            else if (playerEngineAudio != null && playerMaster.ShouldPlayerBeReady && !audioSource.isPlaying && playerMaster.playerState == PlayerMaster.PlayerState.None)
            {
                //StopAllCoroutines();
                playerEngineAudio.Play(audioSource);
            }         
        }
    }

    private IEnumerator ChangeEngine(AudioEventSample audioStart, AudioEventSample audiofix)
    {
        audioStart.Play(audioSource);
        while (audioSource.isPlaying)
        {
            yield return null;
        }
        audiofix.Play(audioSource);
    }

    private void SoundExplosion()
    {
        if (playerExplosion != null)
        {
            StopAllCoroutines();
            playerExplosion.Play(audioSource);
        }
    }

    private void SoundStop()
    {
        StopAllCoroutines();
        audioSource.Stop();
    }

    private void OnDisable()
    {
        playerMaster.EventControllerMovement -= SoundEngine;
        playerMaster.EventPlayerDestroy -= SoundExplosion;
        gamePlay.EventPausePlayGame -= SoundStop;
    }
}
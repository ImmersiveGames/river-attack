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
using UnityEngine;
[RequireComponent(typeof(PlayerMaster))]
[RequireComponent(typeof(AudioSource))]
public class PlayerSounds : MonoBehaviour
{
    #region Variable Private Inspector
    [SerializeField]
    private AudioEventSample playerEngineAudio;
    [SerializeField]
    [Range(.1f, 3)]
    private float enginePitchUp = 1.8f;
    [SerializeField]
    [Range(.1f, 3)]
    private float enginePitchDown = .5f;
    [SerializeField]
    private AudioEventSample playerExplosion;
    [SerializeField]
    private AudioEventSample playerAlert;
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
        playerMaster.EventPlayerReload += SoundStop;
        playerMaster.EventLowHealth += SoundAlert;
        gamePlay.EventPauseGame += SoundStop;
        gamePlay.EventCompleteMission += SoundStop;
    }
 
    private void SetInitialReferences()
    {
        playerMaster = GetComponent<PlayerMaster>();
        audioSource = GetComponent<AudioSource>();
        gamePlay = GamePlayMaster.Instance;
    }

    private void SoundEngine(Vector3 dir)
    {
        if (!playerMaster.ShouldPlayerBeReady)
            SoundStop();
        if (playerEngineAudio != null && playerMaster.ShouldPlayerBeReady)
        {
            if (dir.y > 0)
                playerEngineAudio.UpdateChangePith(audioSource, playerEngineAudio.audioSample.pitch.maxValue, enginePitchUp);
            else if (dir.y < 0)
                playerEngineAudio.UpdateChangePith(audioSource, playerEngineAudio.audioSample.pitch.maxValue, enginePitchDown);
            else
                playerEngineAudio.UpdateChangePith(audioSource, audioSource.pitch, playerEngineAudio.audioSample.pitch.maxValue);
        }
        if (playerEngineAudio != null && playerMaster.ShouldPlayerBeReady && !audioSource.isPlaying)
        {
            playerEngineAudio.Play(audioSource);
        }
        
    }

    private void SoundExplosion()
    {
        //audioSource.Stop();
        if (playerExplosion != null)
        {
            playerExplosion.Play(audioSource);
        }
    }

    private void SoundStop()
    {
        audioSource.Stop();
    }

    private void SoundAlert(int health)
    {
        if (playerAlert != null && playerMaster.ShouldPlayerBeReady && !audioSource.isPlaying)
        {
            playerAlert.Play(audioSource);
        }
    }

    private void OnDisable()
    {
        playerMaster.EventControllerMovement -= SoundEngine;
        playerMaster.EventPlayerDestroy -= SoundExplosion;
        playerMaster.EventPlayerReload -= SoundStop;
        playerMaster.EventLowHealth += SoundAlert;
        gamePlay.EventPauseGame -= SoundStop;
        gamePlay.EventCompleteMission -= SoundStop;
    }
}
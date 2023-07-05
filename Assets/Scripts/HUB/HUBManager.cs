using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class HUBManager : MonoBehaviour
{
    [SerializeField]
    private HUBSettings hubSettings;
    [SerializeField]
    private float waitMoveCam;
    private HUBMaster hubMaster;
    private HUBCameraMaster Cam;


    private IEnumerator Start()
    {
        hubMaster = HUBMaster.Instance;
        Cam = HUBCameraMaster.Instance;
        SetHubConfig();
        while (!LoadSceneManager.Instance.completeLoad)
        {
            yield return null;
        }
        StartCoroutine(HUBLoop());
    }

    private IEnumerator HUBLoop()
    {
        
        yield return StartCoroutine(ShouldBeGameOver());
        yield return StartCoroutine(HaveBeatALevel());
        yield return StartCoroutine(HaveBeatGame());
        //yield return StartCoroutine(HaveUnlockALevel());
        yield return StartCoroutine(ShouldBeInHUB());
        yield return null;
    }

    private void SetHubConfig()
    {
        
    }

    private IEnumerator ShouldBeInHUB()
    {
        Debug.Log("Agora eu devo poder operar a HUB");
        yield return null;
    }

    private IEnumerator HaveBeatGame()
    {
        Debug.Log("verificar se terminei o jogo ;)");
        yield return null;
    }


    private IEnumerator HaveBeatALevel()
    {
        Debug.Log("Venci essa missão e devo prosseguir?");

        if(hubSettings.BeatNewLevel == true)
        {
            Debug.Log("Bati um nivel");
            Levels levelbeat = hubSettings.GetLevelBeat();
            Transform pos = hubMaster.GetIconTransform(levelbeat);
            HUBIconMaster icone = hubMaster.GetIconMaster(levelbeat);
            icone.LevelBeat();
            hubMaster.CallEventBeatLevel(levelbeat);
            yield return new WaitForSeconds(icone.GetTimeAnimationFinish + waitMoveCam);
            hubSettings.AddBeatLevel(levelbeat);
            yield return null;
            // Desbloquear levels?
            List<Levels> listPreiv = hubMaster.GetPreviousLevelOf(levelbeat);
            if (listPreiv != null || listPreiv.Count > 0)
            {
                int length = listPreiv.Count;
                for (int i = 0; i < length; i++)
                {
                    Transform upos = hubMaster.GetIconTransform(listPreiv[i]);
                    HUBIconMaster uicone = hubMaster.GetIconMaster(listPreiv[i]);
                    yield return StartCoroutine(Cam.MoveCam(upos.position, hubSettings.timeToMoveCam, true));
                    uicone.UnLockedLevel();
                    yield return new WaitForSeconds(uicone.GetTimeAnimationUnlock + waitMoveCam);
                    yield return null;
                }
                yield return StartCoroutine(Cam.MoveCam(pos.position, hubSettings.timeToMoveCam, true));
            }
        }
        
        yield return null;
    }

    private IEnumerator ShouldBeGameOver()
    {
        Debug.Log("Estou em gameover?");
        if (!GameManager.Instance.isGameOver) yield break;
        Debug.Log("Estou em game over, Não deveria aparecer aqui");
        // TODO: CallGameOver;
        yield return null;
    }
}

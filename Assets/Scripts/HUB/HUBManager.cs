using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBManager : MonoBehaviour
{
    private HUBSettings hubSettings;
    private HUBMaster hubMaster;
    private GameManager gameManager;
    private GameManagerFirebase firebase;
    [HideInInspector]
    public static bool isCompleting, isUnloked;

    private void OnEnable()
    {
        gameManager = GameManager.Instance;
        hubMaster = HUBMaster.Instance;
        hubSettings = HUBSettings.Instance;
        firebase = gameManager.firebase;
        hubMaster.hubPause = true;
    }

    private IEnumerator Start()
    {
        while (!LoadSceneManager.Instance.completeLoad)// && firebase.dependencyStatus != Firebase.DependencyStatus..FBStatus.None)
        {
            yield return null;
        }
        if (firebase == null || firebase.dependencyStatus != Firebase.DependencyStatus.Available)
        {
            firebase = new GameManagerFirebase();
            yield return null;
        }
        StartCoroutine(HUBLoop());
    }

    private IEnumerator HUBLoop()
    {

        MainSetup();
        yield return StartCoroutine(CheckLevelBeat());
        yield return StartCoroutine(CheckGameBeat());
        hubMaster.hubPause = false;
    }

    private IEnumerator CheckLevelBeat()
    {
        //Complete Mission
        if (HaveBeatActualLevel() && !HaveAlreadyComplete())
        {
            hubMaster.hubPause = true;
            isCompleting = true;
            hubMaster.CallEventCompleteMission(gameManager.actualLevel);
            GameManagerSaves.Instance.SaveLevelComplete("HubLevel", hubSettings.levelHubComplete);
            GameManagerSaves.Instance.SaveLevelComplete("levelComplete", hubSettings.levelHubComplete);
            while (isCompleting)
            {
                yield return null;
            }
            if (!gameManager.actualLevel.beatGame)
            {
                if (hubSettings.levelToUnlock.Count > 0)
                {
                    //Unlock
                    yield return new WaitForSeconds(1);
                    int length = hubSettings.levelToUnlock.Count;
                    for (int i = length - 1; i >= 0; i--)
                    {
                        isUnloked = true;
                        yield return StartCoroutine(HUBCameraMaster.Instance.MoveCam(hubSettings.levelToUnlock[i].levelIconPos, hubMaster.timeMoveCam, true));
                        hubMaster.CallEventUnlockMission(hubSettings.levelToUnlock[i]);
                        while (isUnloked)
                        {
                            yield return null;
                        }
                    }
                    yield return StartCoroutine(HUBCameraMaster.Instance.MoveCam(hubMaster.PlayerIcon.transform.position, hubMaster.timeMoveCam, true));
                    hubSettings.levelToUnlock.Clear();
                    isUnloked = false;
                }
            }
            else
            {
                gameManager.isGameBaet = true;
            }
        }
        if (HaveAlreadyComplete() && gameManager.actualLevel.beatGame) gameManager.isGameBaet = true;
    }

    private IEnumerator CheckGameBeat()
    {
        if (gameManager.isGameBaet == true)
        {
            //Debug.Log("Bati o jogo");
            yield return new WaitForSeconds(3);
            LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes(3).sceneId);
            yield return null;
        }
    }

    private void MainSetup()
    {
        hubSettings.levelHubComplete = GameManagerSaves.Instance.LoadLevelComplete("HubLevel");
    }

    private bool HaveBeatActualLevel()
    {
        if (gameManager.levelsFinish.Contains(gameManager.actualLevel))
            return true;
        return false;
    }

    private bool HaveAlreadyComplete()
    {
        if (hubSettings.levelHubComplete.Contains(gameManager.actualLevel))
            return true;
        return false;
    }
}
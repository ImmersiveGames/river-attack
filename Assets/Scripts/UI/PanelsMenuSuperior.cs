using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelsMenuSuperior : MonoBehaviour
{
    [SerializeField]
    private GameObject objPause, objPlay, objExit, objPanelExit;
    private Button btnPause, btnPlay, btnExit;
    [SerializeField]
    private Toggle audioToggle;

    private GamePlayMaster gamePlay;

    private void Awake()
    {
        audioToggle.isOn = GameManager.muteAudio;
    }

    private void OnEnable()
    {
        SetInitialReferences();
        gamePlay.EventUnPausePlayGame += EnablePauseButtom;
        gamePlay.EventPausePlayGame += DisablePauseButtom;
    }

    private void SetInitialReferences()
    {
        objPlay.SetActive(false);
        objExit.SetActive(false);
        gamePlay = GamePlayMaster.Instance;
        btnPause = objPause.GetComponent<Button>();
        audioToggle.onValueChanged.RemoveAllListeners();
        audioToggle.isOn = GameManager.muteAudio;
        audioToggle.onValueChanged.AddListener(ToggleAudioMute);
    }

    public void ToggleAudioMute(bool arg0)
    {
        GameManager.muteAudio = !GameManager.muteAudio;
        AudioListener.pause = GameManager.muteAudio;
    }

    private void EnablePauseButtom()
    {
        btnPause.interactable = true;
    }

    private void DisablePauseButtom()
    {
        btnPause.interactable = false;
    }

    public void ButtonPauseGame()
    {
        objPause.SetActive(false);
        objPlay.SetActive(true);
        objExit.SetActive(true);
        //Time.timeScale = 0;
        GameManager.Instance.TogglePauseGame();
        gamePlay.CallEventPausePlayGame();
    }

    public void ButtonPlayGame()
    {
        objPause.SetActive(true);
        objPlay.SetActive(false);
        objExit.SetActive(false);
        GameManager.Instance.TogglePauseGame();
        //Time.timeScale = 1;
        gamePlay.CallEventUnPausePlayGame();
    }

    public void ButtonExitGame()
    {
        objPause.SetActive(true);
        objPlay.SetActive(false);
        objExit.SetActive(false);
        objPanelExit.SetActive(true);

    }
    public void ButtonQuitPlayGame()
    {
        gamePlay.SaveAllPlayers();
        objPanelExit.SetActive(false);
        Time.timeScale = 1;
        if (GameSettings.Instance.gameMode.modeId == GameSettings.Instance.GetGameModes("Classic").modeId)
            LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("MainMenu").sceneId);
        else
            LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes("HUD").sceneId);
        gamePlay.CallEventUnPausePlayGame();
    }

    private void OnDisable()
    {
        gamePlay.EventUnPausePlayGame -= EnablePauseButtom;
        gamePlay.EventPausePlayGame -= DisablePauseButtom;
    }
}
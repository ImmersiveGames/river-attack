using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelsMaster : MonoBehaviour
{
    public GameObject[] menus;
    public bool firstMenuStartEnable;
    public GameObject objBackButton;
    public Button backButton;
    public GameObject imgInternet;
    private List<int> navegaMenu;

    private GameSettings gameSettings;

    protected virtual void OnEnable()
    {
        ClearMenu();
        navegaMenu = new List<int>();
        if (firstMenuStartEnable)
        {
            menus[0].SetActive(true);
            navegaMenu.Add(0);
        }
        if (objBackButton)
            objBackButton.SetActive(false);
        gameSettings = GameSettings.Instance;
    }

    private void Start()
    {
        CheckInternetConnection();
    }

    public void ChangeMenu(int nextPointer)
    {
        ClearMenu();
        menus[nextPointer].SetActive(true);
        if (backButton)
            BackButton(nextPointer);
    }

    private void ClaerBackButton()
    {
        if (backButton)
            backButton.onClick.RemoveAllListeners();
    }

    public void BackButton(int nextPointer)
    {
        if (!navegaMenu.Contains(nextPointer))
            navegaMenu.Add(nextPointer);
        else if (navegaMenu.Count >= 2)
            navegaMenu.RemoveAt(navegaMenu.Count - 1);

        bool btnactive = (navegaMenu.Count > 1) ? true : false;
        if (objBackButton)
            objBackButton.SetActive(btnactive);
        ClaerBackButton();
        if (navegaMenu.Count >= 2)
            backButton.onClick.AddListener(() => ChangeMenu(navegaMenu[navegaMenu.Count - 2]));
        else
            backButton.onClick.AddListener(() => ChangeMenu(navegaMenu[0]));
    }
    public void ButtonQuitGame()
    {
        Application.Quit();
    }
    public void ButtonPlayClassicMode()
    {
        gameSettings.ChangeGameMode(0);
        gameSettings.ChangeGameScene(gameSettings.GetGameScenes("PlayGame").sceneId);
    }
    public void ButtonPlayMissionMode()
    {
        gameSettings.ChangeGameMode(0);
        gameSettings.ChangeGameScene(gameSettings.GetGameScenes("HUD").sceneId);
    }
    private void ClearMenu()
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(false);
    }

    public void ToggleAudioMute()
    {
        GameManager.muteAudio = !GameManager.muteAudio;
        AudioListener.pause = GameManager.muteAudio;
    }

    private void CheckInternetConnection()
    {
        if (imgInternet != null)
        {
            if (!InternetCheck.InternetConnection)
                imgInternet.SetActive(true);
            else
                imgInternet.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] mainMenus;
    [SerializeField]
    private GameObject menuBack;

    private NavegationManager navegation;

    private void OnEnable()
    {
        navegation = new NavegationManager(mainMenus, menuBack, true);
    }

    public void ChangeMenu(int indexMenu)
    {
        navegation.ChangeMenu(indexMenu);
    }

   
    public void ButtonQuit()
    {
        navegation.InitMenu();
        LoadSceneManager.Instance.LoadScene(GameSettings.Instance.GetGameScenes(0).sceneId);
    }
}

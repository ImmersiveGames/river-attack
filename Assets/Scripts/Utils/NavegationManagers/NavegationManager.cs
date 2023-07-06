using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavegationManager
{

    private GameObject[] menus;
    private GameObject backButton;
    private List<int> navegaMenu;
    private bool firstMenuStartEnable;

    UnityEngine.UI.Button back;
    public NavegationManager(GameObject[] listMenus, GameObject backObj, bool startEnable = false)
    {
        menus = listMenus;
        backButton = backObj;
        firstMenuStartEnable = startEnable;
        InitMenu();
    }

    public void InitMenu()
    {
        ClaerBackButton();
        navegaMenu = new List<int>();
        ChangeMenu();
        if (firstMenuStartEnable)
        {
            menus[0].SetActive(true);
            navegaMenu.Add(0);
        }
        if (backButton)
            backButton.SetActive(false);
    }

    private void ClearMenu()
    {
        for (int i = 0; i < menus.Length; i++)
            menus[i].SetActive(false);
    }

    public void ChangeMenu(int nextPointer = 0)
    {
        ClearMenu();
        menus[nextPointer].SetActive(true);
        if (backButton)
            BackButton(nextPointer);
    }

    private void ClaerBackButton()
    {
        if (backButton)
        {
            back = backButton.GetComponentInChildren<UnityEngine.UI.Button>();
            back.onClick.RemoveAllListeners();
        }
    }

    public void BackButton(int nextPointer)
    {
        if (!navegaMenu.Contains(nextPointer))
            navegaMenu.Add(nextPointer);
        else if (navegaMenu.Count >= 2)
            navegaMenu.RemoveAt(navegaMenu.Count - 1);
        ClaerBackButton();
        if (navegaMenu.Count >= 2)
            back.onClick.AddListener(() => ChangeMenu(navegaMenu[navegaMenu.Count - 2]));
        else
            back.onClick.AddListener(() => ChangeMenu(navegaMenu[0]));
        bool btnactive = (navegaMenu.Count > 2) ? true : false;
        if (backButton)
            backButton.SetActive(btnactive);
    }
}

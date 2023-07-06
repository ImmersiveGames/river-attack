using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStartNavegation : MonoBehaviour
{

    public GameObject[] menus;
    public GameObject objBackButton;
    public Button backButton;

    private List<int> navegaMenu;

    private void Awake()
    {
        ClearMenu();
        menus[0].SetActive(true);
        navegaMenu = new List<int>();
        navegaMenu.Add(0);
        objBackButton.SetActive(false);
    }

    public void ChangeMenu(int nextPointer)
    {
        ClearMenu();
        menus[nextPointer].SetActive(true);
        BackButton(nextPointer);
    }

    private void ClaerBackButton()
    {
        backButton.onClick.RemoveAllListeners();
    }

    public void BackButton(int nextPointer)
    {
        if (!navegaMenu.Contains(nextPointer))
            navegaMenu.Add(nextPointer);
        else if (navegaMenu.Count >= 2)
            navegaMenu.RemoveAt(navegaMenu.Count - 1);

        bool btnactive = (navegaMenu.Count > 1) ? true : false;
        objBackButton.SetActive(btnactive);
        ClaerBackButton();
        if (navegaMenu.Count >= 2)
            backButton.onClick.AddListener(() => ChangeMenu(navegaMenu[navegaMenu.Count - 2]));
        else
            backButton.onClick.AddListener(() => ChangeMenu(navegaMenu[0]));        
    }

    private void ClearMenu()
    {
        for (int i = 0; i < menus.Length; i++)
        {
            menus[i].SetActive(false);
        }
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

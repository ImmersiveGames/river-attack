using System;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour {

    private LoadSceneManager loadSceneManager;

    private void OnEnable()
    {
        SetInitialReferences();
    }
    void SetInitialReferences()
    {
        try
        {
            loadSceneManager = FindObjectOfType<LoadSceneManager>();
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            loadSceneManager = null;
        }
    }
    public void ChangeScene(int nScene)
    {
        if (loadSceneManager != null)
        {
            loadSceneManager.LoadScene(nScene);
        }
    }

    public void ResetScene()
    { 
        if (loadSceneManager != null)
        {
            loadSceneManager.LoadScene(2);
        }
    }
}

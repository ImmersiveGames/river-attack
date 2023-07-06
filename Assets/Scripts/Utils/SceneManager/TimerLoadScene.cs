using System;
using UnityEngine;

public class TimerLoadScene : MonoBehaviour {

    public float waitTimeInSaconds;
    public int loadScene;
    public int[] skipScene;

    private LoadSceneManager loadSceneManager;

    private void OnEnable()
    {
        SetInitialReferences();
    }

    private void Start()
    {
        InitTimer();
    }
    void SetInitialReferences()
    {
        try
        {
            loadSceneManager = LoadSceneManager.Instance;
        }
        catch (Exception e)
        {
            Debug.LogException(e, this);
            loadSceneManager = null;
        }
    }
    public void InitTimer()
    {
        //TODO: Fazer um inicializador para poder saltar cenas (não sei se é necessário)
        if (loadSceneManager != null)
        {
            if (Array.IndexOf(skipScene, loadScene) >= 0)
                loadScene++;
            loadSceneManager.LoadScene(loadScene, waitTimeInSaconds);
        }
    }
}

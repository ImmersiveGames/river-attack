using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/// <summary>
/// Namespace:      Tools
/// Class:          LoadSceneManager
/// Description:    Control All Scane transitions.
/// Author:         Renato Innocenti                    Date: 05/26/2018
/// Notes:          Attach on gameObject with control scenes (ScaneManager);
/// Revision History:
/// Name: Renato Innocenti           Date:05/26/2018        Description: v2.0
/// Name: Renato Innocenti           Date:05/21/2017        Description: v1.0
/// </summary>
///
public class LoadSceneManager : Singleton<LoadSceneManager>
{
    [Header("Loading Visuals")]
    public GameObject panelLoading;
    public Image loadingBar;
    public Text progressText;
    public Text progressDoneText;

    [Header("Timing Settins")]
    public float waitOnLoadEnd;

    [Header("Loading Settings")]
    public LoadSceneMode loadSceneMode = LoadSceneMode.Single;
    public ThreadPriority loadThreadPriority;
    //public AudioListener audioListener;
    public bool completeLoad = true;

    #region Private Variables
    private int sceneToLoad = -1;
    private AsyncOperation operation;

    public Scene currentScene { get; set; }
    public int GetSceneToLoad { get { return sceneToLoad; } }

    #endregion
    private void OnEnable()
    {
        panelLoading.SetActive(false);
    }

    public void LoadScene(int idScene, float timer = 0)
    {
        Application.backgroundLoadingPriority = ThreadPriority.High;
        sceneToLoad = idScene;
        StartCoroutine(LoadAsync(idScene, timer));
    }

    public IEnumerator LoadAsync(int levelNum, float timer = 0)
    {
        if (timer > 0)
        {
            yield return new WaitForSeconds(timer);
        }
        yield return StartCoroutine(FadeScenesManager.Instance.FadeOut());
        completeLoad = false;
        SetLoadingScreen();
        yield return null;
        yield return StartCoroutine(FadeScenesManager.Instance.FadeIn());
        StartOperation(levelNum);
        float lastProgress = 0f;
        while (DoneLoading() == false)
        {
            yield return null;

            if (Mathf.Approximately(operation.progress, lastProgress) == false)
            {
                loadingBar.fillAmount = operation.progress;
                lastProgress = operation.progress;
            }
        }
        //if (loadSceneMode == LoadSceneMode.Additive)
        //    audioListener.enabled = false;

        ShowCompletionVisuals();

        yield return new WaitForSeconds(waitOnLoadEnd);
        yield return StartCoroutine(FadeScenesManager.Instance.FadeOut());
        panelLoading.SetActive(false);
        operation.allowSceneActivation = true;
        yield return StartCoroutine(FadeScenesManager.Instance.FadeIn());
        completeLoad = true;
    }

    private void SetLoadingScreen()
    {
        panelLoading.SetActive(true);
        progressText.enabled = true;
        progressDoneText.enabled = false;
        loadingBar.fillAmount = 0f;
    }

    private void StartOperation(int levelNum)
    {
        Application.backgroundLoadingPriority = loadThreadPriority;
        operation = SceneManager.LoadSceneAsync(levelNum, loadSceneMode);


        if (loadSceneMode == LoadSceneMode.Single)
            operation.allowSceneActivation = false;
    }
    private void ShowCompletionVisuals()
    {
        //Exibe imagens ou animações de completou loading
        progressText.enabled = false;
        progressDoneText.enabled = true;
    }

    private bool DoneLoading()
    {
        return (loadSceneMode == LoadSceneMode.Additive && operation.isDone) || (loadSceneMode == LoadSceneMode.Single && operation.progress >= 0.9f);
    }

    protected override void OnDestroy() { }
}
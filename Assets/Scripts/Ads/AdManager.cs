using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : Singleton<AdManager> {

    [SerializeField]
    protected bool testMode;
    [SerializeField]
    private string GooglePlayGameID;
    [SerializeField]
    private string iTunesStoreGameID;

    public string AdGameID { get; protected set; }

    private void Awake()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                AdGameID = GooglePlayGameID;
                break;
            case RuntimePlatform.IPhonePlayer:
                AdGameID = iTunesStoreGameID;
                break;
            default:
                AdGameID = GooglePlayGameID;
                break;
        }
        if (AdGameID != null && AdGameID != "")
        {
            Advertisement.Initialize(AdGameID, testMode);
        }
    }

    private void Start()
    {
        StartCoroutine(ShowAdWhenReady());
    }

    public void ShowAd(ShowOptions options = null, string zone = "")
    {
        StartCoroutine(WaitForAd());
        if (string.Equals(zone, ""))
            zone = null;
        if (Advertisement.IsReady(zone))
            Advertisement.Show(zone, options);
    }

    IEnumerator ShowAdWhenReady()
    {
        while (!Advertisement.IsReady())
        {
            //Debug.Log("NotReady");
            yield return null;
        }
        ShowAd();
    }

    IEnumerator WaitForAd()
    {
        float currentTimeScale = Time.timeScale;
        Time.timeScale = 0f;
        yield return null;
        //Debug.Log("time: " + Time.timeScale);
        while (Advertisement.isShowing)
            yield return null;

        Time.timeScale = currentTimeScale;
        //Debug.Log("time: " + Time.timeScale);
    }
    protected override void OnDestroy() { }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobRewards : MonoBehaviour
{

    [SerializeField]
    protected string idAndroid;
    [SerializeField]
    protected string idIOS;
    [SerializeField]
    protected readonly string idTestDevice = "BFB7BF88080C042DB07F25E289460AFC";

    protected string idbanner;
    private string idbannerIOS = "ca-app-pub-3940256099942544/1712485313";
    private string idbannerAndroid = "ca-app-pub-3940256099942544/5224354917";

    public enum TypeAd { None, Android, iOS }
    public TypeAd typeAd { get; private set; }

    public bool adTest;
    protected bool rewarded;
    protected RewardBasedVideoAd rewardBasedVideo;
    FadeScenesManager fadeScenes;

    private void Awake()
    {
        Dictionary<string, object> defaults = new Dictionary<string, object>();
        defaults.Add("config_admob_debug", false);
        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
    }

    protected virtual void OnEnable()
    {
        SetupDummy(idbannerIOS, idbannerAndroid);
        fadeScenes = FadeScenesManager.Instance;
#if UNITY_ANDROID
        typeAd = TypeAd.Android;
#elif UNITY_IPHONE
        typeAd = TypeAd.iOS;
#endif
        Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched();
        bool adteste = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue("config_admob_debug").BooleanValue;
        //Debug.Log("Debug?: " + adteste);
        if (adteste)
            idbanner = (typeAd == TypeAd.Android) ? idbannerAndroid : idbannerIOS;
        else
            idbanner = (typeAd == TypeAd.Android) ? idAndroid : idIOS;
    }

    protected void SetupDummy(string ios, string android)
    {
        this.idbannerIOS = ios;
        this.idbannerAndroid = android;
    }


    protected virtual void Start()
    {
        rewardBasedVideo = RewardBasedVideoAd.Instance;
        CallEvents();
        LoadBanner();
    }

    protected void CallEvents()
    {
        // Called when an ad request has successfully loaded.
        rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
        // Called when an ad request failed to load.
        rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
        // Called when an ad is shown.
        rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
        // Called when the ad starts to play.
        rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
        // Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
        // Called when the ad click caused the user to leave the application.
        rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
    }

    public virtual void LoadBanner()
    {
        // Create an empty ad request.
        rewarded = false;
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(idTestDevice)
            .Build();
        rewardBasedVideo.LoadAd(request, idbanner);
    }

    public virtual void ShowBanner()
    {
        fadeScenes.loadAsync.SetActive(true);
        if (rewardBasedVideo.IsLoaded())
        {
            rewardBasedVideo.Show();
        }
#if UNITY_EDITOR
        fadeScenes.loadAsync.SetActive(false);
#endif
    }

    protected virtual void Reward(string rewardtype, double qnt) { }

    #region Handlers
    public virtual void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
    {  
        Debug.Log("HandleRewardBasedVideoLoaded event received");
    }

    public virtual void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        
        fadeScenes.loadAsync.SetActive(false);
        LoadBanner();
        Debug.Log("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
    }

    public virtual void HandleRewardBasedVideoOpened(object sender, EventArgs args)
    {
        fadeScenes.loadAsync.SetActive(false);
        Debug.Log("HandleRewardBasedVideoOpened event received");
    }

    public virtual void HandleRewardBasedVideoStarted(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardBasedVideoStarted event received");
    }

    public virtual void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        LoadBanner();
        Debug.Log("HandleRewardBasedVideoClosed event received");
    }

    public virtual void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        Reward(args.Type, args.Amount);
        Debug.Log("HandleRewardBasedVideoRewarded event received for "+ args.Amount.ToString() + " " + args.Type);
    }

    public virtual void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
    {
        fadeScenes.loadAsync.SetActive(false);
        Debug.Log("HandleRewardBasedVideoLeftApplication event received");
    }
    #endregion
}
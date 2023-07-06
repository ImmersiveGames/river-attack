using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AdMobCreate : MonoBehaviour {

    [SerializeField]
    protected string idAndroid;
    [SerializeField]
    protected string idIOS;
    [SerializeField]
    protected readonly string idTestDevice = "BFB7BF88080C042DB07F25E289460AFC";

    protected string idbanner;
    protected string idbannerIOS;
    protected string idbannerAndroid;

    public enum TypeAd { None, Android, iOS }
    public TypeAd typeAd { get; private set; }

    private void Awake()
    {
        Dictionary<string, object> defaults = new Dictionary<string, object>();
        defaults.Add("config_admob_debug", false);
        Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
    }

    protected virtual void OnEnable()
    {
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

    public abstract void LoadBanner();
    public abstract void ShowBanner();

    protected void SetupDummy(string ios, string android)
    {
        this.idbannerIOS = ios;
        this.idbannerAndroid = android;
    }

    public virtual void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLoaded event received");
    }

    public virtual void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public virtual void HandleOnAdOpened(object sender, EventArgs args)
    {
        Debug.Log("HandleAdOpened event received");
    }

    public virtual void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleAdClosed event received");
    }

    public virtual void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        Debug.Log("HandleAdLeavingApplication event received");
    }
}

using UnityEngine;
using GoogleMobileAds.Api;
using System;
using System.Collections;

public class AdMobInterstitial : AdMobCreate
{
    new readonly string idbannerIOS = "ca-app-pub-3940256099942544/4411468910";
    new readonly string idbannerAndroid = "ca-app-pub-3940256099942544/1033173712";

    private InterstitialAd interstitial;

    protected override void OnEnable()
    {
        SetupDummy(idbannerIOS, idbannerAndroid);
        base.OnEnable();
        LoadBanner();
    }

    public override void LoadBanner()
    {
        interstitial = new InterstitialAd(idbanner);
        CallEvents();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(idTestDevice)
            .Build();

        // Load the banner with the request.
        interstitial.LoadAd(request);
    }

    protected void CallEvents()
    {
        interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }

    public override void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        FadeScenesManager.Instance.loadAsync.SetActive(false);
        InterstitialAdDestroy();
    }

    public override void HandleOnAdClosed(object sender, EventArgs args)
    {
        FadeScenesManager.Instance.loadAsync.SetActive(false);
        InterstitialAdDestroy();
    }

    private void OnDisable()
    {
        if (interstitial != null)
            interstitial.Destroy();
    }

    public void InterstitialAdDestroy()
    {
        if (interstitial != null)
            interstitial.Destroy();
        LoadBanner();
    }

    public override void ShowBanner()
    {
        FadeScenesManager.Instance.loadAsync.SetActive(true);
        if (interstitial.IsLoaded())
            interstitial.Show();
#if UNITY_EDITOR
        FadeScenesManager.Instance.loadAsync.SetActive(false);
#endif
    }
}

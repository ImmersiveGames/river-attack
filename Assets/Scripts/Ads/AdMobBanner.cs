using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobBanner : AdMobCreate
{
    [SerializeField]
    private AdPosition bannerPosition;
    [SerializeField]
    private BannerSize bannerSize;
    new readonly string idbannerIOS = "ca-app-pub-3940256099942544/2934735716";
    new readonly string idbannerAndroid = "ca-app-pub-3940256099942544/6300978111";

    private BannerView bannerView;
    private enum BannerSize { Banner, IABBanner, Leaderboard, MediumRetangle, Smartbanner }

    protected override void OnEnable()
    {
        SetupDummy(idbannerIOS, idbannerAndroid);
        base.OnEnable();
        LoadBanner();
    }

    public override void LoadBanner() {
        AdSize adsize = SetAdSize(bannerSize);
        bannerView = new BannerView(idbanner, adsize, bannerPosition);
        CallEvents();
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder()
            .AddTestDevice(idTestDevice)
            .Build();
        // Load the banner with the request.
        bannerView.LoadAd(request);
    }

    protected void CallEvents()
    {
        // Called when an ad request has successfully loaded.
        bannerView.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        bannerView.OnAdOpening += HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        bannerView.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
    }

    private AdSize SetAdSize(BannerSize bSize)
    {
        switch (bSize)
        {
            case BannerSize.Banner:
                return AdSize.Banner;
            case BannerSize.IABBanner:
                return AdSize.IABBanner;
            case BannerSize.Leaderboard:
                return AdSize.Leaderboard;
            case BannerSize.MediumRetangle:
                return AdSize.MediumRectangle;
            case BannerSize.Smartbanner:
                return AdSize.SmartBanner;
            default:
                return AdSize.Banner;
        }
    }

    public void DestroyMe()
    {
        bannerView.Destroy();
    }
    private void OnDisable()
    {
        if (bannerView != null)
            DestroyMe();
    }

    private void OnDestroy()
    {
        if (bannerView != null)
        {
            DestroyMe();
        }
    }

    public override void ShowBanner()
    {
    }
}

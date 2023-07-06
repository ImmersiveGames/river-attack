using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerAds : Singleton<GameManagerAds>
{

    [SerializeField]
    private string idAndroidApp = "ca-app-pub-4771631071127457~7757053713";
    [SerializeField]
    private string idIphoneApp = "";
    private string idApp;
    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
            idApp = idAndroidApp;
        else if (Application.platform == RuntimePlatform.IPhonePlayer)
            idApp = idIphoneApp;
    }

    private void Start()
    {
        MobileAds.Initialize(idApp);
    }
}

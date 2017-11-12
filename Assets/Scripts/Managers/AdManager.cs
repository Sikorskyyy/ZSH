using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
//using GoogleMobileAds.Api;

public class AdManager: MonoBehaviour
{

    private string gameId = "1599369";

    public string placementId = "rewardedVideo";

    // public const string appID = "ca-app-pub-3481388842630586~8321859376"; 
    //public const string bannerID = "ca-app-pub-3481388842630586/8919150399";

    public static AdManager Instance;

    bool isShowed;

    // public BannerView bannerView;

    public void Awake()
    {    
        Instance = this;

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, false);
        }

        string appKey = "b3155dd297890f755bb8014967e825af81da54504af732d2";
        Appodeal.disableLocationPermissionCheck();
        //Appodeal.setTesting(true);/////////////////////выключить нахуй
        Appodeal.initialize(appKey, Appodeal.BANNER );

    }

    public void ShowAdRewarded(Action callback)
    {
        if (Advertisement.IsReady(placementId))
        {
            ShowOptions options = new ShowOptions();
            //options.resultCallback = HandleShowResult;
            options.resultCallback = (ShowResult result) =>
                {
                    HandleShowResult(result);
                    if (isShowed)
                    {
                        callback();
                        isShowed = false;
                    }
                };
            Advertisement.Show(placementId, options);
        }
    }

    public void showAd()
    {
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
    }

    void HandleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("Video completed - Offer a reward to the player");
            isShowed = true;
        }
    }


    /* public void RequestBanner()
     {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        //AdRequest request = new AdRequest.Builder().Build();
        AdRequest request = new AdRequest.Builder().AddTestDevice(AdRequest.TestDeviceSimulator).AddTestDevice("E4D53D1F739B2783").Build();
        bannerView.LoadAd(request);

        bannerView.Show();

        Debug.Log("build");
     }
        */


    public void ShowBanner()
    {
        if (Appodeal.isLoaded(Appodeal.BANNER))
            Appodeal.show(Appodeal.BANNER_BOTTOM);
        Debug.Log ("dgd");
    }

    public void HideBanner()
    {
        Appodeal.hide(Appodeal.BANNER);
    }

}

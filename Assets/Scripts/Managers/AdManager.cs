using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class AdManager: MonoBehaviour
{
	
    private string gameId = "1599369";

    public string placementId = "rewardedVideo";

    public const string bannerID = "ca-app-pub-3481388842630586~8321859376"; 

    public static AdManager Instance;

    bool isShowed;

    public BannerView bannerView;

    public void Awake()
    {    
        Instance = this;

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, false);
        }

        RequestBanner();
            
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
        

     public void RequestBanner()
     {
        bannerView = new BannerView(bannerID, AdSize.Banner, AdPosition.Bottom);

        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);

        bannerView.Hide();

        Debug.Log("build");
     }
        
}

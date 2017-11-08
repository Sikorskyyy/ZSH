using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;


public class AdManager: MonoBehaviour
{
	
    private string gameId = "1599369";

    public string placementId = "rewardedVideo";

    public static AdManager Instance;

    bool isShowed;

    public void Awake()
    {    
        Instance = this;

        if (Advertisement.isSupported && !Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, false);
        }
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
        
}

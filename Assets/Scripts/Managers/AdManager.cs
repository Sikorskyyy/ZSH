/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using Assets.Scripts.Common;

public class AdManager {
	
	private string gameId = "1399670";

	public string placementId = "rewardedVideo";

	public static AdManager Instance; 

		public AdManager ()
		{    
			Instance = this;

			if (Advertisement.isSupported &&!Advertisement.isInitialized ) {
				Advertisement.Initialize (gameId, false);
			}
			bonusText = BonusText.instance;
			
		}

		public void ShowAd ()
		{
			if (Advertisement.IsReady (placementId)) {
				ShowOptions options = new ShowOptions ();
				options.resultCallback = HandleShowResult;
				Advertisement.Show (placementId, options);
			}
		}
		

		void HandleShowResult (ShowResult result)
		{
			if(result == ShowResult.Finished) 
            {
                Debug.Log("Video completed - Offer a reward to the player");
			}
            else if(result == ShowResult.Skipped) {
				Debug.LogWarning("Video was skipped - Do NOT reward the player");

			}else if(result == ShowResult.Failed) {
				Debug.LogError("Video failed to show");
			}
		}
        
}*/

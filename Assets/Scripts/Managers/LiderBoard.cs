using UnityEngine;
using System.Collections;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using GooglePlayGames.BasicApi;

public class LiderBoard : MonoBehaviour
{
	#region DEFAULT_UNITY_CALLBACKS

	void Start ()
	{
		if (Application.internetReachability != NetworkReachability.NotReachable) {
			GooglePlayGames.PlayGamesPlatform.DebugLogEnabled = false;
			PlayGamesPlatform.Activate ();
			LogIn ();
		}

	}
	#endregion
	public static void LogIn ()
	{
		if(!Social.localUser.authenticated)
			Social.localUser.Authenticate ((bool success) =>{/*AchievmentManager.dayCheck ();*/});
	}

	public static void OnShowLeaderBoard ()
	{
		if (Social.localUser.authenticated)

            ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI (GPGSI.leaderboard); // Show current (Active) leaderboard
	}

	public static bool OnAddScoreToLeaderBorad (long score)
	{
		bool suc = true;
		if (Social.localUser.authenticated) {
            Social.ReportScore (score, GPGSI.leaderboard, (bool success) =>{});
		}
		return suc;
	}

	#region Achievements
	/*public static void UnlockAchievement(string id)
	{
		Social.ReportProgress(id, 100, success => { });
	}

	public static void IncrementAchievement(string id, int stepsToIncrement)
	{
		PlayGamesPlatform.Instance.IncrementAchievement(id, stepsToIncrement, success => { });
	}

	public static void ShowAchievementsUI()
	{
		Social.ShowAchievementsUI();
	}*/
	#endregion 
		
}
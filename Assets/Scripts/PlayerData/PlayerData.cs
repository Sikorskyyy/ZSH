using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayerData : MonoBehaviour
{

    public static PlayerData Instance { get; private set;}

    static string SCORE_KEY = "score";
    static string TIPS_KEY = "tipsAmount";
    static string TIMER_BONUS_KEY = "timerBonus"; 
 
    public int Score { get; private set;}
    public int TipsCount { get; private set;}
    public int TimerBonusCount  { get; private set;}

    void Start()
    {
        Instance = this;
        
        if (PlayerPrefs.HasKey(SCORE_KEY))
        {
            Score = GetScore();
        }
        else
        {
            Score = 0;
        }

        if (PlayerPrefs.HasKey(TIMER_BONUS_KEY))
        {
            TimerBonusCount = GetTimerBonusCount();
        }
        else
        {
            TimerBonusCount = 0;
        }

        if (PlayerPrefs.HasKey(TIPS_KEY))
        {
            TipsCount = GetTipsCount();
        }
        else
        {
            TipsCount = 0;
        }

        SetTipsCount(5);
        Debug.Log(GetTipsCount());
    }

    public void SetScore (int score)
	{
        Score = score;
        PlayerPrefs.SetString(SCORE_KEY, Encryptor.Encode(score.ToString()));
	}
		
	public int GetScore ()
	{
        return int.Parse(Encryptor.Decode(PlayerPrefs.GetString (SCORE_KEY)));
	}

		
	public void SetTipsCount (int tips)
	{
        TipsCount = tips; 
        PlayerPrefs.SetString(TIPS_KEY, Encryptor.Encode(tips.ToString()));
	}

    public int GetTipsCount ()
    {
        return  int.Parse(Encryptor.Decode(PlayerPrefs.GetString (TIPS_KEY)));
    }


    public void SetTimerBonusCount (int count)
    {
        TimerBonusCount = count;
        PlayerPrefs.SetString(TIMER_BONUS_KEY, Encryptor.Encode(count.ToString()));
    }

	public int GetTimerBonusCount ()
	{
        return  int.Parse(Encryptor.Decode(PlayerPrefs.GetString (TIMER_BONUS_KEY)));
	}
        
}

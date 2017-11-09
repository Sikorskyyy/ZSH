using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BonusUpdateInfo : MonoBehaviour 
{

    public static BonusUpdateInfo Instance;
    public bool isCanUpdate = false;

    [SerializeField] Text timerCount;
    [SerializeField] Text tipsCount;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("firsssst"))
        {
            PlayerPrefs.SetInt("firsssst", 1);

            PlayerData.Instance.SetTimerBonusCount(5);
            PlayerData.Instance.SetTipsCount(5);
        }
        
    }
	
	public void UpdateInfo () 
    {
        timerCount.text = "x " + PlayerData.Instance.TimerBonusCount.ToString();
        tipsCount.text = "x " + PlayerData.Instance.TipsCount.ToString();
	}
}

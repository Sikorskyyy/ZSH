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
    }// Use this for initialization

    void OnEnable()
    {
        if (isCanUpdate)
        {
            UpdateInfo();
        }
    }
	
	public void UpdateInfo () 
    {
        timerCount.text = "x " + PlayerData.Instance.TimerBonusCount.ToString();
        tipsCount.text = "x " + PlayerData.Instance.TipsCount.ToString();
	}
}

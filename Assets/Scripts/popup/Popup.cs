using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] GameObject popup ;

    public string action;

    private void OnMouseUpAsButton()
    {
        var key = "popup";

        switch (action)
        {

            case "late":
                PlayerPrefs.SetInt(key, 0);
                break;

            case "no":
                PlayerPrefs.SetInt(key, -50);
                break;

            case "rate":
                Application.OpenURL("market://details?id=com.QiwiGames.theShaverMystery");
                PlayerData.Instance.SetTimerBonusCount(10);
                PlayerData.Instance.SetTipsCount(10);
            break;


        }   

        popup.SetActive(false);
                
    }
}
   
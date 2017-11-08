using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatchButtons : MonoBehaviour {

    [SerializeField] string action; 
	
    private void OnMouseDown()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.05f, transform.position.z);

    }

    private void OnMouseUp()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 0.05f, transform.position.z);
    }

    private void OnMouseUpAsButton()
    {
        AudioManager.Instance.PlaySound();

        switch (action)
        {

            case "tips":
                AdManager.Instance.ShowAdRewarded(addTips);
            break;

            case "time":
                AdManager.Instance.ShowAdRewarded(addTime);
                  
            break;
            
        }
    }

    void addTips()
    {
        PlayerData.Instance.SetTipsCount(1);
        BonusUpdateInfo.Instance.UpdateInfo();
    }

    void addTime()
    {
        PlayerData.Instance.SetTimerBonusCount(1);
        BonusUpdateInfo.Instance.UpdateInfo();
    }



}

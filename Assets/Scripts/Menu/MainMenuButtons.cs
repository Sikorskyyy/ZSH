using System.Collections;
using UnityEngine;
//using UnityEngine.SceneManagement;

//using UnityEngine.Advertisements;

public class MainMenuButtons : MonoBehaviour
{

    public string action;

    private void Start()
    {		

        if (action == "noAds")
        {
            if (PlayerData.Instance.IsAd)
            {	
                gameObject.SetActive(false);
            }
        }
			
		
    }

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
           
			
            case "records":
                
                LiderBoard.OnShowLeaderBoard();
                break;
               

            case "noAds":
					
                APIController.instance.BuyNoAds();

                break;

            case "1001":
                Application.OpenURL("https://vk.com/mem1001");
                break;

            case "qiwi":
                Application.OpenURL("https://vk.com/qiwigames");
                break;
				
            case "rate":
                Application.OpenURL("market://details?id=com.QiwiGames.theShaverMystery");
                break;

				
            case "exit":
                Application.Quit();
                break;

        }   
                

    }

}

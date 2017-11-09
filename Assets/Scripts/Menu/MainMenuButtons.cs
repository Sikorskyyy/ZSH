using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;

public class MainMenuButtons : MonoBehaviour {

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
                Debug.Log("reco");//нихуя не заработает пока я не залью апк норм ключ 
			break;
               

				case "noAds":
					/*if (APIController.instance == null) {
						APIController.instance = new APIController ();
						APIController.instance.Start();
					}*/
                
					APIController.instance.BuyNoAds ();

				break;


				/*case "music":
					if (PlayerPrefs.GetString ("music") != "off") {
						PlayerPrefs.SetString ("music", "off");
						m_on.SetActive (false);
						m_off.SetActive (true);
					} else {
						PlayerPrefs.SetString ("music", "on");
						m_on.SetActive (true);
						m_off.SetActive (false);
					}
				break;*/

				case "rate":
                Application.OpenURL ("market://details?id=com.QiwiGames.theShaverMystery");
				break;

				
				case "exit":
					Application.Quit ();
				break;

        }   
                

    }

}

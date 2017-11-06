using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.Advertisements;

public class MainMenuButtons : MonoBehaviour {

    public string action;
    //public GameObject m_on, m_off;
	//private LiderBoard leadBoard;
	//private AdManager adManager;
	public static GameObject noAdBtn;
    private void Start()
    {		

		//new secure player data init!!!
	//	PlayerData.NewPlayerData();
		//leadBoard = new LiderBoard ();
      /*  if(PlayerPrefs.GetString("music") != "off")
        {
            m_on.SetActive(true);
            m_off.SetActive(false);
        }
        else
        {
            m_on.SetActive(false);
            m_off.SetActive(true);
        }

		if (action == "watch")
			adManager = new AdManager ();
		
		if (action == "noAds") {
			noAdBtn = gameObject;
			if (PlayerPrefs.HasKey ("NoAdsIsBuyed")) {///бля выключить рекламу не забудь поц))))
				gameObject.SetActive (false);
			}
		}*/
			
		
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
        switch (action)
        {
			case "start":
				SceneManager.LoadScene ("main");
            break;

			case "records":
			//LiderBoard.OnShowLeaderBoard ();
				Debug.Log ("rec");
			break;

			case "pers":
				SceneManager.LoadScene ("PersSelection");
			break;

				case "pratein":
					Application.OpenURL ("https://vk.com/no_pratein");
				break;

				case "watchTip":
					//adManager.ShowAd ();
				break;

                case "watchTime":
                //adManager.ShowAd ();
                break;

				case "noAds":
					/*if (APIController.instance == null) {
						APIController.instance = new APIController ();
						APIController.instance.Start();
					}
					APIController.instance.BuyNoAds ();*/
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
					Application.OpenURL ("market://details?id=com.QiwiGames.Stupid_Kochkas");
				break;

				
				case "exit":
					Application.Quit ();
				break;

        }    

    }

}

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField]
    private Text scoreText;

    [SerializeField]
    private Text bestText;

    [SerializeField]
    private GameObject newRecord;

    [SerializeField]
    private GameObject bestLabel;

    [SerializeField]
    private UnityEvent hideWinMessage;

    [SerializeField] AudioClip clipHighScore;

    [SerializeField] GameObject Accost;

    private bool isHighScore;
    private int score;
    private string boardKey;

    #region GameOver

    public void Show(string board, int isNewRecord, int score)
    {
        boardKey = board;

        bestText.text = PlayerData.Instance.Score.ToString();
        scoreText.text = score.ToString();

        isHighScore = isNewRecord > 0;

        Debug.Log("isHigh Score " + isHighScore.ToString());

        Accost.SetActive(true);

        if (isHighScore)
        {
            var AccostText = Accost.GetComponent<Text>();

            AccostText.text = "ВООООУ КРУТА EEE\n+3 подсказки и 2 таймера";
            PlayerData.Instance.SetTipsCount(3);
            PlayerData.Instance.SetTimerBonusCount(2);

            AudioManager.Instance.PlaySoundOneShot(clipHighScore);

        }
        else if (score >= 200 && score < 350)
        {
            var AccostText = Accost.GetComponent<Text>();

            AccostText.text = "НАРМАЛЬНА КАНЕШНА\n+ подсказка";
            PlayerData.Instance.SetTipsCount(1);

        }
        else if (score >= 350 && score < 600)
        {
            var AccostText = Accost.GetComponent<Text>();

            AccostText.text = "ТЫ ЖЕСТКИ!!!!\n+ таймер";
            PlayerData.Instance.SetTimerBonusCount(1);

        }
        else if (score >= 600 && score < 1000)
        {
            var AccostText = Accost.GetComponent<Text>();

            AccostText.text = "БАУ НУ ТАЩИШЬ РИЛТОЛК\n+2 подсказки и таймер";
            PlayerData.Instance.SetTimerBonusCount(1);
            PlayerData.Instance.SetTipsCount(2);

        }
        else if (score >= 1000)
        {
            var AccostText = Accost.GetComponent<Text>();

            AccostText.text = "ЛОооооЛ АНРИАЛ\n+2 подсказки и 2 таймер";
            PlayerData.Instance.SetTimerBonusCount(2);
            PlayerData.Instance.SetTipsCount(2);

        }
        else
        {
            //Accost.SetActive(false);
            var AccostText = Accost.GetComponent<Text>();
            AccostText.text = "))))))0)))0))0)\n попробуй еще раз";
        }


        newRecord.SetActive(isHighScore);
        bestLabel.SetActive(!isHighScore);

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        hideWinMessage.Invoke();
    }

    #endregion
 
}

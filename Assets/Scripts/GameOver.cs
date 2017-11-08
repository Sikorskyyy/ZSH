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

    private bool isHighScore;
    private int score;
    private string boardKey;

    Vector3 PosScore;
    Vector3 NeRecordPos = new Vector3(229, -299, 0);
    #region GameOver

    bool isfirst = true;

    public void Show(string board, int isNewRecord, int score)
    {
        if (isfirst)
        {
            isfirst = false;
            PosScore = scoreText.transform.position;
        }

        boardKey = board;

        bestText.text = PlayerData.Instance.Score.ToString();
        scoreText.text = score.ToString();

        isHighScore = isNewRecord > 0;

        Debug.Log("isHigh Score " + isHighScore.ToString());

        newRecord.SetActive(isHighScore);
        bestLabel.SetActive(!isHighScore);

        if (isHighScore)
        {
            scoreText.transform.Translate(0, -0.5f, 0);
        }
        else
        {
            //scoreText.transform.Translate(0, -0.5f, 0);
           scoreText.transform.position = PosScore; 
        }

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        hideWinMessage.Invoke();
    }

    #endregion
 
}

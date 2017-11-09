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

    #region GameOver

    public void Show(string board, int isNewRecord, int score)
    {
        boardKey = board;

        bestText.text = PlayerData.Instance.Score.ToString();
        scoreText.text = score.ToString();

        isHighScore = isNewRecord > 0;

        Debug.Log("isHigh Score " + isHighScore.ToString());

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

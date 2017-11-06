﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{

    [SerializeField]
    private Text movesValue;

    [SerializeField]
    private GameObject[] highScore;

    [SerializeField]
    private CanvasGroup highScoreOkButton;

    [SerializeField]
    private UnityEvent hideWinMessage;

    private bool isHighScore;
    private int moves;
    private string boardKey;

    #region GameOver

    public void Show(string board, int movesCount)
    {
        moves = movesCount;
        boardKey = board;

        movesValue.text = moves.ToString();

        isHighScore = LeaderboardDataManager.IsHighScore(boardKey, moves);
       /* for (var i = 0; i < highScore.Length; i++)
        {
            highScore[i].SetActive(isHighScore);
        }*/

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        if (isHighScore)
        {

            //LeaderboardDataManager.SaveScore(boardKey, moves, nameInput.text);
        }
        hideWinMessage.Invoke();
    }

    #endregion



    public void NameChanged(string value)
    {
        highScoreOkButton.alpha = string.Empty == value ? 0.5f : 1f;
    }
		
}
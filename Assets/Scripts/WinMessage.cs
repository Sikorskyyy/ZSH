using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WinMessage : MonoBehaviour
{
    [SerializeField]
    private Text scoreValue;
   
    [SerializeField]
    private UnityEvent hideWinMessage;

    private int moves;
    private string boardKey;

	#region GameOver

    public void Show(string board, int score)
    {
        moves = score;
        boardKey = board;

        scoreValue.text = moves.ToString();

        gameObject.SetActive(true);
    }

    public void Hide()
    {
        hideWinMessage.Invoke();
    }

	#endregion

}

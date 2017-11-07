using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameBoard : MonoBehaviour
{
    [SerializeField]
    private string boardKey;

    [SerializeField]
    private Card[] cards;

    [SerializeField]
    private RandomSpritePicker colorPicker;

    [SerializeField]
    private RandomSpritePicker accessoryPicker;

    [SerializeField]
    private Text timerCount;

    [SerializeField] Slider timerBar;

    [SerializeField]
    private Text newMovesCount;

    [SerializeField]
    private Animator movesCountAnimator;

    [SerializeField]
    private EventSystem eventSystem;

    [SerializeField] Animator GameCanvasAnimator;

    [SerializeField]
    private CustomEvents.UnityStringIntEvent nextLevel;

    [SerializeField]
    private CustomEvents.UnityStringIntEvent gameOver;

    string GAME_OVER_ANIMATOR_CONDITION = "isGameOver";

    bool isUpadate;

    private Cat[] cats;
    private Card selectedCard;
    private int pairsFound;
    private int time;
    float deltaTime;

    int MAX_TIME = 10;

    float TIMEBAR_SCALE;

    public int level{ get; private set;}

    public bool isTip = false;

    int TimerBonusCount = 3;
    int TipsCount = 3;

    #region unity Lifecycle

    void Start()
    {
        level = 0;
    }

    private void OnEnable()
    {
        GenerateCats();
        Shuffle(cats);
        for (var i = 0; i < cards.Length; i++)
        {
            cards[i].Cat = cats[i];
        }

        time = MAX_TIME - level * 10;

        Reset();
    }


    void Update()
    {
        if (isUpadate)
        {
            
            if (deltaTime >= 1)
            {
                deltaTime = 0;
                UpdateTimer();

                if (time == 0)
                {
                    //ToGameOver();
                    nextLevel.Invoke(boardKey, time);
                    GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, false);//nextLevel
                    isUpadate = false;
                    level++;
                }
                time--;
            }
	
            timerBar.value -= Time.deltaTime * TIMEBAR_SCALE;
            deltaTime += Time.deltaTime;
        }
    }

    #endregion


    private void GenerateCats()
    {
        cats = new Cat[cards.Length];

        for (var i = 0; i < cats.Length; i += 2)
        {
            Cat generatedCat = null;
            Cat subCat = null;
            while (null == generatedCat)
            {
                var color = colorPicker.Pick();

                if (!CatExists(color))
                {
                    generatedCat = new Cat { Color = color, /*Accessory = accessory,*/ id = i };

                    color = colorPicker.GetSubPicture();
                    subCat = new Cat { Color = color, /*Accessory = accessory,*/ id = i };
                }
                    
            }

            cats[i] = generatedCat;
            cats[i + 1] = subCat;
        }
    }

    private bool CatExists(Sprite color)
    {
        return System.Array.FindIndex(cats, cat => null != cat && cat.Color == color) >= 0;
    }


    Card OpenPair (Card compareCard)
    {
        var id = compareCard.Cat.id;

        var indexPairCard = System.Array.FindIndex(cards, card => card.Cat.id == id 
            && card != compareCard); 
        var pairCard = cards[indexPairCard];

        pairCard.FlipVisible();

        return pairCard;

    }


    private void Shuffle(Cat[] items)
    {
        for (var i = 0; i < items.Length; i++)
        {
            var tmp = items[i];
            var r = Random.Range(i, items.Length);
            items[i] = items[r];
            items[r] = tmp;
        }
    }

    public void SelectCard(Card card)
    {
        if (null == selectedCard)
        {
            selectedCard = card;
            if (isTip)
            {
               card  = OpenPair(selectedCard);
                isTip = false;
            }
            else
            {
                return;
            }
        }

        eventSystem.enabled = false;
        StartCoroutine(ActionAfterDelay.DoAfterDelay(0.5f, () =>
                {
                    CompareCards(card);
                }));
    }

    public void UseTip()
    {
        if (TipsCount > 0)
        {
            isTip = true;
            TipsCount--;
        }
    }

    public void UseTimerBonus()
    {
        if (TimerBonusCount > 0)
        {
            time += 20;
            timerBar.value += 20 * TIMEBAR_SCALE;
            TimerBonusCount--;
        }
    }

    private void CompareCards(Card card)
    {
        // moves++;
        //UpdateTimer();

        if (card.Cat.id != selectedCard.Cat.id)
        {
            card.FlipHidden();
            selectedCard.FlipHidden();
        }
        else
        {
            pairsFound++;
            card.PairFound();
            selectedCard.PairFound();
            CheckGameEnd();
        }
        selectedCard = null;
        eventSystem.enabled = true;
    }


    void ToGameOver()
    {
        gameOver.Invoke(boardKey, time);
        GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, true);
        level =  0;
    }


    private void CheckGameEnd()
    {
        if (pairsFound >= cards.Length / 2)
        {
            nextLevel.Invoke(boardKey, time);
            GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, false);
            level++;
            isUpadate = false;//nextLevel
        }
    }

    void Reset()
    {
        selectedCard = null;

        GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, true);

        pairsFound = 0;

        TIMEBAR_SCALE = 100 / MAX_TIME; 
        timerBar.value = timerBar.maxValue;
        UpdateTimer(); 
       // Debug.Break();
        isUpadate = true;

        TimerBonusCount = 3;
        TipsCount = 3;
    }

    public void Quit()
    {
        Reset();
        level = 0;
    } 

    private void UpdateTimer()
    {
        if (time == MAX_TIME)
        {
            timerCount.text = time.ToString();
            newMovesCount.text = time.ToString();
            return;
        }

        newMovesCount.text = time.ToString();
        timerCount.text = (time).ToString();
        movesCountAnimator.SetTrigger("UpdateMovesCount");
    }
}

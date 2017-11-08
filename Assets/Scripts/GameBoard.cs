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
    private float time;
    float deltaTime;

    float MAX_TIME = 100;

    float TIMEBAR_SCALE;

    bool isAd = true;
    int showAdCounter;

    bool isFirst = true ;

    public int level{ get; private set;}

    bool isTip = false;
    int TimerBonusCount ;
    int TipsCount ;
    int  AD_COUNTER_CONST = 20;
    int StepsCount ;

    [SerializeField] Text TipsCountTxt;
    [SerializeField] Text TimerBonusTxt;
    [SerializeField] Text LevelText;
    [SerializeField] Text StepsCountTxt;
    [SerializeField] Text LevelJOkeTxt;

    private string score = "" ;
    public float Score
    {
        get
        {
            return float.Parse(Encryptor.Decode(score));
        }

        set
        {
            score = Encryptor.Encode (value.ToString ());
        }

    }

    #region unity Lifecycle

    void Start()
    {
        
        level = 0;
        Score = 0;
        showAdCounter = AD_COUNTER_CONST;
       // isFirst = true;
    }

    private void OnEnable()
    {
        GenerateCats();
        Shuffle(cats);

        isAd =  PlayerData.Instance.IsAd;

        for (var i = 0; i < cards.Length; i++)
        {
            cards[i].Cat = cats[i];
        }

        if (showAdCounter != 0)
        {
            showAdCounter--;
        }

        if (isFirst)
        {
            if (PlayerData.Instance.TipsCount >= 3)
            {
                TipsCount = 3;
                PlayerData.Instance.SetTipsCount(-3);
            }
            else
            {
                TipsCount = PlayerData.Instance.TipsCount;
                PlayerData.Instance.SetTipsCount(-TipsCount);
            }
                

            if (PlayerData.Instance.TimerBonusCount >= 3)
            {
                TimerBonusCount = 3;
                PlayerData.Instance.SetTimerBonusCount(-3);
            }
            else
            {
                TimerBonusCount = PlayerData.Instance.TimerBonusCount;
                PlayerData.Instance.SetTimerBonusCount(-TimerBonusCount);
            }

            isFirst = false;

            Debug.Log(TipsCount.ToString() + TimerBonusCount.ToString());
        }



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
                    ToGameOver();

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

        if (!isTip)
        {
            StepsCount--;
            StepsCountTxt.text = StepsCount.ToString();

            if (StepsCount == 0)
            {
                ToGameOver();
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
        if(!isTip)
        {
            if (TipsCount > 0)
            {
                isTip = true;
                TipsCount--;
            }
                TipsCountTxt.text = TipsCount.ToString();
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
        TimerBonusTxt.text = TimerBonusCount.ToString();
    }

    private void CompareCards(Card card)
    {


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
        Score += (float)(pairsFound *3* (level+1));

        Debug.Log(Score.ToString()+"Score");

        //if 0-loose else win;
        var isNewRecord = 0;
        if ((int)Score > PlayerData.Instance.Score)
        {
            //new record;
            //liderboard
            PlayerData.Instance.SetScore((int)Score);
            LiderBoard.OnAddScoreToLeaderBorad((long)Score);
            isNewRecord = 1;
        }

        if (isAd)
        {
            if (showAdCounter == 0)
            {
                AdManager.Instance.showAd();
                showAdCounter = AD_COUNTER_CONST;
            }
           
        }
    
        gameOver.Invoke(boardKey, isNewRecord);

        GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, true);
        level = 0;
        isUpadate = false;
        isFirst = true;
    }


    private void CheckGameEnd()
    {
        if (pairsFound >= cards.Length / 2)
        {
            Score += (float)((time/10 + (float)(StepsCount/5)) *(level+1));

            Debug.Log(Score.ToString()+"score");

            nextLevel.Invoke(boardKey, (int)Score);
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

        if (level == 0)
        {
            StepsCount = 50;
            MAX_TIME = time = 100;
            LevelJOkeTxt.text = "ИЗИ КАТОЧКА ЛЕВЛ " + level.ToString() ;
        }
        else if (level == 1)
        {
            StepsCount = 40;
            MAX_TIME = time = 85;
            LevelJOkeTxt.text = "НОРМ ЛЕВЛ " + level.ToString();
        }
        else if (level == 2)
        {
            StepsCount = 30;
            MAX_TIME = time = 70;
            LevelJOkeTxt.text = "ТАЩИШЬ ЛЕВЛ " + level.ToString();
        }
        else if (level == 3)
        {
            StepsCount = 25;
            MAX_TIME = time = 50;
            LevelJOkeTxt.text = "ОООГО ЛЕВЛ " + level.ToString();
        }
        else if (level == 4)
        {
            StepsCount = 25;
            MAX_TIME = time = 40;
            LevelJOkeTxt.text = "ЭТА ЖЕСТКА ЛЕВЛ " + level.ToString();
        }
        else if (level == 4)
        {
            StepsCount = 20;
            MAX_TIME = time = 35;
            LevelJOkeTxt.text = "ХАРД ЛЕВЛ " + level.ToString();
        }
        else if (level == 5)
        {
            StepsCount = 18;
            MAX_TIME = time = 30;
            LevelJOkeTxt.text = "ЕЕЕЕЕЕ БОЙ ЛЕВЛ " + level.ToString();
        }
        else
        {
            StepsCount = 16;
            MAX_TIME = time = 28;
            LevelJOkeTxt.text = "ЗАДРОТ ЛЕВЛ "+ level.ToString();
        }



        TIMEBAR_SCALE = 100 / time; 
        timerBar.value = timerBar.maxValue;
        UpdateTimer(); 
       // Debug.Break();
        isUpadate = true;


        TimerBonusTxt.text = TimerBonusCount.ToString();
        TipsCountTxt.text = TipsCount.ToString();
        LevelText.text = (level + 1).ToString();

        StepsCountTxt.text = StepsCount.ToString();

     }

    public void Quit()
    {
        Score = 0;
        level = 0;
        Reset();
        isFirst = true;
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

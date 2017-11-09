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
    private CustomEvents.UnityStringIntIntEvent gameOver;

    string GAME_OVER_ANIMATOR_CONDITION = "isGameOver";

    bool isUpadate;

    private Cat[] cats;
    private Card selectedCard;
    private int pairsFound;
    private float time;
    float deltaTime;

    float MAX_TIME = 100;
    int MAX_STEP_COUNT;

    float TIMEBAR_SCALE;

    bool isAd = true;
    int showAdCounter;

    bool isFirst = true;

    public int level{ get; private set; }

    bool isTip = false;
    int TimerBonusCount;
    int TipsCount;
    int AD_COUNTER_CONST = 10;
    int StepsCount;

    public bool isTest = false;

    [SerializeField] Text TipsCountTxt;
    [SerializeField] Text TimerBonusTxt;
    [SerializeField] Text LevelText;
    [SerializeField] Text StepsCountTxt;
    [SerializeField] Text LevelJOkeTxt;

    private string score = "";

    public float Score
    {
        get
        {
            return float.Parse(Encryptor.Decode(score));
        }

        set
        {
            score = Encryptor.Encode(value.ToString());
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

        isAd = PlayerData.Instance.IsAd;

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
                    var audioClip = colorPicker.GetClip();

                    generatedCat = new Cat { Color = color, clip = audioClip, id = i };

                    color = colorPicker.GetSubPicture();
                    subCat = new Cat { Color = color, clip = audioClip, id = i };
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


    Card OpenPair(Card compareCard)
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
                card = OpenPair(selectedCard);
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

        }

        eventSystem.enabled = false;
        StartCoroutine(ActionAfterDelay.DoAfterDelay(0.5f, () =>
                {
                    CompareCards(card);
                }));
        
    }

    public void UseTip()
    {
        if (!isTip)
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
            AudioManager.Instance.PlaySoundOneShot(selectedCard.Cat.clip);
        }

        CheckGameEnd();

        selectedCard = null;
        eventSystem.enabled = true;
    }


    void ToGameOver()
    {
        Score += (float)(pairsFound * (level + 1));

        Debug.Log(Score.ToString() + "Score");

        var isNewRecord = 0;
        if ((int)Score > PlayerData.Instance.Score)
        {
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
    
        gameOver.Invoke(boardKey, isNewRecord, (int)Score);
        GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, true);

        level = 0;
        isUpadate = false;
        isFirst = true;
        Score = 0;

        PlayerData.Instance.SetTipsCount(TipsCount);
        PlayerData.Instance.SetTimerBonusCount(TimerBonusCount);
    }


    private void CheckGameEnd()
    {
        if (pairsFound >= cards.Length / 2)
        {
            var deltaTime = (MAX_TIME - time);
            var deltaSteps = MAX_STEP_COUNT - StepsCount;
           

            if (deltaSteps == 0)
                deltaSteps = 1;
            if (deltaTime == 0)
                deltaTime = 1;

            var deltaScore = (float)((100 - (deltaTime / 2 + deltaSteps)) * (level + 1));

            Score += deltaScore;


            Debug.Log("Steps" + deltaSteps.ToString() + "\nTime"
                + deltaTime.ToString() + "delscore" + deltaScore.ToString());
            

            Debug.Log(Score.ToString() + "score");

            nextLevel.Invoke(boardKey, (int)Score);
            GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, false);
            level++;
            isUpadate = false;//nextLevel
        }
        else if (StepsCount == 0)
        {
            ToGameOver();
        }
    }

    void Reset()
    {
        selectedCard = null;

        GameCanvasAnimator.SetBool(GAME_OVER_ANIMATOR_CONDITION, true);

        pairsFound = 0;

        if (level == 0)
        {
            MAX_STEP_COUNT = StepsCount = 50;
            MAX_TIME = time = 100;
            LevelJOkeTxt.text = "'ИЗИ КАТОЧКА ЛЕВЛ' " + (level + 1).ToString();
        }
        else if (level == 1)
        {
            MAX_STEP_COUNT = StepsCount = 40;
            MAX_TIME = time = 85;
            LevelJOkeTxt.text = "'НОРМ ЛЕВЛ' " + (level + 1).ToString();
        }
        else if (level == 2)
        {
            MAX_STEP_COUNT = StepsCount = 30;
            MAX_TIME = time = 70;
            LevelJOkeTxt.text = "'ТАЩИШЬ ЛЕВЛ' " + (level + 1).ToString();
        }
        else if (level == 3)
        {
            MAX_STEP_COUNT = StepsCount = 25;
            MAX_TIME = time = 60;
            LevelJOkeTxt.text = "'ОООГО ЛЕВЛ' " + (level + 1).ToString();
        }
        else if (level == 4)
        {
            MAX_STEP_COUNT = StepsCount = 25;
            MAX_TIME = time = 50;
            LevelJOkeTxt.text = "ЭТА ЖЕСТКА ЛЕВЛ " + (level + 1).ToString();
        }
        else if (level == 4)
        {
            MAX_STEP_COUNT = StepsCount = 20;
            MAX_TIME = time = 40;
            LevelJOkeTxt.text = "ХАРД ЛЕВЛ " + (level + 1).ToString();
        }
        else if (level == 5)
        {
            MAX_STEP_COUNT = StepsCount = 18;
            MAX_TIME = time = 35;
            LevelJOkeTxt.text = "ЕЕЕЕЕЕ БОЙ ЛЕВЛ " + (level + 1).ToString();
        }
        else
        {
            MAX_STEP_COUNT = StepsCount = 16;
            MAX_TIME = time = 32;
            LevelJOkeTxt.text = "ЗАДРОТ ЛЕВЛ " + (level + 1).ToString();
        }



        TIMEBAR_SCALE = 100 / time; 
        timerBar.value = timerBar.maxValue;
        UpdateTimer(); 
      
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
        PlayerData.Instance.SetTipsCount(TipsCount);
        PlayerData.Instance.SetTimerBonusCount(TimerBonusCount);
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

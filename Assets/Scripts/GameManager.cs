using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    public Card firstCard;
    public Card secondCard;

    public Text timeTxt;
    public Text endTxt;

    public int cardCnt = 0;
    float timer = 30.0f;
    private void Awake()
    {
        if (instance == null )
        {
            instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timeTxt.text = timer.ToString("N2");
        if(timer <= 0f)
        {
            Time.timeScale = 0f;
            endTxt.gameObject.SetActive(true);
        }
    }

    public void Matched() { 
        if(firstCard.idx == secondCard.idx)
        {
            firstCard.DestroyCard();
            secondCard.DestroyCard();
            cardCnt -= 2;
            if(cardCnt == 0)
            {
                Time.timeScale = 0f;
                endTxt.gameObject.SetActive(true);
            }

        } else
        {
            firstCard.CloseCard();
            secondCard.CloseCard();
        }

        firstCard = null;
        secondCard = null;

    }
}

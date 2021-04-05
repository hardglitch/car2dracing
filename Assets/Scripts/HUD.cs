using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text coinsUI, timeUI, finishCoinsUI, finishTimeUI;
    [SerializeField] private Image[] hpUI;
    [SerializeField] private Sprite isLife, noLife;
    [SerializeField] private GameObject finishScreen, restartScreen, pauseScreen;
    [SerializeField] private GameObject TurnOverCounterImage;

    [SerializeField] private GameObject ratingStar;
    [SerializeField] private Transform parentStar;
    [SerializeField] private float starDistance = 1f;

    [SerializeField] private TimeWork timeWork;
    [SerializeField] private float countdown = 60f;
    private float levelTime = 0f;
    [SerializeField] private readonly float levelTimeAAA = 120f;

    [SerializeField] private CoinBot coinBot;
    [SerializeField] private SFXManager sfxManager;



    private void Start()
    {
        if ((int)timeWork == 2)
            levelTime = countdown;

        Time.timeScale = 1f;
        player.enabled = true;
        TurnOverCounter(false);

        ShowCoinsUI();
        MakeHP();
    }


    private void FixedUpdate()
    {
        TimeManagment();
    }


    public void ShowCoinsUI()
    {
        coinsUI.text = player.GetCoins().ToString("00");
    }

    private void TimeManagment()
    {
        if ((int)timeWork == 1)
        {
            levelTime += Time.fixedDeltaTime;
            timeUI.text = ShowGameTime(levelTime);
        }
        else
        if ((int)timeWork == 2)
        {
            levelTime -= Time.fixedDeltaTime;
            timeUI.text = ShowGameTime(levelTime);
            if (levelTime <= 0)
                MakeRestartScreen();
        }
        else
            timeUI.gameObject.SetActive(false);
    }


    public string ShowGameTime(float gameTime)
    {
        int minutes = (int)gameTime / 60;
        int seconds = (int)gameTime - ((int)gameTime / 60) * 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public void MakeHP()
    {
        for (int i = 0; i < hpUI.Length; i++)
        {
            if (player.GetHealth() > i)
                hpUI[i].sprite = isLife;
            else
                hpUI[i].sprite = noLife;
        }
    }


    public void PauseScreenOn()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        pauseScreen.SetActive(true);
    }


    public void PauseScreenOff()
    {
        Time.timeScale = 1f;
        player.enabled = true;
        pauseScreen.SetActive(false);
    }


    public void Finish()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        finishScreen.SetActive(true);
        int lvl = Global.GetLevel();
        int lvlRating = GetLevelRating();
        sfxManager.PlayFinishSFX();

        if (!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") < lvl || lvl == Global.GetMaxLevel())
            PlayerPrefs.SetInt("Level", lvl);

        if (!PlayerPrefs.HasKey("Coins" + lvl.ToString()))
            PlayerPrefs.SetInt("Coins" + lvl.ToString(), player.GetCoins());

        if (!PlayerPrefs.HasKey("CoinsTotal"))
            PlayerPrefs.SetInt("CoinsTotal", player.GetCoins());
        else
            PlayerPrefs.SetInt("CoinsTotal", PlayerPrefs.GetInt("CoinsTotal") + player.GetCoins());

        if (!PlayerPrefs.HasKey("Time" + lvl.ToString()))
            PlayerPrefs.SetFloat("Time" + lvl.ToString(), levelTime);

        if (!PlayerPrefs.HasKey("Rating" + lvl.ToString()))
            PlayerPrefs.SetInt("Rating" + lvl.ToString(), lvlRating);

        PlayerPrefs.Save();

        finishCoinsUI.text = player.GetCoins().ToString("00");
        finishTimeUI.text = ShowGameTime(levelTime);
        MakeRatingStars(lvlRating);
    }


    private int GetLevelRating()
    {
        float coinRating = (float)player.GetCoins() / (float)coinBot.GetCreatedCoins();
        float timeRating = levelTimeAAA / levelTime;
        int rating = 0;

        if (coinRating + timeRating <= 0.7f) rating = 1;
        if (coinRating + timeRating > 0.7f) rating = 2;
        if (coinRating + timeRating >= 1.7f) rating = 3;

        return rating;
    }


    public void MakeRatingStars(int rating)
    {
        for (int i=rating-1; i>=0; i--)
        {
            Instantiate(
                ratingStar,
                new Vector3 (parentStar.transform.position.x - starDistance * (2*i - rating + 1),
                             parentStar.transform.position.y,
                             parentStar.transform.position.z),
                parentStar.transform.rotation);
        }
    }


    public void MakeRestartScreen()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        restartScreen.SetActive(true);
        sfxManager.PlayRestartSFX();
    }


    public void TurnOverCounter(bool state)
    {
        TurnOverCounterImage.SetActive(state);
    }
}


public enum TimeWork
{
    None,
    Stopwatch,
    Timer
}
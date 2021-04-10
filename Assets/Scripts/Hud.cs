using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class Hud : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text coinsUI, timeUI, finishCoinsUI, finishTimeUI;
    [SerializeField] private Image[] hpUI;
    [SerializeField] private Sprite isLife, noLife;
    [SerializeField] private GameObject finishScreen, restartScreen, pauseScreen;
    [FormerlySerializedAs("TurnOverCounterImage")] [SerializeField] private GameObject turnOverCounterImage;

    [SerializeField] private GameObject ratingStar;
    [SerializeField] private Transform parentStar;
    [SerializeField] private float starDistance = 1f;

    [SerializeField] private TimeWork timeWork;
    [SerializeField] private float countdown = 60f;
    private float _levelTime = 0f;
    [SerializeField] private readonly float _levelTimeAaa = 120f;

    [SerializeField] private CoinBot coinBot;
    [SerializeField] private SfxManager sfxManager;



    private void Start()
    {
        if ((int)timeWork == 2)
            _levelTime = countdown;

        Time.timeScale = 1f;
        player.enabled = true;
        TurnOverCounter(false);

        ShowCoinsUI();
        MakeHp();
    }


    private void FixedUpdate()
    {
        TimeManagement();
    }


    public void ShowCoinsUI()
    {
        coinsUI.text = player.GetCoins().ToString("00");
    }

    private void TimeManagement()
    {
        if ((int)timeWork == 1)
        {
            _levelTime += Time.fixedDeltaTime;
            timeUI.text = ShowGameTime(_levelTime);
        }
        else
        if ((int)timeWork == 2)
        {
            _levelTime -= Time.fixedDeltaTime;
            timeUI.text = ShowGameTime(_levelTime);
            if (_levelTime <= 0)
                MakeRestartScreen();
        }
        else
            timeUI.gameObject.SetActive(false);
    }


    private static string ShowGameTime(float gameTime)
    {
        var minutes = (int)gameTime / 60;
        var seconds = (int)gameTime - ((int)gameTime / 60) * 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public void MakeHp()
    {
        for (var i = 0; i < hpUI.Length; i++)
            hpUI[i].sprite = player.GetHealth() > i ? isLife : noLife;
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
        //Time.timeScale = 0f;
        player.enabled = false;
        finishScreen.GetComponent<Animator>().SetTrigger("Finish");
        var lvl = Global.Level;
        var lvlRating = GetLevelRating();
        sfxManager.PlayFinishSfx();

        if (!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") < lvl || lvl == Global.MaxLevel)
            PlayerPrefs.SetInt("Level", lvl);

        if (!PlayerPrefs.HasKey("Coins" + lvl.ToString()))
            PlayerPrefs.SetInt("Coins" + lvl.ToString(), player.GetCoins());

        if (!PlayerPrefs.HasKey("CoinsTotal"))
            PlayerPrefs.SetInt("CoinsTotal", player.GetCoins());
        else
            PlayerPrefs.SetInt("CoinsTotal", PlayerPrefs.GetInt("CoinsTotal") + player.GetCoins());

        if (!PlayerPrefs.HasKey("Time" + lvl.ToString()))
            PlayerPrefs.SetFloat("Time" + lvl.ToString(), _levelTime);

        if (!PlayerPrefs.HasKey("Rating" + lvl.ToString()))
            PlayerPrefs.SetInt("Rating" + lvl.ToString(), lvlRating);

        PlayerPrefs.Save();

        finishCoinsUI.text = player.GetCoins().ToString("00");
        finishTimeUI.text = ShowGameTime(_levelTime);
        MakeRatingStars(lvlRating);
    }


    private int GetLevelRating()
    {
        var coinRating = (float)player.GetCoins() / (float)coinBot.GetCreatedCoins();
        var timeRating = _levelTimeAaa / _levelTime;
        var rating = 0;

        if (coinRating + timeRating <= 0.7f) rating = 1;
        if (coinRating + timeRating > 0.7f) rating = 2;
        if (coinRating + timeRating >= 1.7f) rating = 3;

        return rating;
    }


    private void MakeRatingStars(int rating)
    {
        for (var i=rating-1; i>=0; i--)
        {
            var transform1 = parentStar.transform;
            var position = transform1.position;
            Instantiate(
                ratingStar,
                new Vector3 (position.x - starDistance * (2*i - rating + 1), position.y, position.z),
                transform1.rotation);
        }
    }


    public void MakeRestartScreen()
    {
        Time.timeScale = 0f;
        player.enabled = false;
        restartScreen.SetActive(true);
        sfxManager.PlayRestartSfx();
    }


    public void TurnOverCounter(bool state)
    {
        turnOverCounterImage.SetActive(state);
    }
}


public enum TimeWork
{
    None,
    Stopwatch,
    Timer
}
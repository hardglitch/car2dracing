﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsUI, timeUI, finishCoinsUI, finishTimeUI;
    [SerializeField] private Image[] hpUI;
    [SerializeField] private Sprite isLife, noLife;
    [SerializeField] private GameObject finishScreen, restartScreen, pauseScreen;
    [SerializeField] private GameObject turnOverCounterImage;

    internal Car Player { get; set; }
    internal GameObject AngleTimerObj => turnOverCounterImage;

    [SerializeField] private GameObject ratingStar;
    [SerializeField] private Transform parentStar;
    [SerializeField] private float starDistance = 1f;

    [SerializeField] private TimeWork timeWork;
    [SerializeField] private float countdown = 60f;
    private float _levelTime;
    private readonly float _levelTimeAaa = 2f * Global.LevelSize;

    [SerializeField] private CoinBot coinBot;
    [SerializeField] private SfxManager sfxManager;
    [SerializeField] private Scoreboard scoreboard;
    private static readonly int Finish1 = Animator.StringToHash("Finish");


    private void Start()
    {
        if ((int)timeWork == 2)
            _levelTime = countdown;

        Time.timeScale = 1f;
        Player.enabled = true;
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
        coinsUI.text = Player.Coins.ToString("00");
    }

    private void TimeManagement()
    {
        switch ((int)timeWork)
        {
            case 1:
                _levelTime += Time.fixedDeltaTime;
                timeUI.text = ShowGameTime(_levelTime);
                break;
            case 2:
                _levelTime -= Time.fixedDeltaTime;
                timeUI.text = ShowGameTime(_levelTime);
                if (_levelTime <= 0) MakeRestartScreen();
                break;
            default:
                timeUI.gameObject.SetActive(false);
                break;
        }
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
            hpUI[i].sprite = Player.Health > i ? isLife : noLife;
    }


    public void PauseScreenOn()
    {
        Time.timeScale = 0f;
        Player.enabled = false;
        pauseScreen.SetActive(true);
    }


    public void PauseScreenOff()
    {
        Time.timeScale = 1f;
        Player.enabled = true;
        pauseScreen.SetActive(false);
    }


    public void Finish()
    {
        // Player.enabled = false;
        finishScreen.GetComponent<Animator>().SetTrigger(Finish1);
        var lvl = Global.Level;
        var lvlRating = GetLevelRating();
        var finishTime = _levelTime;
        sfxManager.PlayFinishSfx();

        if (!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") < lvl || lvl == Global.MaxLevel)
            PlayerPrefs.SetInt("Level", lvl);

        if (!PlayerPrefs.HasKey("Coins" + lvl))
            PlayerPrefs.SetInt("Coins" + lvl, Player.Coins);

        if (!PlayerPrefs.HasKey("CoinsTotal"))
            PlayerPrefs.SetInt("CoinsTotal", Player.Coins);
        else
            PlayerPrefs.SetInt("CoinsTotal", PlayerPrefs.GetInt("CoinsTotal") + Player.Coins);

        if (!PlayerPrefs.HasKey("Time" + lvl))
            PlayerPrefs.SetFloat("Time" + lvl, finishTime);

        if (!PlayerPrefs.HasKey("Rating" + lvl))
            PlayerPrefs.SetInt("Rating" + lvl, lvlRating);

        PlayerPrefs.Save();

        finishCoinsUI.text = Player.Coins.ToString("00");
        finishTimeUI.text = ShowGameTime(finishTime);
        MakeRatingStars(lvlRating);
    }


    private int GetLevelRating()
    {
        var coinRating = Player.Coins / (float)coinBot.GetCreatedCoins();
        var timeRating = _levelTimeAaa / _levelTime;
        var placeRating = 4f / (scoreboard.playerPlace * 10);
        var rating = 0;

        if (coinRating + timeRating + placeRating <= 1.6f) rating = 1;
        if (coinRating + timeRating + placeRating > 1.6f) rating = 2;
        if (coinRating + timeRating + placeRating >= 2.3f) rating = 3;

        return rating;
    }


    private void MakeRatingStars(int rating)
    {
        for (var i=rating-1; i>=0; i--)
        {
            var position = parentStar.transform.position;
            var newStar = Instantiate(
                ratingStar,
                new Vector3 (position.x - starDistance * (2*i - rating + 1), position.y, position.z),
                Quaternion.identity);
            newStar.transform.SetParent(parentStar);
            
        }
    }


    public void MakeRestartScreen()
    {
        Time.timeScale = 0f;
        Player.enabled = false;
        restartScreen.SetActive(true);
        sfxManager.PlayRestartSfx();
    }

    public void GameContinue()
    {
        Time.timeScale = 1f;
        Player.enabled = true;
        restartScreen.SetActive(false);
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
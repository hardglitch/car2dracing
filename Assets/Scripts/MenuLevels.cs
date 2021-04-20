using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuLevels : MonoBehaviour
{
    [Header("Scrolling Content")]
    [SerializeField] private Button[] scrollingImages;

    [Range(1, 300)]
    [SerializeField] private int scrollingElementsDistance;

    [Range(0, 20)]
    [SerializeField] private float snapSpeed;

    [Range(0, 5)]
    [SerializeField] private float scaleOffset;

    [Range(0, 20)]
    [SerializeField] private float scaleSpeed;

    [Header("Other Objects")]

    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private MenuManager menuManager;


    private Vector2[] _scrollingElementsPositions;
    private RectTransform _contentRect;
    private int _selectedScrollingElementsID;
    private bool _isScrolling;
    private Vector2 _contentVector;
    private Vector2[] _scrollingElementsScale;
    private int _scrollingElementsCounter;
    private float[] _originalImageScalesX;
    private RectTransform[] _scrollingImagesRect;
    private float _nearestPos, _distance, _scrollVelocity;


    private void Start()
    {
        _scrollingElementsCounter = scrollingImages.Length;
        _scrollingElementsPositions = new Vector2[_scrollingElementsCounter];
        _scrollingElementsScale = new Vector2[_scrollingElementsCounter];
        _contentRect = GetComponent<RectTransform>();
        _originalImageScalesX = new float[_scrollingElementsCounter];
        _scrollingImagesRect = new RectTransform[_scrollingElementsCounter];


        // set hero settings
        if (PlayerPrefs.HasKey("Hero"))
            Global.Hero = PlayerPrefs.GetInt("Hero");

        // set car settings
        if (PlayerPrefs.HasKey("Car"))
            Global.Car = PlayerPrefs.GetInt("Car");


        for (var i = 0; i < _scrollingElementsCounter; i++)
        {
            try
            {
                // draw hero screens
                if (menuManager.IsHeroScreen())
                    DrawHeroScreen(i);

                // draw car screens
                if (menuManager.IsCarScreen())
                    DrawCarScreen(i);

                // draw level screens
                if (menuManager.IsLevelScreen())
                    DrawLevelScreen(i);
            }
            catch (Exception error)
            {
                Debug.LogError(error);
            }
        }
    }


    private void DrawHeroScreen(int i)
    {
        DrawScrollingElements(i);
    }


    private void DrawCarScreen(int i)
    {
        Global.MaxCar = _scrollingElementsCounter;
        DrawScrollingElements(i);
    }


    private void DrawLevelScreen(int i)
    {
        Global.MaxLevel = _scrollingElementsCounter;

        string tmptext;

        // get records
        // coins
        tmptext = PlayerPrefs.HasKey("Coins" + (i + 1)) ? PlayerPrefs.GetInt("Coins" + (i + 1)).ToString("D5") : "00000";

        scrollingImages[i].transform.Find("Coins").GetComponent<TMP_Text>().text = tmptext;

        // time
        tmptext = PlayerPrefs.HasKey("Time" + (i + 1)) ? ShowTime(PlayerPrefs.GetFloat("Time" + (i + 1))) : "00:00";

        scrollingImages[i].transform.Find("Time").GetComponent<TMP_Text>().text = tmptext;

        // set level settings
        if (PlayerPrefs.HasKey("Level") && i <= PlayerPrefs.GetInt("Level") || i == 0)
            scrollingImages[i].interactable = true;
        else
            scrollingImages[i].interactable = false;

        // draw level screen
        DrawScrollingElements(i);
    }


    private void DrawScrollingElements(int i)
    {
        _scrollingImagesRect[i] = scrollingImages[i].GetComponent<RectTransform>();

        _scrollingImagesRect[i].anchoredPosition = new Vector2(
            (_scrollingImagesRect[i].sizeDelta.x + scrollingElementsDistance) * i, 0);

        _scrollingElementsPositions[i] = -_scrollingImagesRect[i].anchoredPosition;
        _originalImageScalesX[i] = _scrollingImagesRect[i].localScale.x;
    }


    private void FixedUpdate()
    {
        if (_contentRect.anchoredPosition.x <= _scrollingElementsPositions[0].x && !_isScrolling ||
            _contentRect.anchoredPosition.x >= _scrollingElementsPositions[_scrollingElementsPositions.Length - 1].x &&
            !_isScrolling)
            scrollRect.inertia = false;


        _nearestPos = float.MaxValue;
        for (var i = 0; i < _scrollingElementsCounter; i++)
        {
            _distance = Mathf.Abs(_contentRect.anchoredPosition.x - _scrollingElementsPositions[i].x);
            if (_distance < _nearestPos)
            {
                _nearestPos = _distance;
                _selectedScrollingElementsID = i;
            }
            SnapElement(i);
        }

        _scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

        if (_isScrolling && !(_scrollVelocity < 400)) return;
        scrollRect.inertia = false;
        _contentVector.x = Mathf.SmoothStep(
            _contentRect.anchoredPosition.x,
            _scrollingElementsPositions[_selectedScrollingElementsID].x,
            snapSpeed * Time.fixedDeltaTime);
        _contentRect.anchoredPosition = _contentVector;
    }


    private void SnapElement(int id)
    {
        Vector2 imageScale = _scrollingImagesRect[id].localScale;

        var scale = Mathf.Clamp(
            _originalImageScalesX[id] / (_distance / scrollingElementsDistance) * scaleOffset,
            0.5f * _originalImageScalesX[id],
            _originalImageScalesX[id]);

        _scrollingElementsScale[id] = new Vector2(
            Mathf.SmoothStep(imageScale.x, scale, scaleSpeed * Time.fixedDeltaTime),
            Mathf.SmoothStep(imageScale.y, scale, scaleSpeed * Time.fixedDeltaTime));

        _scrollingImagesRect[id].localScale = _scrollingElementsScale[id];
    }

    public void Scrolling(bool scrolling)
    {
        _isScrolling = scrolling;
        if (_isScrolling) scrollRect.inertia = true;
    }


    private static string ShowTime(float gameTime)
    {
        var minutes = (int)gameTime / 60;
        var seconds = (int)gameTime - ((int)gameTime / 60) * 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public void DeleteAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public int GetScrollingElementCounter()
    {
        return _scrollingElementsCounter;
    }
}

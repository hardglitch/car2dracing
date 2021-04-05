using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


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


    private Vector2[] scrollingElementsPositions;
    private RectTransform contentRect;
    private int selectedScrollingElementsID;
    private bool isScrolling = false;
    private Vector2 contentVector = new Vector2();
    private Vector2[] scrollingElementsScale;
    [NonSerialized] public int scrollingElementsCounter;
    private float[] originalImageScalesX;
    private RectTransform[] scrollingImagesRect;
    private float nearestPos, distance, scrollVelocity;


    void Start()
    {
        scrollingElementsCounter = scrollingImages.Length;
        scrollingElementsPositions = new Vector2[scrollingElementsCounter];
        scrollingElementsScale = new Vector2[scrollingElementsCounter];
        contentRect = GetComponent<RectTransform>();
        originalImageScalesX = new float[scrollingElementsCounter];
        scrollingImagesRect = new RectTransform[scrollingElementsCounter];


        // set hero settings
        if (PlayerPrefs.HasKey("Hero"))
            Global.SetHero(PlayerPrefs.GetInt("Hero"));

        // set car settings
        if (PlayerPrefs.HasKey("Car"))
            Global.SetCar(PlayerPrefs.GetInt("Car"));


        for (int i = 0; i < scrollingElementsCounter; i++)
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
        Global.SetMaxCar(scrollingElementsCounter);
        DrawScrollingElements(i);
    }


    private void DrawLevelScreen(int i)
    {
        Global.SetMaxLevel(scrollingElementsCounter);

        string _tmptext;

        // get records
        // coins
        if (PlayerPrefs.HasKey("Coins" + (i + 1).ToString()))
            _tmptext = PlayerPrefs.GetInt("Coins" + (i + 1).ToString()).ToString("D5");
        else
            _tmptext = "00000";

        scrollingImages[i].transform.Find("Coins").GetComponent<TMP_Text>().text = _tmptext;

        // time
        if (PlayerPrefs.HasKey("Time" + (i + 1).ToString()))
            _tmptext = ShowTime(PlayerPrefs.GetFloat("Time" + (i + 1).ToString()));
        else
            _tmptext = "00:00";

        scrollingImages[i].transform.Find("Time").GetComponent<TMP_Text>().text = _tmptext;

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
        scrollingImagesRect[i] = scrollingImages[i].GetComponent<RectTransform>();

        scrollingImagesRect[i].anchoredPosition = new Vector2(
            (scrollingImagesRect[i].sizeDelta.x + scrollingElementsDistance) * i, 0);

        scrollingElementsPositions[i] = -scrollingImagesRect[i].anchoredPosition;
        originalImageScalesX[i] = scrollingImagesRect[i].localScale.x;
    }


    void FixedUpdate()
    {
        if (contentRect.anchoredPosition.x <= scrollingElementsPositions[0].x && !isScrolling ||
            contentRect.anchoredPosition.x >= scrollingElementsPositions[scrollingElementsPositions.Length - 1].x &&
            !isScrolling)
            scrollRect.inertia = false;


        nearestPos = float.MaxValue;
        for (int i = 0; i < scrollingElementsCounter; i++)
        {
            distance = Mathf.Abs(contentRect.anchoredPosition.x - scrollingElementsPositions[i].x);
            if (distance < nearestPos)
            {
                nearestPos = distance;
                selectedScrollingElementsID = i;
            }
            SnapElement(i);
        }

        scrollVelocity = Mathf.Abs(scrollRect.velocity.x);

        if (!isScrolling || scrollVelocity < 400)
        {
            scrollRect.inertia = false;
            contentVector.x = Mathf.SmoothStep(
                contentRect.anchoredPosition.x,
                scrollingElementsPositions[selectedScrollingElementsID].x,
                snapSpeed * Time.fixedDeltaTime);
            contentRect.anchoredPosition = contentVector;
        }
    }


    private void SnapElement(int id)
    {
        Vector2 imageScale = scrollingImagesRect[id].localScale;

        float scale = Mathf.Clamp(
            originalImageScalesX[id] / (distance / scrollingElementsDistance) * scaleOffset,
            0.5f * originalImageScalesX[id],
            originalImageScalesX[id]);

        scrollingElementsScale[id] = new Vector2(
            Mathf.SmoothStep(imageScale.x, scale, scaleSpeed * Time.fixedDeltaTime),
            Mathf.SmoothStep(imageScale.y, scale, scaleSpeed * Time.fixedDeltaTime));

        scrollingImagesRect[id].localScale = scrollingElementsScale[id];
    }

    public void Scrolling(bool scrolling)
    {
        isScrolling = scrolling;
        if (isScrolling) scrollRect.inertia = true;
    }


    public string ShowTime(float gameTime)
    {
        int minutes = (int)gameTime / 60;
        int seconds = (int)gameTime - ((int)gameTime / 60) * 60;

        return minutes.ToString("00") + ":" + seconds.ToString("00");
    }


    public void DeleteAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }
}

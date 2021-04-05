using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelScreen, heroScreen, carScreen;


    public void HeroScreenDone()
    {
        heroScreen.SetActive(false);
        carScreen.SetActive(true);
    }

    public void CarScreenDone()
    {
        carScreen.SetActive(false);
        levelScreen.SetActive(true);
    }

    public bool IsHeroScreen()
    {
        return heroScreen.activeSelf;
    }
    public bool IsCarScreen()
    {
        return carScreen.activeSelf;
    }
    public bool IsLevelScreen()
    {
        return levelScreen.activeSelf;
    }
}

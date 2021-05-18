using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using UnityEngine;

public class ADSettings : MonoBehaviour, IInterstitialAdListener, IBannerAdListener, IRewardedVideoAdListener
{
    public static Car CarObj { get; set; }
    public static Hud HudObj { get; set; }
    
    private const string AppKey = "8b2cc1769646168c36cf9af82bbc775d54fcc0bf68a97814";
    private const int ADTypes = Appodeal.BANNER_BOTTOM | Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO;

    private void Start()
    {
        Appodeal.initialize(AppKey, ADTypes,true);
        Appodeal.setInterstitialCallbacks(this);
        Appodeal.setBannerCallbacks(this);
        Appodeal.setRewardedVideoCallbacks(this);
    }

    public void ShowInterstitial()
    {
        if (Appodeal.canShow(Appodeal.INTERSTITIAL) && !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
            Appodeal.show(Appodeal.INTERSTITIAL);
    }

    public void ShowRewardedVideo()
    {
        if (Appodeal.canShow(Appodeal.REWARDED_VIDEO, "PaymentForHP")
            && !Appodeal.isPrecache(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO, "PaymentForHP");
        }
    }

    public void ShowBanner()
    {
        Appodeal.show(Appodeal.BANNER_BOTTOM);
    }
        
    public void HideBanner()
    {
        Appodeal.hide(Appodeal.BANNER_BOTTOM);
    }

    #region Interfaces Realisations
    public void onInterstitialLoaded(bool isPrecache)
    {
        Debug.Log("onInterstitialLoaded");
    }

    public void onInterstitialFailedToLoad()
    {
        Debug.Log("onInterstitialFailedToLoad");
    }

    public void onInterstitialShowFailed()
    {
        Debug.Log("onInterstitialShowFailed");
    }

    public void onInterstitialShown()
    {
        Debug.Log("onInterstitialShown");
    }

    public void onInterstitialClosed()
    {
        Debug.Log("onInterstitialClosed");
    }

    public void onInterstitialClicked()
    {
        Debug.Log("onInterstitialClicked");
    }

    public void onInterstitialExpired()
    {
        Debug.Log("onInterstitialExpired");
    }

    public void onBannerLoaded(int height, bool isPrecache)
    {
        Debug.Log("onBannerLoaded");
    }

    public void onBannerFailedToLoad()
    {
        Debug.Log("onBannerFailedToLoad");
    }

    public void onBannerShown()
    {
        Debug.Log("onBannerShown");
    }

    public void onBannerClicked()
    {
        Debug.Log("onBannerClicked");
    }

    public void onBannerExpired()
    {
        Debug.Log("onBannerExpired");
    }

    public void onRewardedVideoLoaded(bool precache)
    {
        Debug.Log("onRewardedVideoLoaded");
    }

    public void onRewardedVideoFailedToLoad()
    {
        Debug.Log("onRewardedVideoFailedToLoad");
    }

    public void onRewardedVideoShowFailed()
    {
        Debug.Log("onRewardedVideoShowFailed");
    }

    public void onRewardedVideoShown()
    {
        Debug.Log("onRewardedVideoShown");
    }

    public void onRewardedVideoFinished(double amount, string name)
    {
        Debug.Log("onRewardedVideoFinished");
        CarObj.RecountHealth(+1);
        HudObj.GameContinue();
    }

    public void onRewardedVideoClosed(bool finished)
    {
        Debug.Log("onRewardedVideoClosed");
    }

    public void onRewardedVideoExpired()
    {
        Debug.Log("onRewardedVideoExpired");
    }

    public void onRewardedVideoClicked()
    {
        Debug.Log("onRewardedVideoClicked");
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

using UnityEngine.Advertisements;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;

#if UNITY_IOS
    private string gameId="4127348";
    private string appId="ca-app-pub-4962234576866611~5838634512";
    private string intersitionalId="ca-app-pub-4962234576866611/4605443891";
#else
    private string gameId="4127349";
    private string appId="ca-app-pub-4962234576866611~2637966894";
    private string intersitionalId="ca-app-pub-4962234576866611/8842925350";
#endif


    public GameObject loadingPanel;

    void Start(){
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => {
            LoadLoadInterstitialAd();
        });
        //RequestBannerAd();

        Advertisement.Initialize(gameId,false);
    }

    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);
        
        showIntersitionalAd();
    }

    public void resume(){
        Time.timeScale=1;
        pausePanel.SetActive(false);
    }

    public void openScene(int id){
        Time.timeScale=1;
        loadingPanel.SetActive(true);
        Application.LoadLevelAsync(id);
    }

    public void restart(){
        openScene(Application.loadedLevel);
    }

    public void nextLevel(){
        openScene(Application.loadedLevel+1);
    }

    public void deleteProgress(){
        PlayerPrefs.DeleteAll();
        openScene(0);
    }

      public bool showIntersitionalAd(){
          return showIntersitionalGoogleAd();
      }

      private InterstitialAd _interstitialAd;
    
    public void LoadLoadInterstitialAd()
    {
        // Clean up the old ad before loading a new one.
        if (_interstitialAd != null)
        {
                _interstitialAd.Destroy();
                _interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");

        // create our request used to load the ad.
        var adRequest = new AdRequest();

        // send the request to load the ad.
        InterstitialAd.Load(intersitionalId, adRequest,
            (InterstitialAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("interstitial ad failed to load an ad " +
                                    "with error : " + error);
                    return;
                }

                Debug.Log("Interstitial ad loaded with response : "
                            + ad.GetResponseInfo());

                _interstitialAd = ad;
            });
    }


      public bool showIntersitionalGoogleAd(){
        if (_interstitialAd != null && _interstitialAd.CanShowAd())
        {
            _interstitialAd.Show();

            return true;
        }
        else
        {
            return false;
        }
      }
}

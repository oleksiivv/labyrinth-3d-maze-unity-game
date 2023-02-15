using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

using UnityEngine.Advertisements;

public class UIController : MonoBehaviour
{
    public GameObject pausePanel;

    private string gameId="4127349";

    public GameObject loadingPanel;

    void Start(){
        RequestConfiguration requestConfiguration =
            new RequestConfiguration.Builder()
            .SetSameAppKeyEnabled(true).build();
        MobileAds.SetRequestConfiguration(requestConfiguration);

        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(initStatus => { });
        //RequestBannerAd();
        RequestConfigurationAd();

        Advertisement.Initialize(gameId,false);
    }

    public void pause(){
        Time.timeScale=0;
        pausePanel.SetActive(true);
        if(!showIntersitionalAd()){
            if(Advertisement.IsReady("video")){
                Advertisement.Show("video");
            }
        }
        
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




    private InterstitialAd intersitional;
    private string appId="ca-app-pub-4962234576866611~2637966894";
    private string intersitionalId="ca-app-pub-4962234576866611/8842925350";

     AdRequest AdRequestBuild(){
         return new AdRequest.Builder().Build();
     }


      void RequestConfigurationAd(){
          intersitional=new InterstitialAd(intersitionalId);
          AdRequest request=AdRequestBuild();
          intersitional.LoadAd(request);
          intersitional.OnAdLoaded+=this.HandleOnAdLoaded;
          intersitional.OnAdOpening+=this.HandleOnAdOpening;
          intersitional.OnAdClosed+=this.HandleOnAdClosed;

    }


      public bool showIntersitionalAd(){
          if(intersitional.IsLoaded()){
              intersitional.Show();

              return true;
          }

          return false;
      }

      private void OnDestroy(){
          DestroyIntersitional();

          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

      }

      private void HandleOnAdClosed(object sender, EventArgs e)
      {
          intersitional.OnAdLoaded-=this.HandleOnAdLoaded;
          intersitional.OnAdOpening-=this.HandleOnAdOpening;
          intersitional.OnAdClosed-=this.HandleOnAdClosed;

          RequestConfigurationAd();
      }

     private void HandleOnAdOpening(object sender, EventArgs e)
     {
        
     }

     private void HandleOnAdLoaded(object sender, EventArgs e)
     {
        
     }

     public void DestroyIntersitional(){
        intersitional.Destroy();
     }

    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdMobController : MonoBehaviour
{
    private BannerView banner;

    private string appId="ca-app-pub-4962234576866611~2637966894";
    private string bannerId="ca-app-pub-4962234576866611/8449530928";
    
    void Start(){
        //MobileAds.Initialize(appId);
        //RequestBannerAd();
        
    }

    AdRequest AdRequestBuild(){
        return new AdRequest.Builder().Build();
    }



    //baner

    public void RequestBannerAd(){
        banner=new BannerView(bannerId,AdSize.Banner,AdPosition.Bottom);
        AdRequest request = AdRequestBannerBuild();
        banner.LoadAd(request);
    }

    public void DestroyBanner(){
        if(banner!=null){
            banner.Destroy();
        }
    }



    AdRequest AdRequestBannerBuild(){
        return new AdRequest.Builder().Build();
    }
}

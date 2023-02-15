using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;
using System;

public class CharacterTriggersController : MonoBehaviour
{

    public GameObject winPanel,diePanel;
    public ParticleSystem winParticles, starParticles;

    private SphereCollider collider;
    public Rigidbody rigidbody;

    CharacterMoveController character;
    public StarsCharacterController stars;

    public int level=1;

#if UNITY_IOS
    private string gameId="4127348";
    private string appId="ca-app-pub-4962234576866611~5838634512";
    private string intersitionalId="ca-app-pub-4962234576866611/4605443891";
#else
    private string gameId="4127349";
    private string appId="ca-app-pub-4962234576866611~2637966894";
    private string intersitionalId="ca-app-pub-4962234576866611/8842925350";
#endif

    public Text gemsCnt;

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
        collider=GetComponent<SphereCollider>();
        rigidbody=GetComponent<Rigidbody>();
        character=GetComponent<CharacterMoveController>();

        if(PlayerPrefs.GetInt("Completed"+level.ToString())!=1){
            PlayerPrefs.SetInt("starsAt"+level.ToString(),0);
        }

        gemsCnt.GetComponent<Text>().text=PlayerPrefs.GetInt("gems").ToString();
    }
       void OnTriggerEnter(Collider other){
           if(winPanel.activeSelf==true || diePanel.activeSelf==true)return;

            if(other.gameObject.tag=="hole"){
                //rigidbody.isKinematic=true;
                //Destroy(rigidbody);
                rigidbody.velocity = Vector3.zero;
                rigidbody.angularVelocity = Vector3.zero; 
                character.movable=0;
                gameObject.transform.position=new Vector3(other.gameObject.transform.position.x,gameObject.transform.position.y, other.gameObject.transform.position.z);
                collider.isTrigger=true;
                Invoke(nameof(showDiePanel),1.2f);
                StopAllCoroutines();
            }
            if(other.gameObject.tag=="finish"){
                winParticles.Play();
                Invoke(nameof(showWinPanel),0.5f);
                PlayerPrefs.SetInt("Completed"+level.ToString(),1);

                if(level<18)PlayerPrefs.SetInt("JunLevel"+(level+1).ToString(),1);
                else if(level<36)PlayerPrefs.SetInt("MidLevel"+(level+1-18).ToString(),1);
                else PlayerPrefs.SetInt("ProLevel"+(level+1-36).ToString(),1);

                rigidbody.velocity=Vector3.zero;
                StopAllCoroutines();

            }

            if(other.gameObject.tag=="star"){
                other.gameObject.SetActive(false);
                starParticles.Play();
                PlayerPrefs.SetInt("starsAt"+level.ToString(),PlayerPrefs.GetInt("starsAt"+level.ToString())+1);
                stars.checkActive();
                

                PlayerPrefs.SetInt("AvailStar_"+other.gameObject.name.ToString()+"/"+level.ToString(),1);
            }

            if(other.gameObject.name.Contains("fire")){
                //if(other.gameObject.GetComponent<FireObstacle>().getFireMode()==true){
                    rigidbody.AddForce(Vector3.up*1000);
                    
                    character.movable=0;
                    //gameObject.transform.position=new Vector3(other.gameObject.transform.position.x,gameObject.transform.position.y, other.gameObject.transform.position.z);
                    collider.isTrigger=true;
                    Invoke(nameof(showDiePanel),1.2f);
                    StopAllCoroutines();

                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero; 
                    rigidbody.useGravity=false;
                    //Destroy(rigidbody);
            //    }
            }

            if(other.gameObject.tag=="gem"){
                PlayerPrefs.SetInt("gems",PlayerPrefs.GetInt("gems")+1);
                other.gameObject.SetActive(false);
                starParticles.Play();

                gemsCnt.GetComponent<Text>().text=PlayerPrefs.GetInt("gems").ToString();

            }

        
    }

    void OnTriggerStay(Collider other){
        if(winPanel.activeSelf==true || diePanel.activeSelf==true)return;

        if(other.gameObject.tag=="magnet"){
            //gameObject.transform.position=Vector3.MoveTowards(gameObject.transform.position,other.transform.position,0.8f);
            //rigidbody.AddForceAtPosition(Vector3.forward*-1,other.transform.position);

            rigidbody.AddForce((other.gameObject.transform.position - gameObject.transform.position)*0.45f);
        }
        if(other.gameObject.name.Contains("fire")){
                //if(/*other.gameObject.GetComponent<FireObstacle>().fire.isPlaying*/other.gameObject.GetComponent<FireObstacle>().getFireMode()==true){
                    rigidbody.AddForce(Vector3.up*1000);
                    
                    character.movable=0;
                    //gameObject.transform.position=new Vector3(other.gameObject.transform.position.x,gameObject.transform.position.y, other.gameObject.transform.position.z);
                    collider.isTrigger=true;
                    Invoke(nameof(showDiePanel),1.2f);
                    StopAllCoroutines();

                    rigidbody.velocity = Vector3.zero;
                    rigidbody.angularVelocity = Vector3.zero; 
                    rigidbody.useGravity=false;
                    //Destroy(rigidbody);
                //}
            }
    }

    static int addCnt=0;
    private bool showed = false;
    public void showDiePanel(){
        diePanel.SetActive(true);

        if(!showed && addCnt%2==1){
            if(Advertisement.IsReady("video")){
                Advertisement.Show("video");
                showed = true;
            }
            else{
                showIntersitionalAd();
                showed = true;
            }
        }
        addCnt++;
    }

    void showWinPanel(){
        winPanel.SetActive(true);
    }








    private InterstitialAd intersitional;

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

          //RequestConfigurationAd();

        
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject junPanel,midPanel, proPanel;
    public Image[] levelsJun;
    public Image[] levelsMid;
    public Image[] levelsPro;

    //public Image[] keyJun;
    //public Image[] keyMid;
    //public Image[] keyPro;

    public Color32 activeLevelColor, closedLevelColor;

    public Text[] starsJun;
    public Text[] starsMid;
    public Text[] starsPro;

    public Text totalStarsJun,totalStarsMid,totalStarsPro,totalStars;

    public int totalStarsNumberJun,totalStarsNumberMid, totalStarsNumberPro;

    public GameObject levelsBg;

    public GameObject midReqPanel,proReqPanel;

    public GameObject loadingPanel;


    void Start(){
        totalStarsNumberJun=0;
        totalStarsNumberMid=0;
        totalStarsNumberPro=0;

        for(int i=1;i<levelsJun.Length;i++){
            if(PlayerPrefs.GetInt("JunLevel"+i.ToString())==1){
                levelsJun[i].GetComponent<Image>().color=activeLevelColor;
                //keyJun[i-1].gameObject.SetActive(false);
            }
            else{
                levelsJun[i].GetComponent<Image>().color=closedLevelColor;
                //keyJun[i-1].gameObject.SetActive(true);
                starsJun[i].gameObject.SetActive(false);
            }
        }

        for(int i=1;i<levelsMid.Length;i++){
            if(PlayerPrefs.GetInt("MidLevel"+i.ToString())==1){
                levelsMid[i].GetComponent<Image>().color=activeLevelColor;
                //keyMid[i-1].gameObject.SetActive(false);
            }
            else{
                levelsMid[i].GetComponent<Image>().color=closedLevelColor;
                //keyMid[i-1].gameObject.SetActive(true);
                starsMid[i].gameObject.SetActive(false);
            }
        }

        for(int i=1;i<levelsPro.Length;i++){
            if(PlayerPrefs.GetInt("ProLevel"+i.ToString())==1){
                levelsPro[i].GetComponent<Image>().color=activeLevelColor;
                //keyPro[i-1].gameObject.SetActive(false);
            }
            else{
                levelsPro[i].GetComponent<Image>().color=closedLevelColor;
                //keyPro[i-1].gameObject.SetActive(true);
                starsPro[i].gameObject.SetActive(false);
            }
        }


        for(int i=0;i<starsJun.Length;i++){
            totalStarsNumberJun+=PlayerPrefs.GetInt("starsAt"+i.ToString());
            
            starsJun[i].GetComponent<Text>().text=PlayerPrefs.GetInt("starsAt"+i.ToString()).ToString()+"/3";
        }

        for(int i=0;i<starsMid.Length;i++){
            totalStarsNumberMid+=PlayerPrefs.GetInt("starsAt"+(i+starsJun.Length).ToString());
          
            starsMid[i].GetComponent<Text>().text=PlayerPrefs.GetInt("starsAt"+(i+18).ToString()).ToString()+"/3";
        }

        for(int i=0;i<starsPro.Length;i++){
            totalStarsNumberPro+=PlayerPrefs.GetInt("starsAt"+(i+starsJun.Length+starsMid.Length).ToString());
           
            starsPro[i].GetComponent<Text>().text=PlayerPrefs.GetInt("starsAt"+(i+18+18).ToString()).ToString()+"/3";
        }

        totalStarsJun.GetComponent<Text>().text="Collected: "+totalStarsNumberJun.ToString()+"/54";
        totalStarsMid.GetComponent<Text>().text="Collected: "+totalStarsNumberMid.ToString()+"/54";
        totalStarsPro.GetComponent<Text>().text="Collected: "+totalStarsNumberPro.ToString()+"/54";

        int totalStarsCnt=totalStarsNumberJun+totalStarsNumberMid+totalStarsNumberPro;
        totalStars.GetComponent<Text>().text="Total collected: "+(totalStarsCnt).ToString()+"/"+(54*3).ToString();

        if(totalStarsCnt>=36){
            midReqPanel.SetActive(false);
        }
        if(totalStarsCnt>=87){
            proReqPanel.SetActive(false);
        }
    }


    public void openJunPanel(){
        levelsBg.SetActive(true);
        junPanel.SetActive(true);
    }
    public void openMidPanel(int minStarsNumber){
        if(midReqPanel.activeSelf==false){
            midPanel.SetActive(true);
            levelsBg.SetActive(true);
        }
    }

    public void openProPanel(int minStarsNumber){
        if(proReqPanel.activeSelf==false){
            proPanel.SetActive(true);
            levelsBg.SetActive(true);
        }
    }


    public void closePanel(){
        junPanel.SetActive(false);
        midPanel.SetActive(false);
        proPanel.SetActive(false);

        levelsBg.SetActive(false);
    }

    void openScene(int id){
        loadingPanel.SetActive(true);
        Application.LoadLevelAsync(id);
    }


    public void openLevel(int id){
        if(id<19){
            if(PlayerPrefs.GetInt("JunLevel"+(id-1).ToString())==1 || id==1){
                loadingPanel.SetActive(true);
                Application.LoadLevelAsync(id);
            }
        }
        else if(id>18 && id < 37){
            if(PlayerPrefs.GetInt("MidLevel"+(id-18-1).ToString())==1 || id==19){
                loadingPanel.SetActive(true);
                Application.LoadLevelAsync(id);
            }
        }
        else{
            if(PlayerPrefs.GetInt("ProLevel"+(id-18-18-1).ToString())==1 || id==19+18){
                loadingPanel.SetActive(true);
                Application.LoadLevelAsync(id);
            }
        }
    }

    public GameObject settingsPanel;
    public GameObject shopPanel;

    public void openSettings(){
        settingsPanel.SetActive(true);
    }
    public void openShop(){
        shopPanel.SetActive(true);
    }

    public void closeSettings(){
        settingsPanel.SetActive(false);
    }
    public void closeShop(){
        shopPanel.SetActive(false);
    }

}

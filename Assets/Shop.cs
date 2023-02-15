using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Image[] items;
    public GameObject[] buyEquipment;
    public GameObject[] chooseEquipment;

    public int[] prices;
    public Color32 normalItemColor;
    public Color32 currentItemColor;
    public Color32 availableItemColor;
    public Text[] pricesText;

    public Text gemsCnt;
    

    void Start(){
        PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems")+10000);
        for(int i=0;i<prices.Length;i++){
            pricesText[i].GetComponent<Text>().text=prices[i].ToString();
        }
        showUpdatedItems();
    }

    void Update(){

    }

    public void buy(int id){
        if(PlayerPrefs.GetInt("gems")<prices[id])return;

        PlayerPrefs.SetInt("gems", PlayerPrefs.GetInt("gems")-prices[id]);
        PlayerPrefs.SetInt(("playerMaterial"+id.ToString()),1);
        PlayerPrefs.SetInt("CurrentPlayerMaterial",id);

        showUpdatedItems();
    }

    public void choose(int id){
        if(PlayerPrefs.GetInt("playerMaterial"+id.ToString())==1 || id==0){
            PlayerPrefs.SetInt("CurrentPlayerMaterial",id);

            showUpdatedItems();
        }
    }


    public void showUpdatedItems(){

        gemsCnt.GetComponent<Text>().text=PlayerPrefs.GetInt("gems").ToString();
        for(int i=0;i<items.Length;i++){
            if(PlayerPrefs.GetInt("playerMaterial"+i.ToString())==1 || i==0){
                items[i].GetComponent<Image>().color=availableItemColor;
                chooseEquipment[i].gameObject.SetActive(true);
                buyEquipment[i].gameObject.SetActive(false);
            }
            else{
                items[i].GetComponent<Image>().color=normalItemColor;
                buyEquipment[i].gameObject.SetActive(true);
                chooseEquipment[i].gameObject.SetActive(false);
            }
        }

        chooseEquipment[PlayerPrefs.GetInt("CurrentPlayerMaterial")].gameObject.SetActive(false);
        buyEquipment[PlayerPrefs.GetInt("CurrentPlayerMaterial")].gameObject.SetActive(false);
        items[PlayerPrefs.GetInt("CurrentPlayerMaterial")].GetComponent<Image>().color=currentItemColor;
    }
    
}

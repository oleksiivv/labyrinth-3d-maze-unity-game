using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarsCharacterController : MonoBehaviour
{

    public GameObject[] stars;
    public Image[] uiStars;

    public Color32 activeStarColor;
    private CharacterTriggersController character;

    void Start(){
        
        character=GetComponent<CharacterTriggersController>();

        if(PlayerPrefs.GetInt("Completed"+character.level.ToString())!=1){
            PlayerPrefs.SetInt("starsAt"+character.level.ToString(),0);

            foreach(var star in stars){
                PlayerPrefs.SetInt("AvailStar_"+star.gameObject.name.ToString()+"/"+character.level.ToString(),0);
            }
        }


        getAllActive();
    }

    public void checkActive(){
        int curr=PlayerPrefs.GetInt("starsAt"+character.level.ToString());

        uiStars[curr-1].GetComponent<Animator>().enabled=true;
    }

    public void getAllActive(){
        int curr=PlayerPrefs.GetInt("starsAt"+character.level.ToString());
        for(int i=0;i<curr;i++){
            uiStars[i].GetComponent<Image>().color=activeStarColor;
        }

        foreach(var star in stars){
            if(PlayerPrefs.GetInt("AvailStar_"+star.gameObject.name.ToString()+"/"+character.level.ToString())==1){
                star.SetActive(false);
            }
            else{
                star.SetActive(true);
            }
        }
    }

}

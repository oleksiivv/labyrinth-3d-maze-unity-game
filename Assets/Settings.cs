using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public GameObject soundOn;
    public GameObject soundOff;

    public GameObject undoPanel;

    public GameObject[] audioHolders;

    void Start(){
        setAudioPlayable();
    }
    public void showUndoPanel(){
        undoPanel.SetActive(true);
    }

    public void closeUndoPanel(){
        undoPanel.SetActive(false);
    }


    public void undoProgress(){
        
        PlayerPrefs.DeleteAll();
        closeUndoPanel();
        Application.LoadLevel(Application.loadedLevel);

    }

    public void mute(){
        PlayerPrefs.SetInt("muted",1);
        setAudioPlayable();
    }

    public void unmute(){
        PlayerPrefs.SetInt("muted",0);
        setAudioPlayable();
    }

    public void setAudioPlayable(){
        int muted=PlayerPrefs.GetInt("muted");
        if(muted==0){
            foreach(var holder in audioHolders){
                holder.GetComponent<AudioSource>().enabled=true;
            }
            soundOn.SetActive(false);
            soundOff.SetActive(true);
        }
        else{
            foreach(var holder in audioHolders){
                holder.GetComponent<AudioSource>().enabled=false;
            }
            soundOn.SetActive(true);
            soundOff.SetActive(false);
        }
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource[] sound,music;
    // Start is called before the first frame update
    void Start()
    {
        updateSound();
        updateMusic();
    }


    public void updateSound(){
        foreach(var s in sound){
            if(PlayerPrefs.GetInt("muted")==0){

                s.enabled=true;

            }
            else{
                s.enabled=false;
            }
        }
    }

    public void updateMusic(){
        foreach(var m in music){
            if(PlayerPrefs.GetInt("muted")==0){

                m.enabled=true;

            }
            else{
                m.enabled=false;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource source;

    void Awake(){
        source.enabled=false;
        source.clip=clips[Random.Range(0,clips.Length)];

        source.enabled=PlayerPrefs.GetInt("!music")==0;
    }
}

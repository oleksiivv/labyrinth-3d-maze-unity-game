using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireObstacle : MonoBehaviour
{
    public ParticleSystem fire;

    private bool fireOn=true;

    void Start(){
        //fireOn=true;
        if(fire==null){
            fire=gameObject.transform.GetChild(0).GetComponent<ParticleSystem>();
        }
        //StartCoroutine(runtime());
        Invoke(nameof(StartRuntime),2);
    }

    void StartRuntime(){
        StartCoroutine(runtime());
    }

    IEnumerator runtime(){
        while(true){
            fire.gameObject.SetActive(true);

            //GetComponent<BoxCollider>().enabled=!GetComponent<BoxCollider>().enabled;
            
            if(!fireOn){
                this.fireOn=true;
                //fire.loop=true;
                fire.Play();

                GetComponent<BoxCollider>().enabled=true;
                yield return new WaitForSeconds(4);
            }
            if(fireOn){
                this.fireOn=false;
                fire.Stop(true);

                GetComponent<BoxCollider>().enabled=false;
                yield return new WaitForSeconds(4);
            }
            
            
        }
    }

    public bool getFireMode(){
        return fireOn;
    }
}

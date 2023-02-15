using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMidLevel : MonoBehaviour
{
    public Text cntText;
    private int cnt=0;

    public CharacterTriggersController player;

    public int timer;

    public Text timerText;
    public GameObject startCntPanel;

    private int AllTime;

    public Text winText;






    void Start(){
        if(timer==0){
            timer=30;
        }

        AllTime=timer;

        timerText.text=timer.ToString();
        cnt=4;
        cntText.gameObject.SetActive(true);
        StartCoroutine(startCnt());
    }


    IEnumerator startCnt(){
        while(cnt!=-1){
            
            cnt--;
            if(cnt==0){
                cntText.text="Start!";
            }
            else if(cnt>0){
                cntText.text=cnt.ToString();
            }
            else{
                startCntPanel.gameObject.SetActive(false);
                //cntText.gameObject.SetActive(false);
                StartCoroutine(game());
            }
            yield return new WaitForSeconds(1);
        }
    }

    void Update(){
       if(player.winPanel.activeSelf){
           StopAllCoroutines();
           timerText.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled=false;
           player.GetComponent<Rigidbody>().velocity=Vector3.zero;

           timerText.GetComponent<Animator>().SetBool("win",true);

           winText.GetComponent<Text>().text="Level "+(player.level+1).ToString()+" completed!\n"+(AllTime-timer).ToString()+" seconds spent";
        }
        if(player.diePanel.activeSelf){
            player.GetComponent<Rigidbody>().velocity=Vector3.zero;
            player.diePanel.SetActive(true);
            StopAllCoroutines();
            timerText.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled=false;

            timerText.GetComponent<Animator>().SetBool("lose",true);
        }
    }

    IEnumerator game(){
        while(timer>0){
            timer--;
            timerText.GetComponent<Text>().text=timer.ToString();

            if(timer==0){
                player.GetComponent<Rigidbody>().velocity=Vector3.zero;
                player.diePanel.SetActive(true);
                StopAllCoroutines();
                timerText.gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>().enabled=false;

                timerText.GetComponent<Animator>().SetBool("lose",true);
                //timerText.GetComponent<Text>().color=new Color32(255,0,0,255);
                //timerText.gameObject.transform.GetChild(0).GetComponent<Image>().color=new Color32(255,0,0,255);
            }

            yield return new WaitForSeconds(1); 
        }
    }
}

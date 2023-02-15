using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject[] gameObjectsToChange;
    public GameObject handleOff, handleOn;
    public Vector3 newPos;
    public Vector3 newRotate;

    void OnTriggerStay(Collider other){
        if(other.gameObject.name.ToUpper().Contains("PLAYER")){
            foreach(var gameObj in gameObjectsToChange){
                if(gameObj.transform.position.y>-6){
                    gameObj.transform.position=Vector3.MoveTowards(gameObj.transform.position,
                                                                new Vector3(gameObj.transform.position.x,-6,gameObj.transform.position.z),0.04f);
                }
                /*else{
                    gameObj.transform.position=Vector3.MoveTowards(gameObj.transform.position,
                                                                new Vector3(gameObj.transform.position.x,-3.5f,gameObj.transform.position.z),1f);
                }*/
                //gameObj.SetActive(!gameObj.activeSelf);
            }

            handleOff.SetActive(false);
            handleOn.SetActive(true);
            
            //handleOff.SetActive(!handleOff.activeSelf);
            //handleOn.SetActive(!handleOn.activeSelf);
        }
    }
}

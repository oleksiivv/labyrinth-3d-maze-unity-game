using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovablePlatform : MonoBehaviour
{
    int dir=-1;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(changeDir());
    }

    void Update(){
        transform.Translate(Vector3.right*dir/20);
    }

    IEnumerator changeDir(){
        while(true){
            dir*=-1;
            yield return new WaitForSeconds(1.5f);
        }
    }

    // void OnCollisionEnter(Collision other){
    //     if(other.gameObject.name.ToUpper().Contains("PLAYER")){
    //         other.gameObject.transform.parent=this.gameObject.transform;
    //     }
    // }

    // void OnCollisionExit(Collision other){
    //     if(other.gameObject.name.ToUpper().Contains("PLAYER")){
    //         other.gameObject.transform.parent=null;
    //     }
    // }
}

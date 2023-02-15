using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    void OnTriggerEnter(Collider other){
        if(!other.gameObject.name.ToUpper().Contains("PLAYER") && !other.gameObject.name.ToUpper().Contains("PLANE")){
            Destroy(gameObject);
        }
    }
}

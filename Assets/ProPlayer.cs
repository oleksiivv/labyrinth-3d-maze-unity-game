using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProPlayer : MonoBehaviour
{
    public CharacterTriggersController character;
    // Start is called before the first frame update

    void Start(){
        character=GetComponent<CharacterTriggersController>();
    }

    private int collisionsNear=0;
    void OnCollisionEnter(Collision other){
        collisionsNear++;
        if(collisionsNear==0 /*&& character.rigidbody.velocity.y==0*/){
            character.rigidbody.velocity=Vector3.down*100;
            Invoke(nameof(showDiePanel),1f);
            StopAllCoroutines();
        }
    }

    void Update(){
        print(collisionsNear);
    }
    void OnCollisionExit(Collision other){
        collisionsNear--;
        if(collisionsNear==0 /*&& character.rigidbody.velocity.y==0*/){
            character.rigidbody.velocity=Vector3.down*100;
            Invoke(nameof(showDiePanel),1f);
            StopAllCoroutines();
        }
    
    }

    public void showDiePanel(){
        character.diePanel.SetActive(true);
    }
}

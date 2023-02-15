using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveController : MonoBehaviour
{
    public Joystick joystick;
    private Rigidbody rb;

    public int movable;

    private GameObject camera;
    // Start is called before the first frame update
    void Start()
    {
        movable=1;
        Time.timeScale=1;
        rb = GetComponent<Rigidbody>();

        camera=Camera.main.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(Vector3.right*-joystick.Horizontal*3*movable);  
        rb.AddForce(Vector3.forward*-joystick.Vertical*3*movable); 
        if(joystick.Horizontal!=0){
            if(camera.gameObject.transform.position.x>-5 || camera.gameObject.transform.position.x<5){
                camera.gameObject.transform.Translate(new Vector3(1,0,0)*joystick.Horizontal/70);
            }
        }  
    }
}

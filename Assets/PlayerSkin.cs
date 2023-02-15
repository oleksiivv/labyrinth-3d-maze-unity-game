using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour
{
    public Material[] skins;

    public GameObject coin;
    public Vector2 startPoint,endPoint;

    void Start()
    {

        // for(int x=(int)mapWidth.x;x<(int)mapWidth.y;x++){
        //     for(int y=(int)mapHeight.x;y<(int)mapHeight.y;y++){

        //     }
        // }

        for(int i=0; i<Application.loadedLevel+Random.Range(0,3);i++){
            Instantiate(coin, new Vector3(Random.Range(startPoint.x,endPoint.x),-3f,Random.Range(startPoint.y,endPoint.y)), coin.transform.rotation);
        }

        GetComponent<MeshRenderer>().material=skins[PlayerPrefs.GetInt("CurrentPlayerMaterial")];
 
    }

    
}

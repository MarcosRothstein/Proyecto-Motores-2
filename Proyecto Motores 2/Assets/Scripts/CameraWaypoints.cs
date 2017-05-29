using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWaypoints : MonoBehaviour {

    public int ID;
    public string cameraName;
    public bool transition;
    public float speed;
    public CameraEffects cameraEffect;

    public  enum CameraEffects
    {
        Old_TV,
        Static,
        Cold
    } 

}

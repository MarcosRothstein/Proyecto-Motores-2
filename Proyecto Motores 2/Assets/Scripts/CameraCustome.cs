using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCustome : MonoBehaviour
{
    public List<CameraWaypoints> cameraWaypoints;
    public int numberOfCameras=0;
    public CameraWaypoints cameraAdded;


    public void AddWaypoint()
    {
        //Debug.Log("Camera added");
        CameraWaypoints newCamera = Instantiate(cameraAdded,transform.position,transform.rotation);
        numberOfCameras++;
        newCamera.ID = numberOfCameras;
        newCamera.cameraName = "Camera " + newCamera.ID;
        newCamera.gameObject.name = newCamera.cameraName;
        cameraWaypoints.Add(newCamera);
    }

    public void RemoveLastWaypoint()
    {
        if (numberOfCameras < 1) return;
        numberOfCameras--;
        DestroyImmediate(cameraWaypoints[numberOfCameras].gameObject);
        cameraWaypoints.RemoveAt(numberOfCameras);
        
    }

    public void RemoveAllWaypoints()
    {
        //Debug.Log("Remove all cameras");
        numberOfCameras = 0;
        foreach(CameraWaypoints cam in cameraWaypoints)
        {
            //print(cam.cameraName);
            DestroyImmediate(cam.gameObject);
        }
        cameraWaypoints.Clear();
    }
}

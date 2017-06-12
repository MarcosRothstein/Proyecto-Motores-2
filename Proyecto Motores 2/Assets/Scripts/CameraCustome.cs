using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCustome : MonoBehaviour
{
    public List<CameraWaypoints> cameraWaypoints;
    public int numberOfCameras=0;
    public CameraWaypoints cameraAdded;
    public List<Curve> curves;
    public Curve curve;
    GameObject curveContainer;

    public void AddWaypoint()
    {
        //Debug.Log("Camera added");
        CameraWaypoints newCamera = Instantiate(cameraAdded,transform.position,transform.rotation);
        numberOfCameras++;
        newCamera.ID = numberOfCameras;
        newCamera.cameraName = "Camera " + newCamera.ID;
        newCamera.gameObject.name = newCamera.cameraName;
        cameraWaypoints.Add(newCamera);

        //Agregar curva
        if(numberOfCameras>1)
        {
            
            if (curveContainer == null)
            {
                curveContainer = new GameObject();
                curveContainer.gameObject.name = "Curves";
            }
            
            Curve newCurve = Instantiate(curve, new Vector3(0,0,0), Quaternion.identity);
            curves.Add(newCurve);
            newCurve.CreateCurve(cameraWaypoints[numberOfCameras-2].gameObject.transform.position, cameraWaypoints[numberOfCameras-1].gameObject.transform.position, numberOfCameras-1);
            newCurve.transform.SetParent(curveContainer.transform);

        }
    }

    public void RemoveLastWaypoint()
    {
        if (numberOfCameras < 1) return;
        numberOfCameras--;
        DestroyImmediate(cameraWaypoints[numberOfCameras].gameObject);
        cameraWaypoints.RemoveAt(numberOfCameras);

        if (curves.Count < 1) return;
        DestroyImmediate(curves[numberOfCameras - 1].gameObject);
        curves.RemoveAt(numberOfCameras - 1);

    }

    public void RemoveAllWaypoints()
    {

        foreach (Curve c in curves)
        {
            DestroyImmediate(c.gameObject);
        }
        curves.Clear();

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

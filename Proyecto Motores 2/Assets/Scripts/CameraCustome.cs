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
    GameObject cameraContainer;

    public void AddWaypoint()
    {
        //Debug.Log("Camera added");

        if (cameraContainer == null)
        {
            cameraContainer = new GameObject();
            cameraContainer.gameObject.name = "Camera Positions";
        }

        CameraWaypoints newCamera = Instantiate(cameraAdded,transform.position,transform.rotation);
        numberOfCameras++;
        newCamera.ID = numberOfCameras;
        newCamera.cameraName = "Camera " + newCamera.ID;
        newCamera.gameObject.name = newCamera.cameraName;
        cameraWaypoints.Add(newCamera);
        newCamera.transform.SetParent(cameraContainer.transform);


        //Agregar curva
        if (numberOfCameras>1)
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
        if (cameraContainer != null && numberOfCameras == 0) DestroyImmediate(cameraContainer.gameObject);


        if (curves.Count < 1) return;
        DestroyImmediate(curves[numberOfCameras - 1].gameObject);
        curves.RemoveAt(numberOfCameras - 1);
        if (curveContainer != null && curves.Count == 0) DestroyImmediate(curveContainer.gameObject);

    }

    public void RemoveAllWaypoints()
    {

        foreach (Curve c in curves)
        {
            DestroyImmediate(c.gameObject);
        }
        curves.Clear();
        if(curveContainer!=null)DestroyImmediate(curveContainer.gameObject);
        //Debug.Log("Remove all cameras");
        numberOfCameras = 0;
        foreach(CameraWaypoints cam in cameraWaypoints)
        {
            //print(cam.cameraName);
            DestroyImmediate(cam.gameObject);
        }
        cameraWaypoints.Clear();
        if(cameraContainer!=null)DestroyImmediate(cameraContainer.gameObject);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {

    public Nodo[] points;

    public void Reset()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = new Vector3(i,0f,0f);
        }
    }
    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points[0].transform.position, points[1].transform.position, points[2].transform.position, t));
    }
   
}

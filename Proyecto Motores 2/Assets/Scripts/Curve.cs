using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {
    public Nodo Prfab;
    public List<Nodo> points = new List<Nodo>();
    public void Reset()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].transform.position = new Vector3(i,0f,0f);
        }
    }
    public Vector3 GetDirection(float t)
    {
        return GetVelocity(t).normalized;
    }
    public void AddCurve()
    {
        Nodo point = points[points.Length - 1];
        point.transform.position += new Vector3 (1f,0f,0f);
        points[points.Length - 3] = Instantiate<Nodo>(Prfab);
        points[points.Length - 3].transform.SetParent(this.transform);
        point.transform.position += new Vector3(1f, 0f, 0f);
        points[points.Length - 2] = Instantiate<Nodo>(Prfab);
        points[points.Length - 2].transform.SetParent(this.transform);
        point.transform.position += new Vector3(1f, 0f, 0f);
        points[points.Length - 1] = Instantiate<Nodo>(Prfab);
        points[points.Length - 1].transform.SetParent(this.transform);
    }
    public void deletLastCurve()
    {

    }
    public Vector3 GetPoint(float t)
    {
        return transform.TransformPoint(Bezier.GetPoint(points[0].transform.position, points[1].transform.position, points[2].transform.position, points[3].transform.position, t));
    }
    public Vector3 GetVelocity(float t)
    {
        return transform.TransformPoint(Bezier.GetFirstD(points[0].transform.position, points[1].transform.position, points[2].transform.position, points[3].transform.position, t)) -
            transform.position;
    }
   
}

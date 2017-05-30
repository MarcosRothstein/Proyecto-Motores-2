using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {
    public Nodo Prfab;
    public List<Nodo> points = new List<Nodo>();
    public void Reset()
    {
        for (int i = 0; i < points.Count; i++)
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
        for (int i = 0; i < 3; i++)
        {
            var nodo = Instantiate<Nodo>(Prfab);
            points.Add(nodo);
            nodo.transform.position += new Vector3(1f+i, 0f, 0f);
            nodo.transform.SetParent(this.transform);
        }       
    }
    public void deletLastCurve()
    {
        if (points.Count >= 3)
        {
            var elim = points.GetRange(points.Count - 3, 3);
            points.RemoveRange(points.Count - 3, 3);
            for (int i = 0; i < elim.Count; i++)
            {
                DestroyImmediate(elim[i].gameObject);
            }
        }
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

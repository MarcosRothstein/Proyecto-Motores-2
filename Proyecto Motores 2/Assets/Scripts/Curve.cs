using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve : MonoBehaviour {
    public Nodo Prfab;
    public int id;
    public List<Nodo> points = new List<Nodo>();

    public void CreateCurve(Vector3 pos1, Vector3 pos2, int identity)
    {
        var nodo1 = Instantiate<Nodo>(Prfab);
        points.Add(nodo1);
        nodo1.transform.position = pos1;
        nodo1.transform.SetParent(this.transform);
        nodo1.gameObject.name = "Nodo1";

        var nodo2 = Instantiate<Nodo>(Prfab);
        points.Add(nodo2);
        nodo2.transform.position = new Vector3((pos1.x+pos2.x)/2, (pos1.y + pos2.y) / 2,pos1.z);
        nodo2.transform.SetParent(this.transform);
        nodo2.gameObject.name = "Nodo2";

        var nodo3 = Instantiate<Nodo>(Prfab);
        points.Add(nodo3);
        nodo3.transform.position = new Vector3((pos1.x + pos2.x) / 2, (pos1.y + pos2.y) / 2, pos2.z);
        nodo3.transform.SetParent(this.transform);
        nodo3.gameObject.name = "Nodo3";

        var nodo4 = Instantiate<Nodo>(Prfab);
        points.Add(nodo4);
        nodo4.transform.position = pos2;
        nodo4.transform.SetParent(this.transform);
        nodo4.gameObject.name = "Nodo4";


        id = identity;
    }


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

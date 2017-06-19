using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Curve))]
public class CurveEditor : Editor {


    private Curve _target;
    private Transform handleTransform;
    private Quaternion handleRotation;
    private const int lineSteps = 10;

    private const float handleSize = 0.04f;
	private const float pickSize = 0.06f;
	
	private int selectedIndex = -1;

    private const float directionScale = 0.5f;

    public void Update()
    {
        OnSceneGUI();
    }

    private void OnSceneGUI()
    {
        _target = (Curve)target;
        handleTransform = _target.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
        handleTransform.rotation : Quaternion.identity;

        Vector3 p0 = ShowPoint(0);

        for (int i = 1; i < _target.points.Count; i += 3)
        {
            Vector3 p1 = ShowPoint(i);
            Vector3 p2 = ShowPoint(i + 1);
            Vector3 p3 = ShowPoint(i + 2);

            Handles.color = Color.gray;
            Handles.DrawLine(p0, p1);
            Handles.DrawLine(p2, p3);
            Handles.DrawBezier(p0, p3, p1, p2, Color.white, null, 2f);

            p0 = p3;
        }

    }
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _target = (Curve)target;
        if (GUILayout.Button("Add Curve"))
        {
            Undo.RecordObject(_target, "Add Curve");
            _target.AddCurve();
        }
        if (GUILayout.Button("delete Last Curve"))
        {
            _target.deletLastCurve();
        }
    }
  
    private void showDir()
    {
        Handles.color = Color.green;
        Vector3 point = _target.GetPoint(0f);
        Handles.DrawLine(point, point + _target.GetDirection(0f) * directionScale);
        for (int i = 1; i <= lineSteps; i++)
        {
            point = _target.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(point, point + _target.GetDirection(i / (float)lineSteps) * directionScale);
        }
    }
    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(_target.points[index].transform.position);
        Handles.color = Color.white;
        if (Handles.Button(point, handleRotation, handleSize, pickSize, Handles.DotCap))
        {
            selectedIndex = index;
        }
        if (selectedIndex == index)
        {
            EditorGUI.BeginChangeCheck();
            point = Handles.DoPositionHandle(point, handleRotation);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_target, "Move Point");
                _target.points[index].transform.position = handleTransform.InverseTransformPoint(point);
            }
        }
        return point;
    }
}

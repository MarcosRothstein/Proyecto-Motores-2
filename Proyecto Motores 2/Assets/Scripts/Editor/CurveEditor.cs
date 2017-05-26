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

    private void OnSceneGUI()
    {
        _target = (Curve)target;
        handleTransform = _target.transform;
        handleRotation = Tools.pivotRotation == PivotRotation.Local ?
            handleTransform.rotation : Quaternion.identity;
        Vector3 p0 = ShowPoint(0);
        Vector3 p1 = ShowPoint(1);
        Vector3 p2 = ShowPoint(2);

        Handles.color = Color.gray;
        Handles.DrawLine(p0, p1);
        Handles.DrawLine(p1, p2);

        Handles.color = Color.white;
        Vector3 lineStart = _target.GetPoint(0f);
        for (int i = 1; i <= lineSteps; i++)
        {
            Vector3 lineEnd = _target.GetPoint(i / (float)lineSteps);
            Handles.DrawLine(lineStart, lineEnd);
            lineStart = lineEnd;
        }

    }
    private Vector3 ShowPoint(int index)
    {
        Vector3 point = handleTransform.TransformPoint(_target.points[index].transform.position);
        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, handleRotation);
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(_target, "Move Point");
            _target.points[index].transform.position = handleTransform.InverseTransformPoint(point);
        }
        return point;
    }
}

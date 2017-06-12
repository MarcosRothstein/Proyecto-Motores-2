using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(CameraCustome))]
public class CameraCustomeEditor : Editor {

    private CameraCustome _target;

	// Use this for initialization
	void OnEnable ()
    {
        _target = (CameraCustome)target;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void OnInspectorGUI()
    {
        ShowValues();
        Repaint();
    }

    private void ShowValues()
    {
        EditorGUILayout.LabelField("Camera Customize Editor", EditorStyles.centeredGreyMiniLabel);
        if (GUILayout.Button("Add camera"))
        {
            _target.AddWaypoint();
        }
        if (GUILayout.Button("Remove last camera"))
        {
            _target.RemoveLastWaypoint();
        }
        if (GUILayout.Button("Remove all cameras"))
        {
            _target.RemoveAllWaypoints();
        }


        _target.cameraAdded = (CameraWaypoints)EditorGUILayout.ObjectField("Waypoint: ", _target.cameraAdded, typeof(CameraWaypoints), true);
        _target.curve = (Curve)EditorGUILayout.ObjectField("Curve: ", _target.curve, typeof(Curve), true);




        //Recorre la lista y grafica por cada elemento en ella sus elementos
        for (int i = 0; i < _target.numberOfCameras; i++)
        {
            // _target.cameraWaypoints[i] = (CameraWaypoints)EditorGUILayout.ObjectField(_target.cameraWaypoints[i], typeof(CameraWaypoints));
            _target.cameraWaypoints[i].cameraName = EditorGUILayout.TextField(_target.cameraWaypoints[i].cameraName);
            _target.cameraWaypoints[i].gameObject.name = _target.cameraWaypoints[i].cameraName;
            _target.cameraWaypoints[i].transition = EditorGUILayout.Toggle("Transition", _target.cameraWaypoints[i].transition);
            if(_target.cameraWaypoints[i].transition==true)
            {
                _target.cameraWaypoints[i].speed = EditorGUILayout.FloatField("Speed of transition", _target.cameraWaypoints[i].speed);
            }
            //_target.cameraWaypoints[i].cameraEffect = EditorGUILayout.EnumPopup("Efecto", CameraWaypoints.CameraEffects);


        }

    }


}

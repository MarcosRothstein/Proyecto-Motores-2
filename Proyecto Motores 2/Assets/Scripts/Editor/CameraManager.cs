	using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CameraManager : EditorWindow {

	private Camera _camera;
	private RenderTexture _preview;
[MenuItem("Window/Shot editor/Open camera manager")]

	static void CreateWindow()
	{
		GetWindow(typeof(CameraManager)).Show();

	}
		
	private void GetCameraPreview()
	{
		_preview = new RenderTexture(512,512,0);	
	}


	void OnGUI()
	{

		minSize = new Vector2(256, 512);

		_camera = (Camera)EditorGUILayout.ObjectField("Cámara: ", _camera, typeof(Camera),true);


		EditorGUILayout.BeginHorizontal();

		GUILayout.Button("←");
		GUILayout.Button("Update");
		GUILayout.Button(	"→");


		EditorGUILayout.EndHorizontal();


		if(_camera != null)
		{
			GetCameraPreview();
			_camera.targetTexture = _preview;
			_camera.Render();
			_camera.targetTexture = null;
			if(_preview) GUI.DrawTexture(new Rect(0 , 50, 270, 256), _preview, ScaleMode.ScaleToFit, false, 1f);
		}
		else 
		{
			GUILayoutUtility.GetRect(0, 25, 64, 64);
			EditorGUILayout.LabelField("El preview aparecerá aquí");
			GUILayoutUtility.GetRect(0, 25, 64, 64);

		}



	}



	void OnDestroy()
	{
		_preview = null;
		_camera = null;
	}
	 	
}

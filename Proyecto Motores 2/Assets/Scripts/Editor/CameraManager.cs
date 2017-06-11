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
		EditorGUILayout.BeginVertical();
		_camera = (Camera)EditorGUILayout.ObjectField("Cámara: ", _camera, typeof(Camera),true);




		EditorGUILayout.BeginHorizontal();

		GUILayout.Button("←");
		GUILayout.Button("Update");
		GUILayout.Button("→");


		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();

		//_camera.transform.position= EditorGUILayout.Vector3Field("Camera Position ", _camera.transform.position);

	

	

		if(_camera != null)
		{
			GetCameraPreview();
			_camera.targetTexture = _preview;
			_camera.Render();
			_camera.targetTexture = null;
			if(_preview) GUI.DrawTexture(GUILayoutUtility.GetRect(256,256), _preview, ScaleMode.ScaleToFit, false, 1f);

			EditorGUILayout.Vector3Field("Camera Position", _camera.transform.position);
			
			//EditorGUI.Vector3Field(GUILayoutUtility.GetRect(64,64), "Camera position:", _camera.transform.position);

		}
		else 
		{

			GUI.DrawTexture(GUILayoutUtility.GetRect(256,256),(Texture2D)Resources.Load("Textures/Checker"),ScaleMode.ScaleToFit,true,1f);
//			GUILayoutUtility.GetRect(128,128);
//			EditorGUILayout.LabelField("El preview aparecerá aquí");
//			GUILayoutUtility.GetRect(128,128);

		}
			


	
	}



	void OnDestroy()
	{
		_preview = null;
		_camera = null;
	}
	 	
}

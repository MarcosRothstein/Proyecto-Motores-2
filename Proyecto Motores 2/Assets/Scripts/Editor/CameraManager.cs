	using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class CameraManager : EditorWindow {

	private Camera _camera;
	private Texture2D _preview;
	private CameraCustome _cc;
	private bool _previewRendered = false;


[MenuItem("Window/Shot editor/Open camera manager")]

	static void CreateWindow()
	{
		GetWindow(typeof(CameraManager)).Show();

	}


		
	
		
	private void GetCameraPreview()
	{
		if(!_previewRendered)
		{
		RenderTexture rt = new RenderTexture(Screen.width,Screen.height, 24);
		RenderTexture.active = rt;
		_camera.targetTexture = rt;
		_preview =  new Texture2D(Screen.width,Screen.height);
		_camera.Render();
		_preview.ReadPixels(new Rect(0,0,Screen.width,Screen.height),0,0);
		_preview.Apply();

		_camera.targetTexture = null;
		RenderTexture.active = null;
		DestroyImmediate(rt);
		Debug.Log("Rendering thing!");
			_previewRendered = true;
		}
//		if(!_previewRendered)
//
//		{
//
//			Debug.Log(_preview);
//			_preview = new RenderTexture(512,512,0);
//		
//
//			_camera.targetTexture = _preview;
//			_camera.Render();
//			_camera.targetTexture = null;
//			_previewRendered = true;
//		}
	
	}


	void OnGUI()
	{
		
		minSize = new Vector2(256, 512);

		_camera = (Camera)EditorGUILayout.ObjectField("Cámara: ", _camera, typeof(Camera),true);







		//_camera.transform.position= EditorGUILayout.Vector3Field("Camera Position ", _camera.transform.position);

	

	

		if(_camera != null)
		{
			_cc = _camera.GetComponent<CameraCustome>();
				
			GetCameraPreview();

			if(_preview) GUI.DrawTexture(GUILayoutUtility.GetRect(256,256), _preview, ScaleMode.ScaleToFit, false, 1f);

			EditorGUILayout.Vector3Field("Camera Position", _camera.transform.position);

			EditorGUILayout.Vector4Field("Camera Rotation", QuaternionToVector4(_camera.transform.rotation));
			//EditorGUI.Vector3Field(GUILayoutUtility.GetRect(64,64), "Camera position:", _camera.transform.position);

			EditorGUILayout.LabelField("Number of camera waypoints stored " + _cc.numberOfCameras, EditorStyles.centeredGreyMiniLabel);


		}
		else 
		{

			GUI.DrawTexture(GUILayoutUtility.GetRect(256,256),(Texture2D)Resources.Load("Textures/Checker"),ScaleMode.ScaleToFit,true,1f);
//			GUILayoutUtility.GetRect(128,128);
//			EditorGUILayout.LabelField("El preview aparecerá aquí");
//			GUILayoutUtility.GetRect(128,128);

		}


		if(_cc  != null && _camera != null) 
		{
		EditorGUILayout.BeginHorizontal();

		//GUILayout.Button("←")
		if(GUILayout.Button("←"))
		{
				Debug.Log("ASDASD");

		}
		
		if(GUILayout.Button("Update Preview"))
		{
				_previewRendered = false;
				GetCameraPreview();

		}

		if(GUILayout.Button("→"))
		{
				Debug.Log("ASDASD+++");
		}



		EditorGUILayout.EndHorizontal();

			EditorGUILayout.LabelField("Current camera  " + _cc.numberOfCameras, EditorStyles.centeredGreyMiniLabel);
		}

	
	}

	static Vector4 QuaternionToVector4(Quaternion rot)
	{
		return new Vector4(rot.x, rot.y, rot.z, rot.w);
	}


	void OnDestroy()
	{
		
	}
	 	
}

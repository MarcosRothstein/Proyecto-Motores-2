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
		_camera = (Camera)EditorGUILayout.ObjectField("Cámara: ", _camera, typeof(Camera),true);
		if(_camera != null)
		{
			GetCameraPreview();
			_camera.targetTexture = _preview;
			_camera.Render();
			_camera.targetTexture = null;
			if(_preview) GUI.DrawTexture(new Rect(25, 25, 200, 200), _preview, ScaleMode.ScaleToFit, false, 1f);
		}
		
	}

	void OnDestroy()
	{
		_preview = null;
	}
	 	
}

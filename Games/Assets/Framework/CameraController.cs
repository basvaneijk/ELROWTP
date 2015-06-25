/*
    Framework for ELRO Wants To Play
    Copyright (C) 2015 Frits Mensink
    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.
    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.
    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public WebCamTexture mCamera = null;
	public GameObject plane;
	
	// Use this for initialization
	void Start ()
	{
		plane = GameObject.FindWithTag ("Webcamtexture");
		float height = (float)Camera.main.orthographicSize * 2.0f;
		float width = (float)height * Screen.width / Screen.height;
		plane.transform.localScale = new Vector3 (width / 10, 0.1f, height / 17);
		WebCamTexture tCamera = new WebCamTexture ();
		int h = tCamera.requestedHeight;
		int w = tCamera.requestedWidth;
		mCamera = new WebCamTexture (WebCamTexture.devices [0].name, w / 2, h / 2, 30);
		plane.GetComponent<Renderer> ().material.mainTexture = mCamera;
		mCamera.Play ();
     
	}
	void OnDestroy ()
	{
		mCamera.Stop ();
	}
	
	// Update is called once per frame
	void Update ()
	{
       
	}
}

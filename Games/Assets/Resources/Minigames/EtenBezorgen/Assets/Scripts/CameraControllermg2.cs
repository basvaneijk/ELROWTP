using UnityEngine;
using System.Collections;

public class CameraControllermg2 : MonoBehaviour
{
	public WebCamTexture mCamera = null;
	public GameObject plane;
	
	// Use this for initialization
	void Start ()
	{
		plane = GameObject.FindWithTag ("Webcamtexture");
		float height = (float) Camera.main.orthographicSize * 2.0f;
		float width = (float) height * Screen.width / Screen.height;
		plane.transform.localScale = new Vector3(width/10,0.1f,height/17);
		WebCamTexture tCamera = new WebCamTexture();
		int h = tCamera.requestedHeight;
		int w = tCamera.requestedWidth;
		mCamera = new WebCamTexture (WebCamTexture.devices [0].name, w/2, h/2, 30);
		plane.GetComponent<Renderer>().material.mainTexture = mCamera;
		mCamera.Play();
     
	}
	void OnApplicationQuit() {
		mCamera.Stop ();
	}
	
	// Update is called once per frame
	void Update()
	{
       
	}
}
